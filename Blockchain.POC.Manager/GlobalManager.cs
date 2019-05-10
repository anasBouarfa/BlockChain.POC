namespace Blockchain.POC.Manager
{
    public partial class GlobalManager : IGlobalManager
    {
        private readonly int usedPort;

        public GlobalManager(int port)
        {
            usedPort = port;
        }
    }
}