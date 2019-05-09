using Blockchain.POC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blockchain.POC.Manager
{
    public partial class GlobalManager
    {
        #region Block methods

        public Block GetLastBlock(BlockChain chain)
        {
            return chain.Blocks.OrderByDescending(o => o.Index).FirstOrDefault();
        }

        public void Mine(Block block)
        {
            throw new NotImplementedException();
        }

        public void AddBlock(BlockChain chain, Block block)
        {
            throw new NotImplementedException();
        }

        #endregion Block methods
    }
}
