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
        #region Blockchain methods

        void AddBlock(BlockChain chain, Block block);

        bool IsBlockChainValid(BlockChain chain);

        BlockChain LoadLocalBlockChain();

        bool IsLocalBlockchainAvailable();

        bool IsLocalBlockChainUpToDate(BlockChain localChain, BlockChain remoteChain);

        bool SaveBlockChain(BlockChain chain);

        #endregion Blockchain methods
    }
}
