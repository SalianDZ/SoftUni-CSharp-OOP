using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Cake : Dessert
    {
        private const decimal cakePrice = 5m;
        private const double cakeGrams = 250;
        private const double cakeCalories = 1000;
        public Cake(string name) : base(name, cakePrice, cakeGrams, cakeCalories)
        {
        }
    }
}
