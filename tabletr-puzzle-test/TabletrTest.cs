using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tabletr_puzzle;

namespace tabletr_puzzle_test
{
    [TestClass]
    public class TabletrTest
    {
        [TestMethod]
        public void Functions()
        {
            Assert.AreEqual(2, Tabletr.someFunction(1));
        }
    }
}
