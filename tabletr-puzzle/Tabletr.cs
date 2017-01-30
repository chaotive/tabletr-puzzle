using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tabletr_puzzle
{
    public class Tabletr
    {        
        Tabletr(int rows, int columns, List<int> solution) {            
            var length = rows * columns;
            var state = generateState(length);
            
        }

        public static List<int> generateState(int length) {
            var random = new Random(Guid.NewGuid().GetHashCode());
            return Enumerable.Range(1, length).OrderBy(x => random.Next()).ToList();
        }
    }
}
