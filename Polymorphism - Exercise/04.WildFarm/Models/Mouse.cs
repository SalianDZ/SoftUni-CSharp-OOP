using _04.WildFarm.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Mouse : Mammal
    {
        private const double weightIncrease = 0.10;
        public Mouse(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }

        public override string ProduceSound()
        {
            return "Squeak";
        }

        public override void FeedAnimal(IFood food)
        {
            if (food.GetType().Name != "Vegetable" && food.GetType().Name != "vegetable" && food.GetType().Name != "Fruit" && food.GetType().Name != "fruit")
            {
                throw new ArgumentException($"Mouse does not eat {food.GetType().Name}!");
            }
            Weight += food.Quantity * weightIncrease;
            FoodEaten += food.Quantity;
        }
    }
}
