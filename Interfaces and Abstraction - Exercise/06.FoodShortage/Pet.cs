using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfo
{
    public class Pet : IBirthable
    {
        public Pet(string name, string birthdate)
        {
            Name = name;
            Birthdate = birthdate;
        }

        public string Birthdate { get; set; }

        public string Name { get; set; }
    }
}
