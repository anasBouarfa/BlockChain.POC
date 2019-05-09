using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockchain.POC.Entities;
using System.Threading.Tasks;

namespace Blockchain.POC.Manager
{
    public partial interface IGlobalManager
    {
        #region Block methods

        void Mine(Block block);

        Block GetLastBlock(BlockChain chain);

        #endregion Block methods
    }
}
