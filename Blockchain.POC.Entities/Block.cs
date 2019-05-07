using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.POC.Entities
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<Account> Accounts { get; set; }
        public int Nonce { get; set; } = 0;
    }
}
