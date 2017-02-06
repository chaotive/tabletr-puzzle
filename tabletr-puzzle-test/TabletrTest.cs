﻿using System;
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
            var t1 = new Tabletr(5, 5, new List<int>() { 0, 2, 1 });
            Assert.AreEqual(3, t1.solution.Count);
            Assert.AreEqual(25, t1.state.Count);

            var t2 = new Tabletr(2, 2, new List<int>() { 0, 2, 1, 3 }, new List<int>() { 2, 1, 0, 3 });
            Assert.AreEqual(4, t2.solution.Count);
            Assert.AreEqual(4, t2.state.Count);            
        }

        [TestMethod]
        public void usage()
        {
            var t1 = new Tabletr(2, 2, new List<int>() { 0, 2, 1, 3 }, new List<int>() { 2, 1, 0, 3 });
            /* Pending, should test it with 2x2 or bigger matrixs */
        }

        [TestMethod]
        public void testTryAndMove()
        {
            var t1 = new Tabletr(2, 2, new List<int>() { 0, 2, 1, 3 }, new List<int>() { 2, 1, 0, 3 });
            Assert.AreEqual(-1, t1.tryAndMove(1));
            Assert.AreEqual(2, t1.tryAndMove(0));
        }

        [TestMethod]
        public void testTryMove()
        {
            var t1 = new Tabletr(2, 2, new List<int>() { 0, 2, 1, 3 }, new List<int>() { 2, 1, 0, 3 });
            Assert.AreEqual("down", t1.tryMove(0));
            Assert.AreEqual("can't move", t1.tryMove(1));
            Assert.AreEqual("can't move", t1.tryMove(2));
            Assert.AreEqual("left", t1.tryMove(3));

            var t2 = new Tabletr(2, 2, new List<int>() { 0, 2, 1, 3 }, new List<int>() { 1, 0, 2, 3 });
            Assert.AreEqual("right", t2.tryMove(0));
            Assert.AreEqual("can't move", t2.tryMove(1));
            Assert.AreEqual("can't move", t2.tryMove(2));
            Assert.AreEqual("up", t2.tryMove(3));
        }

        [TestMethod]
        public void testMove()
        {
            var t1 = new Tabletr(2, 2, new List<int>() { 0, 2, 1, 3 }, new List<int>() { 2, 1, 0, 3 });
            Assert.AreEqual(2, t1.move(0, "down"));            
            Assert.AreEqual(0, t1.move(1,"left"));

            var t2 = new Tabletr(2, 2, new List<int>() { 0, 2, 1, 3 }, new List<int>() { 1, 0, 2, 3 });
            Assert.AreEqual(1, t2.move(0, "right"));            
            Assert.AreEqual(0, t2.move(2, "up"));
        }

        [TestMethod]
        public void testGenerateState()
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

        [TestMethod]
        public void testCheckCompleted()
        {      
            Assert.AreEqual(false, Tabletr.checkCompleted( new List<int>() { 0 }, new List<int>() { 1 } ));
            Assert.AreEqual(false, Tabletr.checkCompleted(new List<int>() { 0 }, new List<int>() { 0, 0 }));
            Assert.AreEqual(false, Tabletr.checkCompleted(new List<int>() { 0, 1, 2 }, new List<int>() { 1, 2, 0 }));

            Assert.AreEqual(true, Tabletr.checkCompleted(new List<int>() { 0, 0 }, new List<int>() { 0, 0 }));
            Assert.AreEqual(true, Tabletr.checkCompleted(new List<int>() { 0, 1, 2 }, new List<int>() { 0, 1, 2 }));
        }
    }
}
