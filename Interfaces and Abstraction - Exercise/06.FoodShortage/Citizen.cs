using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfo
{
    public class Citizen : IPerson
    {
        public Citizen(string name, int age, string id, string birthday)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthdate = birthday;
            Food = 0;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Id { get; set; }

        public string Birthdate { get; set; }

        public int Food { get; set; }

        public int BuyFood()
        {
            Food += 10;
            return 10;
        }
    }
}
