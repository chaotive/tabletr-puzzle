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


            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(14);
            debugGenerateState(32);

            Assert.AreNotEqual(Tabletr.generateState(2), Tabletr.generateState(2));            
        }

        void debugGenerateState(int length) {
            Console.Write("State " + length + ": ");
            foreach (var n in Tabletr.generateState(length)) { Console.Write(n + " "); }
            Console.WriteLine();
        }
    }
}
