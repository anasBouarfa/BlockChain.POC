using Blockchain.POC.Entities;
using System;
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

        public Block Mine(Block block)
        {
            var leadingZeros = new string('0', BlockChain.Difficulty);
            string hash = new string(' ', 128);

            while (hash.IsNullOrWhitespace() || hash.Substring(0, BlockChain.Difficulty) != leadingZeros)
            {
                block.Nonce++;
                hash = CalculateBlockHash(block);
            }

            block.Hash = hash;

            return block;
        }

        public string CalculateBlockHash(Block block)
        {
            return $"{block.TimeStamp}-{block.PreviousHash}-{block.Transactions.JsonSerialize()}-{block.Nonce}".GetHash();
        }

        #endregion Block methods
    }
}