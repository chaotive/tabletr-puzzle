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

        public string tryMove(int index) {
            // 1  2  3  4
            // 5  6  7  8
            // 9 10 11 12
            //13 14 15 16

            var left = -1;
            var right = -1;
            var up = -1;
            var down = -1;

            if ( (index == 1) || (index % columns + 1 == 0) ) right = index + 1;                
            else if (index % columns == 0) left = index - 1;
            else {
                left = index - 1;
                right = index + 1;
            }

            if (index <= columns) down = index + columns;
            else if (index <= rows * columns && index > (rows * columns) - columns) up = index - columns;
            else
            {
                up = index - columns;
                down = index + columns;
            }

            if (state[up - 1] == 0) return "up";
            if (state[down - 1] == 0) return "down";
            if (state[left - 1] == 0) return "left";
            if (state[right - 1] == 0) return "right";

            return null;
        }
    }
}
