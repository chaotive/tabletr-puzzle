using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tabletr_puzzle;
using System.Collections.Generic;

namespace tabletr_puzzle_test
{
    [TestClass]
    public class TabletrTest
    {                
        [TestMethod]        
        public void constructor()
        {
            var t1 = new Tabletr(2, 2, new List<string>() { "", "3", "2", "1" }, 3);
            Assert.AreEqual(4, t1.solutionSequence.Count);
            Assert.AreEqual(4, t1.state.Count);
            
            var t2 = new Tabletr(2, 2, new List<string>() { "", "2", "1", "3" }, new List<string>() { "2", "1", "", "3" });
            Assert.AreEqual(4, t2.solutionSequence.Count);
            Assert.AreEqual(4, t2.state.Count);                        
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "complexity")]
        public void constructor_exceptions()
        {            
            var t3 = new Tabletr(2, 2, new List<string>() { "", "3", "2", "1" }, 4);
        }
        
        [TestMethod]
        public void usage3x3_fixed()
        {
            var t1 = new Tabletr(3, 3, new List<string>() {
                "1", "2", "3",
                "4", "5", "6",
                "7", "8", ""
            }, new List<string>() {
                "1", "5", "2",
                "7", "4", "3",
                "", "8", "6"
            });
            
            Assert.AreEqual("down", t1.move("7"));
            Assert.AreEqual("left", t1.move("4"));
            Assert.AreEqual("down", t1.move("5"));
            Assert.AreEqual("left", t1.move("2"));
            Assert.AreEqual("up", t1.move("3"));
            Assert.AreEqual("up", t1.move("6"));

            Assert.AreEqual(true, t1.completed);            
            Assert.AreEqual("completed", t1.move("6"));
        }

        [TestMethod]
        public void usage3x3_dynamic()
        {
            var t1 = new Tabletr(3, 3, new List<string>() {
                "1", "2", "3",
                "4", "5", "6",
                "7", "8", ""
            }, 8);

            t1.solution.moves.Reverse();
            t1.solution.moves.ForEach(m => {
                t1.move(m.value);
            });
            
            Assert.AreEqual(true, t1.completed);
            Assert.AreEqual("completed", t1.move("6"));
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

            var t3 = new Tabletr(3, 3, new List<string>() {
                "1", "2", "3",
                "4", "5", "6",
                "7", "8", ""
            }, new List<string>() {
                "1", "5", "2",
                "7", "4", "3",
                "", "8", "6"
            });

            Assert.AreEqual("", t3.move("3"));
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
