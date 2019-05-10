using System.Collections.Generic;

namespace Blockchain.POC.Entities
{
    public class BlockChain
    {
        public List<Transaction> PendingTransactions { get; set; }

        public List<Block> Blocks { set; get; }

        public List<Account> Accounts { get; set; }

        public const int Difficulty = 2;

        public int Reward = 1; //1 cryptocurrency
    }
}