using _04.WildFarm.Food;
using _04.WildFarm.Models;

namespace _04.WildFarm
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Animal> animals = new();
            while (true)
            {
                string[] animalTokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (animalTokens[0] == "End")
                {
                    break;
                }

                string[] foodTokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string foodType = foodTokens[0];
                int foodQuantity = int.Parse(foodTokens[1]);
                IFood food;

                if (foodType == "Vegetable")
                {
                    food = new Vegetable(foodQuantity);
                }
                else if (foodType == "Fruit")
                {
                    food = new Fruit(foodQuantity);
                }
                else if (foodType == "Meat")
                {
                    food = new Meat(foodQuantity);
                }
                else
                {
                    food = new Seeds(foodQuantity);
                }

                string type = animalTokens[0];
                string name = animalTokens[1];
                double weight = double.Parse(animalTokens[2]);

                
                try
                {
                    if (type == "Cat")
                    {
                        string livingRegion = animalTokens[3];
                        string breed = animalTokens[4];
                        Cat cat = new(name, weight, livingRegion, breed);
                        animals.Add(cat);
                        Console.WriteLine(cat.ProduceSound());
                        cat.FeedAnimal(food);
                    }
                    else if (type == "Tiger")
                    {
                        string livingRegion = animalTokens[3];
                        string breed = animalTokens[4];
                        Tiger tiger = new(name, weight, livingRegion, breed);
                        animals.Add(tiger);
                        Console.WriteLine(tiger.ProduceSound());
                        tiger.FeedAnimal(food);
                    }
                    else if (type == "Hen")
                    {
                        double wingSize = double.Parse(animalTokens[3]);
                        Hen hen = new(name, weight, wingSize);
                        animals.Add(hen);
                        Console.WriteLine(hen.ProduceSound());
                        hen.FeedAnimal(food);
                    }
                    else if (type == "Owl")
                    {
                        double wingSize = double.Parse(animalTokens[3]);
                        Owl owl = new(name, weight, wingSize);
                        animals.Add(owl);
                        Console.WriteLine(owl.ProduceSound());
                        owl.FeedAnimal(food);
                    }
                    else if (type == "Mouse")
                    {
                        string livingRegion = animalTokens[3];
                        Mouse mouse = new(name, weight, livingRegion);
                        animals.Add(mouse);
                        Console.WriteLine(mouse.ProduceSound());
                        mouse.FeedAnimal(food);
                    }
                    else if (type == "Dog")
                    {
                        string livingRegion = animalTokens[3];
                        Dog dog = new(name, weight, livingRegion);
                        animals.Add(dog);
                        Console.WriteLine(dog.ProduceSound());
                        dog.FeedAnimal(food);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }
        }
    }
}