using System;

namespace Blockchain.POC.Entities
{
    public class Transaction
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public DateTime CreationDate { get; set; }
        public double Amount { get; set; }

        public Transaction(string fromAddress, string toAddress, double amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
            CreationDate = DateTime.Now;
        }
    }
}