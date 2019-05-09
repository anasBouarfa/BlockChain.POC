using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockchain.POC.Entities;

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
