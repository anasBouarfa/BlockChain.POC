using Blockchain.POC.Entities;
using System;

namespace Blockchain.POC.Manager
{
    public partial interface IGlobalManager
    {
        #region Account methods

        BlockChain CreateAccount(BlockChain chain, string username, string password, string firstname, string lastname, DateTime dateOfBirth);

        bool IsUsernameTaken(BlockChain chain, string username);

        string IsAccountLoginValid(BlockChain chain, string username, string password);

        int GetAccountBalance(BlockChain chain, string address);

        bool IsAccountUsernameValid(BlockChain chain, string username);

        Account GetAccountByUsername(BlockChain chain, string username);

        Account EncryptAccount(Account account);

        #endregion Account methods
    }
}