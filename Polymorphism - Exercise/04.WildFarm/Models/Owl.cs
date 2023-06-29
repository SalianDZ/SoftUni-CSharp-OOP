using _04.WildFarm.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Owl : Bird
    {
        private const double weightIncrease = 0.25;
        public Owl(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        public override string ProduceSound()
        {
            return "Hoot Hoot";
        }

        public override void FeedAnimal(IFood food)
        {
            if (food.GetType().Name != "Meat" && food.GetType().Name != "meat")
            {
                throw new ArgumentException($"Owl does not eat {food.GetType().Name}!");
            }
            Weight += food.Quantity * weightIncrease;
            FoodEaten += food.Quantity;
        }
    }
}
