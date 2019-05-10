using Blockchain.POC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blockchain.POC.Manager
{
    public partial class GlobalManager
    {
        #region Account methods

        public string IsAccountLoginValid(BlockChain chain, string username, string password)
        {
            return chain.Accounts.FirstOrDefault(a => a.HashedPassword == password.GetHash() && a.Username == username).Address;
        }

        public bool IsUsernameTaken(BlockChain chain, string username)
        {
            return chain.Accounts.Any(a => a.Username == username);
        }

        public BlockChain CreateAccount(BlockChain chain, string username, string password, string firstname, string lastname, DateTime dateOfBirth)
        {
            if (!username.IsNullOrWhitespace() &&
               !password.IsNullOrWhitespace())
            {
                if (!chain.Accounts.Select(s => s.Username).Any(a => a == username.Encrypt()))
                {

                    var account = new Account(username, password, firstname, lastname, dateOfBirth);

                    account = EncryptAccount(account);
                    chain.Accounts?.Add(account);

                    chain.PendingTransactions?.Add(new Transaction(null, account.Address, 1));

                    return chain;
                }
            }

            return null;
        }

        public double GetAccountBalance(BlockChain chain, string address)
        {
            return chain.Blocks.SelectMany(s => s.Transactions)
                    .Where(t => t.FromAddress == address || t.ToAddress == address)
                    .Select(s => s.ToAddress == address ? s.Amount : -s.Amount)
                    .Sum();
        }

        public Account GetAccountByAddress(BlockChain chain, string address)
        {
            return chain.Accounts.FirstOrDefault(a => a.Address == address);
        }

        public bool IsAccountAddressValid(BlockChain chain, string address)
        {
            if (chain != null)
            {
                return chain.Accounts.Any(a => a.Address == address);
            }

            return false;
        }

        public Account EncryptAccount(Account account)
        {
            return new Account()
            {
                Address = account.Address,
                CreationDate = account.CreationDate,
                FirstName = account.FirstName.Encrypt(),
                LastName = account.LastName.Encrypt(),
                Username = account.Username.Encrypt(),
                HashedPassword = account.HashedPassword,
                Balance = int.Parse(account.Balance).EncodeNumber()
            };
        }

        #endregion Account methods
    }
}
