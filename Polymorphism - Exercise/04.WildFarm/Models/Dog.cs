using _04.WildFarm.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace _04.WildFarm.Models
{
    public class Dog : Mammal
    {
        private const double weightIncrease = 0.40;
        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }

        public override string ProduceSound()
        {
            return "Woof!";
        }

        public override void FeedAnimal(IFood food)
        {
            if (food.GetType().Name != "Meat" && food.GetType().Name != "meat")
            {
                throw new ArgumentException($"Dog does not eat {food.GetType().Name}!");
            }
            Weight += food.Quantity * weightIncrease;
            FoodEaten += food.Quantity;
        }
    }
}
