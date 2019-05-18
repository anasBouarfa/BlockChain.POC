using Blockchain.POC.Entities;
using System;
using System.Linq;

namespace Blockchain.POC.Manager
{
    public partial class GlobalManager
    {
        #region Account methods

        public string IsAccountLoginValid(BlockChain chain, string username, string password)
        {
            return chain.Accounts.FirstOrDefault(a => a.HashedPassword == password.GetHash() && a.Username == username.Encrypt())?.Address;
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
                    account.Balance = "0";
                    account = EncryptAccount(account);
                    chain.Accounts?.Add(account);

                    chain.PendingTransactions?.Add(new Transaction(null, account.Address, 1));

                    return chain;
                }
            }

            return null;
        }

        public int GetAccountBalance(BlockChain chain, string address)
        {
            return chain.Accounts.FirstOrDefault(a => a.Address == address).Balance.DecodeToNumber();
        }

        public Account GetAccountByUsername(BlockChain chain, string username)
        {
            return chain.Accounts.FirstOrDefault(a => a.Username == username.Encrypt());
        }

        public string GetFullnameAccountByAddress(BlockChain chain, string address)
        {
            var account = chain.Accounts.FirstOrDefault(a => a.Address == address);

            return account.FirstName + " " + account.LastName;
        }

        public bool IsAccountUsernameValid(BlockChain chain, string username)
        {
            if (chain != null)
            {
                return chain.Accounts.Exists(a => a.Username == username.Encrypt());
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