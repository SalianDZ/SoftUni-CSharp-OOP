using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfo
{
    public interface IPerson : IBirthable, IBuyer
    {
        public string Name { get; }
        public int Age { get; }
    }
}
