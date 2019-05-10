using Blockchain.POC.Entities;

namespace Blockchain.POC.Manager
{
    public partial interface IGlobalManager
    {
        #region Block methods

        void Mine(Block block);

        Block GetLastBlock(BlockChain chain);

        void CalculateBlockHash(Block block);

        #endregion Block methods
    }
}