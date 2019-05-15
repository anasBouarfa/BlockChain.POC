using System.Linq;
using Blockchain.POC.Entities;
using Blockchain.POC.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testCommon
{
    [TestClass]
    public class Serialization
    {
        [TestMethod]
        public void Deserialization()
        {
            using (var _globalManager = new GlobalManager(12900))
            {
                var chain = _globalManager.LoadLocalBlockChain();
                Transaction transaction = chain.Blocks.SelectMany(s => s.Transactions).FirstOrDefault();

                Assert.IsNotNull(transaction.ToAddress);
            }
        }
    }
}
