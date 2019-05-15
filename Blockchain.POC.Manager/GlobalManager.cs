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
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}