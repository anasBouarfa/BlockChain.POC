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
            var leadingZeros = new string('0', BlockChain.Difficulty);
            while (block.Hash == null || block.Hash.Substring(0, BlockChain.Difficulty) != leadingZeros)
            {
                block.Nonce++;
                CalculateBlockHash(block);
            }
        }

        public void AddBlock(BlockChain chain, List<Transaction> transactions)
        {
            Block latestBlock = GetLastBlock(chain);

            Block block = new Block(DateTime.Now, latestBlock.Hash, transactions)
            {
                Index = ++latestBlock.Index
            };

            CalculateBlockHash(block);

            Mine(block);
        }

        public void CalculateBlockHash(Block block)
        {
            block.Hash = $"{block.TimeStamp}-{block.PreviousHash}-{block.Transactions.JsonSerialize()}-{block.Nonce}".GetHash();
        }

        #endregion Block methods
    }
}