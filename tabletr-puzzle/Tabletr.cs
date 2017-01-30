using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tabletr_puzzle
{
    public class Tabletr
    {
        public int rows;
        public int columns;
        public List<int> solution;
        public List<int> state;
        
        public Tabletr(int rows, int columns, List<int> solution, List<int> initialState = null) {
            var length = rows * columns;
            this.rows = rows;
            this.columns = columns;
            this.solution = solution;
            state = initialState ?? generateState(length);            
        }

        public static List<int> generateState(int length) {
            var random = new Random(Guid.NewGuid().GetHashCode());
            return Enumerable.Range(0, length).OrderBy(x => random.Next()).ToList();
        }
    }
}
