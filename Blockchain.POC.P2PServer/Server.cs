using System;
using System.Collections.Generic;
using Blockchain.POC.Manager;
using System.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Text;
using System.Threading.Tasks;
using Blockchain.POC.Entities;
using Newtonsoft.Json;

namespace Blockchain.POC.P2PServer
{
    public class Server : WebSocketBehavior
    {
        IDictionary<string, WebSocketServer> wsDict = new Dictionary<string, WebSocketServer>();
        WebSocketServer wss = null;
        private IGlobalManager _globalManager;
        private int port;

        public Server()
        {
        }

        public Server(IGlobalManager globalManager)
        {
            _globalManager = globalManager;
        }

        public void Start(int port)
        {
            wss = new WebSocketServer(string.Format("ws://localhost:{0}", port));
            wss.AddWebSocketService<Server>("/BlockchainPOC");
            wss.Start();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Hi Server")
            {
                Console.WriteLine(e.Data);
                Send("Hi Client");
            }
            else
            {
                BlockChain remoteChain = JsonConvert.DeserializeObject<BlockChain>(e.Data);

                if (_globalManager.IsBlockChainValid(remoteChain) && remoteChain.Blocks.Count >10 /*localChainCount*/)
                {
                    //List<Transaction> newTransactions = new List<Transaction>();
                    //newTransactions.AddRange(remoteChain.PendingTransactions);
                    //newTransactions.AddRange(Program.PhillyCoin.PendingTransactions);

                    //remoteChain.PendingTransactions = newTransactions;
                    //Program.PhillyCoin = remoteChain;
                }

                //if (!chainSynched)
                //{
                //    Send(JsonConvert.SerializeObject(Program.PhillyCoin));
                //    chainSynched = true;
                //}
            }
        }

        //public void Send(string url, string data)
        //{
        //    foreach (var item in wsDict)
        //    {
        //        if (item.Key == url)
        //        {
        //            item.Value.Send(data);
        //        }
        //    }
        //}
    }



    //public void Broadcast(string data)
    //{
    //    foreach (var item in wsDict)
    //    {
    //        item.Value.Send(data);
    //    }
    //}

    //public IList<string> GetServers()
    //{
    //    IList<string> servers = new List<string>();
    //    foreach (var item in wsDict)
    //    {
    //        servers.Add(item.Key);
    //    }
    //    return servers;
    //}

    //public void Close()
    //{
    //    foreach (var item in wsDict)
    //    {
    //        item.Value.Close();
    //    }
    //}
}
