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
        public List<string> solution;
        public List<string> state;
        public bool completed = false;
        
        public Tabletr(int rows, int columns, List<string> solution, List<string> initialState = null) {
            var length = rows * columns;
            this.rows = rows;
            this.columns = columns;
            this.solution = solution;
            state = initialState ?? generateIntState(length);            
        }

        public static List<string> generateIntState(int length) {
            var random = new Random(Guid.NewGuid().GetHashCode());
            return Enumerable.Range(0, length).
                OrderBy(x => random.Next()).
                ToList().
                ConvertAll<string>(x => 
                    x.ToString().Replace("0", ""));
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
            
            if (up != -1 && state[up] == "") return "up";
            if (down != -1 && state[down] == "") return "down";
            if (left != -1 && state[left] == "") return "left";
            if (right != -1 && state[right] == "") return "right";

            return "can't move";
        }

        public int moveIndex(int index, string direction) {
            var next = -1;
            switch (direction) {
                case "up": next = index - columns; break;
                case "down": next = index + columns; break;
                case "left": next = index - 1; break;
                case "right": next = index + 1; break;
            }

            var oldElem = state[index];
            var newElem = state[next];
            state[index] = newElem;
            state[next] = oldElem;

            completed = checkCompleted(solution, state);

            return next;
        }

        public int tryAndMoveIndex(int index) {
            var d = tryMove(index);
            if (d == "can't move") { return -1; }
            else return moveIndex(index, d);
        }

        public string move(string value)
        {
            if (completed == true) return "completed";
            else
            {
                var index = state.IndexOf(value);
                var d = tryMove(index);
                if (d == "can't move") return "";
                else
                {
                    moveIndex(index, d);
                    return d;
                }
            }
        }

        public static bool checkCompleted(List<string> solution, List<string> state) {
            return Enumerable.SequenceEqual(solution, state);            
        }
    }
}
