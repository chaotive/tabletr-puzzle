using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tabletr_puzzle
{
    public struct Solution {        
        public List<string> initialState;
        public List<string> solutionSequence;
        public List<MoveOp> moves;

        public Solution(List<string> solutionSequence, List<string> initialState, List<MoveOp> moves) {
            this.solutionSequence = solutionSequence;
            this.initialState = initialState;
            this.moves = moves;
        }
    }

    public struct MoveOp {
        public string value;
        public string direction;

        public MoveOp(string value, string direction) {
            this.value = value;
            this.direction = direction;
        }
    }
}
