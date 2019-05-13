using Blockchain.POC.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blockchain.POC.Manager
{
    public partial class GlobalManager
    {
        public bool IsBlockChainValid(BlockChain chain)
        {
            if (chain != null && !chain.Blocks.IsNullOrEmpty())
            {
                chain.Blocks = chain.Blocks.OrderBy(o => o.Index).ToList();

                Block previousBlock = chain.Blocks.FirstOrDefault();

                foreach (var block in chain.Blocks.Skip(1))
                {
                    if (block.PreviousHash != previousBlock.Hash)
                    {
                        return false;
                    }
                    else
                    {
                        previousBlock = block;
                    }
                }

                return true;
            }

            return false;
        }


        public BlockChain AddBlock(BlockChain chain)
        {
            Block latestBlock = GetLastBlock(chain);

            Block block = new Block(DateTime.Now, latestBlock.Hash, chain.PendingTransactions)
            {
                Index = ++latestBlock.Index
            };

            block = Mine(block);

            chain.Blocks.Add(block);

            foreach(var transaction in block.Transactions)
            {
                var senderAccount = chain.Accounts.FirstOrDefault(a => a.Address == transaction.FromAddress);
                chain.Accounts.Remove(senderAccount);
                senderAccount.Balance = (senderAccount.Balance.DecodeToNumber() - transaction.Amount).EncodeNumber();
                chain.Accounts.Add(senderAccount);

                var recieverAccount = chain.Accounts.FirstOrDefault(a => a.Address == transaction.FromAddress);
                chain.Accounts.Remove(recieverAccount);
                recieverAccount.Balance = (recieverAccount.Balance.DecodeToNumber() + transaction.Amount).EncodeNumber();
                chain.Accounts.Add(recieverAccount);
            }

            chain.PendingTransactions = new List<Transaction>();

            return chain;
        }

        public bool IsLocalBlockchainAvailable()
        {
            return FileHelper.IsFileExistant(@"C:/Blockchains/" + usedPort);
        }

        public bool IsLocalBlockChainUpToDate(BlockChain localChain, BlockChain remoteChain)
        {
            return IsBlockChainValid(localChain) && localChain.Blocks.Count >= remoteChain.Blocks.Count;
        }

        public BlockChain LoadLocalBlockChain()
        {
            return FileHelper<BlockChain>.LoadObjectFromJson(@"C:/Blockchains/" + usedPort + ".json");
        }

        public bool SaveBlockChain(BlockChain chain)
        {
            return FileHelper<BlockChain>.CreateFileFromObject(chain, @"C:/Blockchains/", usedPort + ".json");
        }
    }
}