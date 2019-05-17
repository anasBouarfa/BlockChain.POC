using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using System.Linq;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Blockchain.POC.P2PServer
{
    public class Server : WebSocketBehavior
    {
        private WebSocketServer wss = null;
        private IGlobalManager _globalManager;
        private static int _port;

        public Server()
        {
        }

        public Server(int port) : base()
        {
            _port = port;
            _globalManager = new GlobalManager(port);
        }

        public void Start()
        {
            wss = new WebSocketServer(string.Format("ws://localhost:{0}", _port));
            wss.AddWebSocketService<Server>("/BlockchainPOC");
            wss.Start();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            using (_globalManager = new GlobalManager(_port))
            {
                if (e.Data == "Show me your blockchain !")
                {
                    Send(_globalManager.LoadBlockChainAsString());
                }
                else
                {
                    BlockChain remoteChain = JsonConvert.DeserializeObject<BlockChain>(e.Data);
                    BlockChain localChain = _globalManager.LoadLocalBlockChain();

                    bool updateBlockchain = false;

                    if (!_globalManager.IsLocalBlockChainUpToDate(localChain, remoteChain))
                    {
                        updateBlockchain = true;
                    }

                    if (remoteChain.Accounts.Count > localChain.Accounts.Count)
                    {
                        localChain.Accounts = remoteChain.Accounts.Union(localChain.Accounts).Distinct().ToList();
                        updateBlockchain = true;
                    }

                    if(updateBlockchain)
                    {
                        _globalManager.SaveBlockChain(localChain);
                    }
                }
            }
        }
    }
}