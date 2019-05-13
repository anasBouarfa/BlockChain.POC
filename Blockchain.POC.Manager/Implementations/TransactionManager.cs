using Blockchain.POC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blockchain.POC.Manager
{
    public partial class GlobalManager
    {
        public List<Transaction> GetTransactionsByAddress(BlockChain chain, string address)
        {
            IEnumerable<Transaction> recievedTransactions = chain.Blocks.SelectMany(t => t.Transactions).Where(t => t.ToAddress == address);
            IEnumerable<Transaction> createdTransactions = chain.Blocks.SelectMany(t => t.Transactions).Where(t => t.FromAddress == address);

            recievedTransactions = recievedTransactions.Select(s => new Transaction(s.FromAddress.IsNullOrWhitespace() ? "system" : GetUserNameByAddress(chain, s.FromAddress), "You", s.Amount, false));
            createdTransactions = createdTransactions.Select(s => new Transaction("You", GetUserNameByAddress(chain, s.ToAddress), s.Amount));

            return createdTransactions.Union(recievedTransactions).OrderByDescending(o => o.CreationDate).ToList();
        }

        public string GetUserNameByAddress(BlockChain chain, string address)
        {
            return chain.Accounts.FirstOrDefault(a => a.Address == address).Username;
        }

        public List<Transaction> GetPendingTransactionsByAddress(BlockChain chain, string address)
        {
            if(!chain.PendingTransactions.IsNullOrEmpty() && !address.IsNullOrWhitespace())
            {
                address = address.Encrypt();

                return chain.PendingTransactions.Where(t => t.FromAddress == address || t.ToAddress == address).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}