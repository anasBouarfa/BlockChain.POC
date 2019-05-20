using System.Collections.Generic;

namespace Blockchain.POC.Entities
{
    public class BlockChain
    {
        public List<Transaction> PendingTransactions { get; set; }

        public IList<Block> Blocks { set; get; }

        public List<Account> Accounts { get; set; }

        public const int Difficulty = 2;

        public const int Reward = 5; //1 cryptocurrency
    }
}