using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;

namespace Blockchain.POC.P2PClient
{
    public class Client
    {
        private IGlobalManager _globalManager;
        public IDictionary<string, WebSocket> urlwebSockets { get; set; }

        public Client(IGlobalManager globalManager)
        {
            _globalManager = globalManager;
        }

        public WebSocket Connect(string url, BlockChain chain)
        {
            WebSocket ws = new WebSocket(url);
            ws.OnMessage += (sender, e) =>
            {
                if (e.Data != null)
                {
                    BlockChain remoteChain = JsonConvert.DeserializeObject<BlockChain>(e.Data);

                    if (chain != null)
                    {
                        if (_globalManager.IsLocalBlockChainUpToDate(chain, remoteChain))
                        {
                            if (!chain.PendingTransactions.Any())
                            {
                                if (remoteChain.PendingTransactions.Any())
                                {
                                    chain.PendingTransactions = remoteChain.PendingTransactions;
                                    _globalManager.SaveBlockChain(chain);
                                }
                            }
                            else
                            {
                                if (remoteChain.PendingTransactions.Count >= chain.PendingTransactions.Count)
                                {
                                    DateTime maxDateTime = chain.PendingTransactions.Max(pt => pt.CreationDate);

                                    chain.PendingTransactions.Union(remoteChain.PendingTransactions.Where(pt => pt.CreationDate > maxDateTime));

                                    _globalManager.SaveBlockChain(chain);
                                }
                            }
                        }
                    }
                    else
                    {
                        _globalManager.SaveBlockChain(remoteChain);
                    }
                }
            };

            ws.Connect();

            if (chain != null)
            {
                ws.Send(JsonConvert.SerializeObject(chain));
            }

            return ws;
        }

        public void Send(string url, string data)
        {
            foreach (var item in urlwebSockets)
            {
                if (item.Key == url)
                {
                    item.Value.Send(data);
                }
            }
        }

        public void BroadcastChain(BlockChain chain)
        {
            foreach (var endpoint in urlwebSockets)
            {
                try
                {
                    endpoint.Value.Send(JsonConvert.SerializeObject(chain));
                }
                catch
                {
                }
            }
        }

        public List<string> GetServers()
        {
            List<string> servers = new List<string>();
            foreach (var item in urlwebSockets)
            {
                servers.Add(item.Key);
            }
            return servers;
        }

        public void Close()
        {
            foreach (var item in urlwebSockets)
            {
                item.Value.Close();
            }
        }
    }
}