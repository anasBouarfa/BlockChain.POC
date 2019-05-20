using Blockchain.POC.Entities;
using System;

namespace Blockchain.POC.Manager
{
    public partial interface IGlobalManager : IDisposable
    {
        #region Blockchain methods

        BlockChain AddBlock(BlockChain chain, string minerAddress);

        bool IsBlockChainValid(BlockChain chain);

        BlockChain LoadLocalBlockChain();

        string LoadBlockChainAsString();

        bool IsLocalBlockchainAvailable();

        bool IsLocalBlockChainUpToDate(BlockChain localChain, BlockChain remoteChain);

        bool SaveBlockChain(BlockChain chain);

        #endregion Blockchain methods
    }
}