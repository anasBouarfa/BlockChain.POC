using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockchain.POC.Entities;

namespace Blockchain.POC.Manager
{
    public class GlobalManager : IGlobalManager
    {
        private int usedPort;

        public GlobalManager(int port)
        {
            usedPort = port;
        }

        #region Block methods

        public Block GetLastBlock(BlockChain chain)
        {
            return chain.Blocks.OrderByDescending(o => o.Index).FirstOrDefault();
        }

        public void Mine(Block block)
        {
            throw new NotImplementedException();
        }

        public void AddBlock(BlockChain chain, Block block)
        {
            throw new NotImplementedException();
        }

        #endregion Block methods

        #region Account methods

        public string IsAccountLoginValid(BlockChain chain, string username, string password)
        {
            return chain.Accounts.FirstOrDefault(a => a.HashedPassword == password.GetHash() && a.Username == username).Address;
        }

        public bool IsUsernameTaken(BlockChain chain, string username)
        {
            return chain.Accounts.Any(a => a.Username == username);
        }

        public Account CreateAccount(string username, string password, string firstname, string lastname, DateTime dateOfBirth)
        {
            if (!username.IsNullOrWhitespace() &&
               !password.IsNullOrWhitespace())
            {
                //TODO :check if username exists

                var account = new Account(username, password, firstname, lastname, dateOfBirth);

                return account;
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
            return chain.Accounts.FirstOrDefault(a => a.Username == address);
        }

        public Account GetAccountByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public bool IsAccountAddressValid(BlockChain chain, string address)
        {
            if(chain != null)
            {
                return chain.Accounts.Any(a => a.Address == address);
            }

            return false;
        }

        #endregion Account methods

        #region BlockChain methods

        public bool IsBlockChainValid(BlockChain chain)
        {
            if (chain != null && !chain.Blocks.IsNullOrEmpty())
            {
                chain.Blocks = chain.Blocks.OrderBy(o => o.Index).ToList();

                Block previousBlock = chain.Blocks.FirstOrDefault();

                foreach (var block in chain.Blocks.Skip(1))
                {
                    if (block.PreviousHash != previousBlock.Hash)
                    {
                        return false;
                    }
                    else
                    {
                        previousBlock = block;
                    }
                }

                return true;
            }

            return false;
        }

        public bool IsLocalBlockchainAvailable()
        {
            return FileHelper.IsFileExistant(@"Blockchains/" + usedPort);
        }

        public bool IsLocalBlockChainUpToDate(BlockChain localChain, BlockChain remoteChain)
        {
            return IsBlockChainValid(localChain) && localChain.Blocks.Count >= remoteChain.Blocks.Count;
        }

        public BlockChain LoadLocalBlockChain()
        {
            return FileHelper<BlockChain>.LoadObjectFromJson(@"Blockchains/" + usedPort + ".json");
        }

        public bool SaveBlockChain(BlockChain chain)
        {
            return FileHelper<BlockChain>.CreateFileFromObject(chain, @"Blockchains/", usedPort + ".json");
        }

        #endregion BlockChain methods

        #region Transaction methods

        public List<Transaction> GetTransactionsByAddress(BlockChain chain, string address)
        {
            IEnumerable<Transaction> recievedTransactions = chain.Blocks.SelectMany(t => t.Transactions).Where(t => t.ToAddress == address);
            IEnumerable<Transaction> createdTransactions = chain.Blocks.SelectMany(t => t.Transactions).Where(t => t.FromAddress == address);

            recievedTransactions = recievedTransactions.Select(s => new Transaction(s.FromAddress ?? "system", "You", s.Amount));
            createdTransactions = createdTransactions.Select(s => new Transaction("You", s.ToAddress, s.Amount));

            return createdTransactions.Union(recievedTransactions).OrderByDescending(o => o.CreationDate).ToList();
        }

        public string GetUserNameByAddress(BlockChain chain, string address)
        {
            var account = chain.Accounts.FirstOrDefault(a => a.Address == address);

            return account.FirstName + " " + account.LastName;
        }

        #endregion Transaction methods
    }
}
