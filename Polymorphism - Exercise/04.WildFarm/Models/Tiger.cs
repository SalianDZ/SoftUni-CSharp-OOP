using _04.WildFarm.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Tiger : Feline
    {
        private const double weightIncrease = 1.00;
        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {
        }

        public override string ProduceSound()
        {
            return "ROAR!!!";
        }

        public override void FeedAnimal(IFood food)
        {
            if (food.GetType().Name != "Meat" && food.GetType().Name != "meat")
            {
                throw new ArgumentException($"Tiger does not eat {food.GetType().Name}!");
            }
            Weight += food.Quantity * weightIncrease;
            FoodEaten += food.Quantity;
        }
    }
}
