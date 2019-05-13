using System;

namespace Blockchain.POC.Entities
{
    public class Transaction
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public DateTime CreationDate { get; set; }
        public int Amount { get; set; }

        public Transaction(string fromAddress, string toAddress, int amount)
        {
            FromAddress = fromAddress?.Encrypt();
            ToAddress = toAddress?.Encrypt();
            Amount = amount;
            CreationDate = DateTime.Now;
        }
    }
}