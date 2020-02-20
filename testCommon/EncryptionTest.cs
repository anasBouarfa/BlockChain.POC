using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Blockchain.POC.Test
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(235874)]
        [DataRow(58775)]
        [DataRow(988545479)]
        public void EncodeNumberTest(int num)
        {
            string tested = num.EncodeNumber();

            Assert.AreEqual(num, tested.DecodeToNumber());
        }

        [TestMethod]
        [DataRow("Anas Bouarfa")]
        [DataRow("I am testing at this very moment")]
        [DataRow("Please Work !")]
        public void EncryptTest(string text)
        {
            string tested = text.Encrypt();

            Assert.AreEqual(text, tested.Decrypt());
        }
    }
}