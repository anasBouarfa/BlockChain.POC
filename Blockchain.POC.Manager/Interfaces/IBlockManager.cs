using Blockchain.POC.Entities;

namespace Blockchain.POC.Manager
{
    public partial interface IGlobalManager
    {
        #region Block methods

        Block Mine(Block block);

        Block GetLastBlock(BlockChain chain);

        string CalculateBlockHash(Block block);

        #endregion Block methods
    }
}