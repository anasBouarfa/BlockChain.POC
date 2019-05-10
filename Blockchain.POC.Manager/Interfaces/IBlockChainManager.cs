using Blockchain.POC.Entities;

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