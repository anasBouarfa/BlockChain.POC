﻿using System;
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
                BlockChain localChain = _globalManager.LoadLocalBlockChain();

                if (!_globalManager.IsLocalBlockChainUpToDate(localChain, remoteChain))
                {
                    _globalManager.SaveBlockChain(remoteChain);
                }
                else
                {
                    if (remoteChain.Accounts.Count > localChain.Accounts.Count)
                    {
                        localChain.Accounts = remoteChain.Accounts;
                        _globalManager.SaveBlockChain(localChain);
                    }
                }
            }
        }
    }
}
