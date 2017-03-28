using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tabletr_puzzle
{
    public struct MoveOp {
        public string value;
        public string direction;

        public MoveOp(string value, string direction) {
            this.value = value;
            this.direction = direction;
        }
    }
}
