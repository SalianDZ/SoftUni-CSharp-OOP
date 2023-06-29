using _04.WildFarm.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Cat : Feline
    {
        private const double weightIncrease = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {
        }

        public override string ProduceSound()
        {
            return "Meow";
        }

        public override void FeedAnimal(IFood food)
        {
            if (food.GetType().Name != "Vegetable" && food.GetType().Name != "vegetable" && food.GetType().Name != "Meat" && food.GetType().Name != "meat")
            {
                throw new ArgumentException($"Cat does not eat {food.GetType().Name}!");
            }
            Weight += weightIncrease * food.Quantity;
            FoodEaten += food.Quantity;
        }
    }
}
 