using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.POC.Entities
{
    public class BlockChain
    {
        public List<Transaction> PendingTransactions { get; set; }
        public List<Block> Blocks { set; get; }

        public List<Account> Accounts { get; set; }

        public int Difficulty { set; get; } = 2;

        public int Reward = 1; //1 cryptocurrency
    }
}
