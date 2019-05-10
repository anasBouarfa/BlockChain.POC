using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace testCommon
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        [DataRow(235874)]
        [DataRow(58775)]
        [DataRow(988545479)]
        public void EncodeNumberTest(int num)
        {
            string tested = num.EncodeNumber();

            Assert.AreEqual(num, tested.DecodeToNumber());
        }

        [TestMethod]
        [DataRow("Oumaima Mdaghri")]
        [DataRow("j'ai fait les courses")]
        [DataRow("Accenture")]
        public void EncryptTest(string text)
        {
            string tested = text.Encrypt();

            Assert.AreEqual(text, tested.Decrypt());
        }
    }
}