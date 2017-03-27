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
        public Solution solution;
        public List<string> solutionSequence;        
        public List<string> state;
        public bool completed;
        public int complexity;

        public Tabletr(int rows, int columns, List<string> solutionSequence, int complexity) :
            this(rows, columns, solutionSequence, complexity, null) { }

        public Tabletr(int rows, int columns, List<string> solutionSequence, List<string> initialState) :
            this(rows, columns, solutionSequence, 0, initialState) { }
       
        private Tabletr(int rows, int columns, List<string> solutionSequence, int complexity, List<string> initialState) {                
            this.rows = rows;
            this.columns = columns;
            this.solutionSequence = solutionSequence;
            this.complexity = complexity;
            if (initialState == null) solution = generateSolution(this);                                
            else state = initialState;

            completed = false;
        }

        public static Solution generateSolution(Tabletr tp) {
            var random = new Random(Guid.NewGuid().GetHashCode());             
            var validMoves = 0;
            
            tp.state = tp.solutionSequence;
            var spaceIndex = tp.state.IndexOf("");
            var index = 0;
            if (spaceIndex == 0) index = spaceIndex + 1;
            else index = spaceIndex - 1;
            var value = tp.state[index];
            var direction = tp.tryMove(index);
            var moves = new List<MoveOp>() { new MoveOp(value, direction) };
            tp.state[spaceIndex] = value;
            tp.state[index] = "";

            Console.WriteLine("tp.state: " + tp.state);

            return new Solution();
            
            /*while (validMoves < tp.complexity) {
                var x = (random.Next(tp.solutionSequence.Count) + 1).ToString();
                var r = tp.move(x);
                if (! (r == "" || r == "completed"))  
                {
                    validMoves++;
                    moves.Add(new MoveOp());
                }                 
            }
            
            //tp.state = algo;

            return new Solution(Enumerable.Range(0, solutionSequence.Count).
                OrderBy(x => random.Next()).
                ToList().
                ConvertAll<string>(x => 
                    x.ToString().Replace("0", "")));*/
        }
        

        public string tryMove(int index) {
            // 1 2 3
            // 4 5 6
            // 7 8 9
            
            var left = -1;
            var right = -1;
            var up = -1;
            var down = -1;
            
            if ( (index == 0) || (index % columns == 0) ) right = index + 1;                
            else if ((index + 1) % columns == 0) left = index - 1;
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

            completed = checkCompleted(solutionSequence, state);

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
