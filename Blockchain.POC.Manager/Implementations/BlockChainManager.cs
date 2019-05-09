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

        public bool IsLocalBlockchainAvailable()
        {
            return FileHelper.IsFileExistant(@"Blockchains/" + usedPort);
        }

        public bool IsLocalBlockChainUpToDate(BlockChain localChain, BlockChain remoteChain)
        {
            return IsBlockChainValid(localChain) && localChain.Blocks.Count >= remoteChain.Blocks.Count;
        }

        public BlockChain LoadLocalBlockChain()
        {
            return FileHelper<BlockChain>.LoadObjectFromJson(@"Blockchains/" + usedPort + ".json");
        }

        public bool SaveBlockChain(BlockChain chain)
        {
            return FileHelper<BlockChain>.CreateFileFromObject(chain, @"Blockchains/", usedPort + ".json");
        }
    }
}
