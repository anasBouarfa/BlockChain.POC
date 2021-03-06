﻿using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using System;
using System.Collections.Generic;

namespace Blockchain.POC.ConfigurationConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Start--Genesis block
            CreateGenesisBlock(12900);
            Console.WriteLine("Genesis block was succesfully created");
            Console.ReadLine();
            //End--Genesis block
        }

        private static void CreateGenesisBlock(int port)
        {
            IGlobalManager _globalManager = new GlobalManager(port);

            Account firstAccount = new Account("admin", "12345", "admin", "admin", new DateTime(1998, 9, 9))
            {
                Balance = BlockChain.Reward.ToString()
            };

            Transaction initialTransaction = new Transaction(null, firstAccount.Address, BlockChain.Reward);

            var chain = new BlockChain
            {
                Accounts = new List<Account>
                {
                    _globalManager.EncryptAccount(firstAccount)
                },
                PendingTransactions = new List<Transaction>
                {
                    new Transaction(null, firstAccount.Address, BlockChain.Reward, true)
                }
            };

            var block = new Block(DateTime.Now, null, new List<Transaction>() { initialTransaction });

            block.Hash = _globalManager.CalculateBlockHash(block);
            block.Index = 0;

            chain.Blocks = new List<Block>
            {
                block
            };

            _globalManager.SaveBlockChain(chain);
        }
    }
}