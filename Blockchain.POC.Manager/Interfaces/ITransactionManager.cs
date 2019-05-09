using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockchain.POC.Entities;
using System.Threading.Tasks;

namespace Blockchain.POC.Manager
{
    public partial interface IGlobalManager
    {
        #region Transaction methods

        List<Transaction> GetTransactionsByAddress(BlockChain chain, string address);

        #endregion Transaction methods

    }
}
