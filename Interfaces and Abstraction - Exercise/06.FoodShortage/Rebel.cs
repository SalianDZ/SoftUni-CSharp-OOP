using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfo
{
    public class Rebel : IPerson
    {
        public Rebel(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
            Food = 0;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }
        public int Food { get; set; }

        public string Birthdate { get; }

        public int BuyFood()
        {
            Food += 5;
            return 5;
        }
    }
}
