using System;
using System.Collections.Generic;

namespace Blockchain.POC.Entities
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public int Nonce { get; set; } = 0;

        public Block(DateTime timeStamp, string previousHash, List<Transaction> transactions)
        {
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Transactions = transactions;
        }
    }
}