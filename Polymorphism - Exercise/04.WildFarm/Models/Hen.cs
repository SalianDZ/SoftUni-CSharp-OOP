using _04.WildFarm.Food;

namespace _04.WildFarm.Models
{
    public class Hen : Bird
    {
        private const double weightIncrease = 0.35;
        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        public override string ProduceSound()
        {
            return "Cluck";
        }

        public override void FeedAnimal(IFood food)
        {
            Weight += weightIncrease * food.Quantity;
            FoodEaten += food.Quantity;
        }
    }
}
