using Blockchain.POC.Entities;
using System.Collections.Generic;

namespace Blockchain.POC.Manager
{
    public partial interface IGlobalManager
    {
        #region Transaction methods

        List<Transaction> GetTransactionsByAddress(BlockChain chain, string address);

        List<Transaction> GetPendingTransactionsByAddress(BlockChain chain, string address);

        #endregion Transaction methods
    }
}