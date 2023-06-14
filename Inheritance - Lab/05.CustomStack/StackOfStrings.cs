using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty()
        {
            if (Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Stack<string> AddRange(IEnumerable<string> range)
        {
            Stack<string> result = new();
            foreach (var item in range)
            {
                result.Push(item);
            }
            return result;
        }
    }
}
