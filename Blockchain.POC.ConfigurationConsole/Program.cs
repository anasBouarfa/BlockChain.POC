using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.POC.ConfigurationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Genesis block
            IGlobalManager _globalManager = new GlobalManager(12900);


            Account firstAccount = new Account("admin", "12345", "admin", "admin", new DateTime(1998, 9, 9))
            {
                Balance = (1).EncodeNumber()
            };

            Transaction initialTransaction = new Transaction(null, firstAccount.Address, BlockChain.Reward);

            var blockchain = new BlockChain
            {
                Accounts = new List<Account>
                {
                    _globalManager.EncryptAccount(firstAccount)
                },
                PendingTransactions = new List<Transaction>(),
                Blocks = new List<Block>
                {
                    new Block(DateTime.Now, null, new List<Transaction>() {initialTransaction})
                },
            };

            _globalManager.SaveBlockChain(blockchain);

        }
    }
}
