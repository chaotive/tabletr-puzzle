using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace tabletr_puzzle
{
    public class Solution
    {
        public List<string> initialState;
        public List<string> solutionSequence;
        public List<MoveOp> moves;

        public Solution(List<string> solutionSequence, List<string> initialState, List<MoveOp> moves)
        {
            this.solutionSequence = solutionSequence;
            this.initialState = initialState;
            this.moves = moves;
        }

        public static Solution generate(Tabletr tp)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            Debug.WriteLine("solutionSequence:");
            tp.solutionSequence.ForEach(tpv => Debug.Write(" " + tpv));
            Debug.WriteLine("");

            tp.state = new List<string>();
            tp.solutionSequence.ForEach(s => tp.state.Add(s));
            var validMoves = 1;
            var spaceIndex = tp.state.IndexOf("");
            var index = 0;

            if (spaceIndex == 0) index = spaceIndex + 1;
            else index = spaceIndex - 1;
            
            var value = tp.state[index];
            var direction = tp.tryMove(index);
            var moves = new List<MoveOp>() { new MoveOp(value, direction) };
            tp.state[spaceIndex] = value;
            tp.state[index] = "";

            Debug.WriteLine("state:");
            tp.state.ForEach(tpv => Debug.Write(" " + tpv));
            Debug.WriteLine("\nspaceIndex: " + spaceIndex + " index: " + index);

            var usedValues = new List<string>() { value };
            var availableValues = Enumerable.Range(1, tp.solutionSequence.Count - 1).
                OrderBy(x => random.Next()).
                ToList().
                ConvertAll<string>(x =>
                    x.ToString());
            availableValues.Remove(value);
            
            while (validMoves < tp.complexity)
            {                
                var i = 0;
                var v = "";
                var d = "";

                do
                {
                    Debug.WriteLine("availableValues iteration " + i);
                    availableValues.ForEach(av => Debug.Write(" " + av));
                    Debug.WriteLine("");
                    v = availableValues[i];
                    d = tp.move(v);
                    i++;
                } while (d == "" && i < availableValues.Count);
                if (d == "" && i == availableValues.Count) break;

                validMoves++;
                moves.Add(new MoveOp(v, d));
                usedValues.Add(v);
                availableValues.Remove(v);
            }

            Debug.WriteLine("moves:");
            moves.ForEach(mo => Debug.WriteLine(mo.value + " " + mo.direction));
            
            return new Solution(tp.solutionSequence, tp.state, moves);
        }
    }
}
