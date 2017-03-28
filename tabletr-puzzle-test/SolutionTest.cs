using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tabletr_puzzle;
using System.Collections.Generic;
using System.Linq;

namespace tabletr_puzzle_test
{
    [TestClass]
    public class SolutionTest
    {

        [TestMethod]
        public void testGenerateSolution()
        {

            var t1 = new Tabletr(2, 2, 
                new List<string>() { "", "3", "2", "1" }, 
                new List<string>() { "", "3", "2", "1" });
            t1.complexity = 3;

            var t2 = new Tabletr(2, 2,
                new List<string>() { "", "3", "2", "1" },
                new List<string>() { "", "3", "2", "1" });
            t2.complexity = 1;

            var solution1 = Solution.generate(t1);
            Assert.AreEqual(3, solution1.moves.Count);
            CollectionAssert.AreNotEqual(solution1.initialState, solution1.solutionSequence);

            var solution2 = Solution.generate(t2);
            Assert.AreEqual(1, solution2.moves.Count);
            CollectionAssert.AreNotEqual(solution2.initialState, solution2.solutionSequence);

            CollectionAssert.AreEqual(solution1.solutionSequence, solution2.solutionSequence);                        
        }

    }
}
