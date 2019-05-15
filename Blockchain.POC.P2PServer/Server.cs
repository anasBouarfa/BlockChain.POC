using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using Newtonsoft.Json;
using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Blockchain.POC.P2PServer
{
    public class Server : WebSocketBehavior
    {
        private WebSocketServer wss = null;
        private IGlobalManager _globalManager;

        public Server()
        {
        }

        public Server(IGlobalManager globalManager) : base()
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
            if (e.Data == "Show me your blockchain !")
            {
                Send(_globalManager.LoadBlockChainAsString());
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