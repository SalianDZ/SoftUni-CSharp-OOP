using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public abstract class Car : ICar
    {

        protected Car(string model, string color)
        {
            Model = model;
            Color = color;
        }
        public string Model { get;}

        public string Color { get;}

        public string Start()
        {
            return "Engine start";
        }

        public string Stop()
        {
            return "Breaaak!";
        }
    }
}
