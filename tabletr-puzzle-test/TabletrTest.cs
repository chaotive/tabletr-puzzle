using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tabletr_puzzle;
using System.Collections.Generic;
using System.Linq;

namespace tabletr_puzzle_test
{
    [TestClass]
    public class TabletrTest
    {
        [TestMethod]
        public void constructor()
        {
            var t1 = new Tabletr(5, 5, new List<int>() { 4, 2, 1 });                    
            Assert.AreEqual(3, t1.solution.Count);
            Assert.AreEqual(25, t1.state.Count);

            var t2 = new Tabletr(2, 2, new List<int>() { 4, 2, 1, 3}, new List<int>() { 2, 1, 0, 3 });
            Assert.AreEqual(4, t2.solution.Count);
            Assert.AreEqual(4, t2.state.Count);
        }

        [TestMethod]
        public void testGenerateState()
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

            var state1 = Tabletr.generateState(4);
            var state2 = Tabletr.generateState(4);

            Assert.AreEqual(state1.Contains(0), true);
            Assert.AreEqual(4, state1.Count);
            Assert.AreNotEqual(state1, state2);            
        }

        void debugGenerateState(int length) {
            Console.Write("State " + length + ": ");
            foreach (var n in Tabletr.generateState(length)) { Console.Write(n + " "); }
            Console.WriteLine();
        }
    }
}
