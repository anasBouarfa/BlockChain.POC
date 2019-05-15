using System;

namespace Blockchain.POC.Manager
{
    public partial class GlobalManager : IGlobalManager, IDisposable
    {
        private readonly int usedPort;

        public GlobalManager(int port)
        {
            usedPort = port;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}