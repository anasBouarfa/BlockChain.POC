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

        public BlockChain AddBlock(BlockChain chain, string minerAddress)
        {
            Block latestBlock = GetLastBlock(chain);

            Block block = new Block(DateTime.Now, latestBlock.Hash, chain.PendingTransactions)
            {
                Index = ++latestBlock.Index
            };

            block = Mine(block);

            chain.Blocks.Add(block);

            chain.PendingTransactions = new List<Transaction>
            { new Transaction(null, minerAddress, BlockChain.Reward)};

            foreach (var transaction in block.Transactions)
            {
                if(transaction.FromAddress != null)
                {
                    var senderAccountBalance = chain.Accounts.FirstOrDefault(a => a.Address == transaction.FromAddress.Decrypt()).Balance;
                    chain.Accounts.FirstOrDefault(a => a.Address == transaction.FromAddress.Decrypt()).Balance = (senderAccountBalance.DecodeToNumber() - transaction.Amount).EncodeNumber();
                }

                var recieverAccountBalance = chain.Accounts.FirstOrDefault(a => a.Address == transaction.ToAddress.Decrypt()).Balance;
                chain.Accounts.FirstOrDefault(a => a.Address == transaction.ToAddress.Decrypt()).Balance = (recieverAccountBalance.DecodeToNumber() + transaction.Amount).EncodeNumber();
            }

            return chain;
        }

        public bool IsLocalBlockchainAvailable()
        {
            return FileHelper.IsFileExistant(@"C:/Blockchains/" + usedPort + ".json");
        }

        public bool IsLocalBlockChainUpToDate(BlockChain localChain, BlockChain remoteChain)
        {
            return IsBlockChainValid(localChain) && localChain.Blocks.Count >= remoteChain.Blocks.Count;
        }

        public BlockChain LoadLocalBlockChain()
        {
            return FileHelper<BlockChain>.LoadObjectFromJson(@"C:/Blockchains/" + usedPort + ".json");
        }

        public string LoadBlockChainAsString()
        {
            return FileHelper.ReadFromFile(@"C:/Blockchains/" + usedPort + ".json");
        }

        public bool SaveBlockChain(BlockChain chain)
        {
            return FileHelper<BlockChain>.CreateFileFromObject(chain, @"C:/Blockchains/", usedPort + ".json");
        }
    }
}