using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Coffee : HotBeverage
    {
        private const decimal coffeePrice = 3.50m;
        private const double coffeeMilliliters = 50;
        public Coffee(string name, double caffeine) : base(name, coffeePrice, coffeeMilliliters)
        {
            Caffeine = caffeine;
        }
        public double Caffeine { get; set; }
    }
}