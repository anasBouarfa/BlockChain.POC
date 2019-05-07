using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockchain.POC.Entities;
using System.Threading.Tasks;

namespace Blockchain.POC.Manager
{
    public interface IGlobalManager
    {

        #region Blockchain methods

        void AddBlock(BlockChain chain, Block block);

        bool IsBlockChainValid(BlockChain chain);

        BlockChain LoadLocalBlockChain();

        bool IsLocalBlockchainAvailable();

        bool IsLocalBlockChainUpToDate(BlockChain localChain, BlockChain remoteChain);

        bool SaveBlockChain(BlockChain chain);

        #endregion Blockchain methods

        #region Block methods

        void Mine(Block block);

        Block GetLastBlock(BlockChain chain);

        #endregion Block methods

        #region Account methods

        Account CreateAccount(string username, string password, string firstname, string lastname, DateTime dateOfBirth);

        bool IsUsernameTaken(BlockChain chain, string username);

        string IsAccountLoginValid(BlockChain chain, string username, string password);

        double GetAccountBalance(BlockChain chain, string address);

        Account GetAccountByAddress(string address);

        #endregion Account methods

        #region Transaction methods

        List<Transaction> GetTransactionsByAddress(BlockChain chain, string address);

        #endregion Transaction methods

    }
}
