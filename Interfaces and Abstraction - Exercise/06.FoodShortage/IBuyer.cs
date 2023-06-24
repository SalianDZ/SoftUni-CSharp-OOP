using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfo
{
    public interface IBuyer
    {
        public int Food { get;}
        int BuyFood();
    }
}
