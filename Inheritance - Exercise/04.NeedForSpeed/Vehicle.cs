using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class Vehicle
    {
        private const double defaultFuelConsupmtion = 1.25;
        public Vehicle(int horsePower, double fuel)
        {
            Fuel = fuel;
            HorsePower = horsePower;
        }
        public virtual double FuelConsumption => defaultFuelConsupmtion;
        public double Fuel { get; set; }
        public int HorsePower { get; set; }

        public virtual void Drive(double kilometers)
        {
            double consumedFuel = kilometers * FuelConsumption;
            Fuel -= consumedFuel;
        }
    }
}
