using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
                if (e.Data == "Hi Client")
                {
                    Console.WriteLine(e.Data);
                }
                else
                {
                    BlockChain newChain = JsonConvert.DeserializeObject<BlockChain>(e.Data);

                    if (_globalManager.IsLocalBlockChainUpToDate(chain, newChain))
                    {
                        List<Transaction> newTransactions = new List<Transaction>();
                        newTransactions.AddRange(newChain.PendingTransactions);
                        newTransactions.AddRange(chain.PendingTransactions);

                        newChain.PendingTransactions?.AddRange(chain.PendingTransactions);

                        chain = newChain;

                        _globalManager.SaveBlockChain(chain);
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