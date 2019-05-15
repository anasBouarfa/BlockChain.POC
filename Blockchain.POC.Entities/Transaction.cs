using Newtonsoft.Json;
using System;

namespace Blockchain.POC.Entities
{
    public class Transaction
    {
        public string FromAddress { get; set; }

        [JsonProperty(PropertyName = "ToAddress")]
        public string ToAddress { get; set; }
        public DateTime CreationDate { get; set; }
        public int Amount { get; set; }

        public Transaction()
        {

        }

        public Transaction(string fromAddress, string toAddress, int amount, bool encrypt = true)
        {
            FromAddress = encrypt ? fromAddress?.Encrypt() : fromAddress;
            ToAddress = encrypt ? toAddress?.Encrypt() : toAddress;
            Amount = amount;
            CreationDate = DateTime.Now;
        }
    }
}