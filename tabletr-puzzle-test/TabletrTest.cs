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
            var t1 = new Tabletr(5, 5, new List<string>() { "", "2", "1" });
            Assert.AreEqual(3, t1.solution.Count);
            Assert.AreEqual(25, t1.state.Count);

            var t2 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "2", "1", "", "3" });
            Assert.AreEqual(4, t2.solution.Count);
            Assert.AreEqual(4, t2.state.Count);            
        }

        [TestMethod]
        public void usage()
        {
            var t1 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "2", "1", "", "3" });
            /* Pending, should test it with 2x2 or bigger matrixs */
        }

        [TestMethod]
        public void testTryAndMove()
        {
            var t1 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "2", "1", "", "3" });
            Assert.AreEqual(-1, t1.tryAndMoveIndex(1));
            Assert.AreEqual(2, t1.tryAndMoveIndex(0));
        }

        [TestMethod]
        public void testTryMoveIndex()
        {
            var t1 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "2", "1", "", "3" });
            Assert.AreEqual("down", t1.tryMove(0));
            Assert.AreEqual("can't move", t1.tryMove(1));
            Assert.AreEqual("can't move", t1.tryMove(2));
            Assert.AreEqual("left", t1.tryMove(3));

            var t2 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "1", "", "2", "3" });
            Assert.AreEqual("right", t2.tryMove(0));
            Assert.AreEqual("can't move", t2.tryMove(1));
            Assert.AreEqual("can't move", t2.tryMove(2));
            Assert.AreEqual("up", t2.tryMove(3));
        }

        [TestMethod]
        public void testMove()
        {
            var t1 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "2", "1", "", "3" });
            Assert.AreEqual("down", t1.move("2"));
            Assert.AreEqual("left", t1.move("1"));
            Assert.AreEqual("", t1.move(""));
            Assert.AreEqual("right", t1.move("1"));
            Assert.AreEqual("", t1.move("3"));

            var t2 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "1", "", "2", "3" });
            Assert.AreEqual("right", t2.move("1"));
            Assert.AreEqual("", t2.move(""));
            Assert.AreEqual("up", t2.move("2"));
            Assert.AreEqual("left", t2.move("3"));

            var t3 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "2", "", "1", "3" });
            Assert.AreEqual("right", t3.move("2"));
            Assert.AreEqual("completed", t3.move("2"));
        }

        [TestMethod]
        public void testMoveIndex()
        {
            var t1 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "2", "1", "", "3" });
            Assert.AreEqual(2, t1.moveIndex(0, "down"));            
            Assert.AreEqual(0, t1.moveIndex(1,"left"));

            var t2 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "1", "", "2", "3" });
            Assert.AreEqual(1, t2.moveIndex(0, "right"));            
            Assert.AreEqual(0, t2.moveIndex(2, "up"));
        }

        [TestMethod]
        public void testGenerateIntState()
        {
            /*
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(4);
            debugGenerateState(14);
            debugGenerateState(32);
            */

            var state1 = Tabletr.generateIntState(4);
            var state2 = Tabletr.generateIntState(4);

            Assert.AreEqual(state1.Contains(""), true);
            Assert.AreEqual(4, state1.Count);
            Assert.AreNotEqual(state1, state2);            
        }

        void debugGenerateState(int length) {
            Console.Write("State " + length + ": ");
            foreach (var n in Tabletr.generateIntState(length)) { Console.Write(n + " "); }
            Console.WriteLine();
        }

        [TestMethod]
        public void testCheckCompleted()
        {      
            Assert.AreEqual(false, Tabletr.checkCompleted( new List<string>() { "" }, new List<string>() { "1" } ));
            Assert.AreEqual(false, Tabletr.checkCompleted(new List<string>() { "" }, new List<string>() { "", "" }));
            Assert.AreEqual(false, Tabletr.checkCompleted(new List<string>() { "", "1", "2" }, new List<string>() { "1", "2", "" }));

            Assert.AreEqual(true, Tabletr.checkCompleted(new List<string>() { "", "" }, new List<string>() { "", "" }));
            Assert.AreEqual(true, Tabletr.checkCompleted(new List<string>() { "", "1", "2" }, new List<string>() { "", "1", "2" }));
        }
    }
}
