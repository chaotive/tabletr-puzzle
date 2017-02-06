using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public bool completed = false;
        
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

            if ( (index == 0) || (index % columns == 0) ) right = index + 1;                
            else if (index % columns - 1 == 0) left = index - 1;
            else {
                left = index - 1;
                right = index + 1;
            }

            if (index < columns) down = index + columns;
            else if (index < rows * columns && index >= (rows * columns) - columns) up = index - columns;
            else
            {
                up = index - columns;
                down = index + columns;
            }
            
            if (up != -1 && state[up] == 0) return "up";
            if (down != -1 && state[down] == 0) return "down";
            if (left != -1 && state[left] == 0) return "left";
            if (right != -1 && state[right] == 0) return "right";

            return "can't move";
        }

        public int move(int index, string direction) {
            var next = -1;
            switch (direction) {
                case "up": next = index - columns; break;
                case "down": next = index + columns; break;
                case "left": next = index - 1; break;
                case "right": next = index + 1; break;
            }

            var oldElem = state[index];
            var newElem = state[next];
            state[index] = state[newElem];
            state[next] = oldElem;

            completed = checkCompleted(solution, state);

            return next;
        }

        public int tryAndMove(int index) {
            var d = tryMove(index);
            if (d == "can't move") { return -1; }
            else return move(index, d);
        }

        public static bool checkCompleted(List<int> solution, List<int> state) {
            return Enumerable.SequenceEqual(solution, state);            
        }
    }
}
