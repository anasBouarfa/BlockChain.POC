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
            address = address.Encrypt();
            IEnumerable<Transaction> transactions = chain.Blocks.SelectMany(t => t.Transactions);

            IEnumerable<Transaction> recievedTransactions = transactions.Where(t => t.ToAddress == address);
            IEnumerable<Transaction> createdTransactions = transactions.Where(t => t.FromAddress == address);

            recievedTransactions = recievedTransactions.Select(s => new Transaction(s.FromAddress.IsNullOrWhitespace() ? "system" : GetUserNameByAddress(chain, s.FromAddress.Decrypt()), "You", s.Amount, false));
            createdTransactions = createdTransactions.Select(s => new Transaction("You", GetUserNameByAddress(chain, s.ToAddress.Decrypt()), s.Amount, false));

            return createdTransactions.Union(recievedTransactions).OrderByDescending(o => o.CreationDate).ToList();
        }

        public string GetUserNameByAddress(BlockChain chain, string address)
        {
            return chain.Accounts.FirstOrDefault(a => a.Address == address.Decrypt()).Username;
        }

        public List<Transaction> GetPendingTransactionsByAddress(BlockChain chain, string address)
        {
            if (!chain.PendingTransactions.IsNullOrEmpty() && !address.IsNullOrWhitespace())
            {
                address = address.Encrypt();

                var recieved = chain.PendingTransactions.Where(t => t.ToAddress == address);
                var created = chain.PendingTransactions.Where(t => t.FromAddress == address);

                recieved = recieved.Select(s => new Transaction(s.FromAddress.IsNullOrWhitespace() ? "system" : GetUserNameByAddress(chain, s.FromAddress.Decrypt()), "You", s.Amount, false));
                created = created.Select(s => new Transaction(s.FromAddress.IsNullOrWhitespace() ? "system" : GetUserNameByAddress(chain, s.FromAddress.Decrypt()), "You", s.Amount, false));

                return recieved.Union(created).OrderByDescending(o => o.CreationDate).ToList();
            }
            else
            {
                return null;
            }
        }

        public int GetPendingTransactionsBaluByAddress(BlockChain chain, string address)
        {
            if (!chain.PendingTransactions.IsNullOrEmpty() && !address.IsNullOrWhitespace())
            {
                address = address.Encrypt();

                return chain.PendingTransactions.Where(t => t.ToAddress == address && t.FromAddress == address).Sum(s => s.ToAddress == address ? s.Amount : -s.Amount );
            }
            else
            {
                return 0;
            }
        }
    }
}