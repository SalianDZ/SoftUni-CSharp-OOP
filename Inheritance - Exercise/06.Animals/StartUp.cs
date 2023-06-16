using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            bool wrongData = false;
            List<Animal> animals = new List<Animal>();
            while (true)
            {
                string animal = Console.ReadLine();
                if (animal == "Beast!")
                {
                    break;
                }
                string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (animal == "Cat")
                {
                    Cat cat = new(tokens[0], int.Parse(tokens[1]), tokens[2]);
                    if (cat.Name == "" || cat.Name == " " || cat.Gender == "" ||cat.Gender == " " || cat.Age < 0)
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }
                    animals.Add(cat);
                }
                else if (animal == "Dog")
                {
                    Dog dog = new(tokens[0], int.Parse(tokens[1]), tokens[2]);
                    if (dog.Name == "" || dog.Name == " " || dog.Gender == "" || dog.Gender == " " || dog.Age < 0)
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }
                    animals.Add(dog);
                }
                else if (animal == "Frog")
                {
                    Frog frog = new(tokens[0], int.Parse(tokens[1]), tokens[2]);
                    if (frog.Name == "" || frog.Name == " " || frog.Gender == "" || frog.Gender == " " || frog.Age < 0)
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }
                    animals.Add(frog);
                }
                else if (animal == "Tomcat")
                {
                    Tomcat cat = new(tokens[0], int.Parse(tokens[1]));
                    if (cat.Name == "" || cat.Name == " " || cat.Gender == "" || cat.Gender == " " || cat.Age < 0)
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }
                    animals.Add(cat);
                }
                else if (animal == "Kitten" || animal == "Kittens")
                {
                    Tomcat cat = new(tokens[0], int.Parse(tokens[1]));
                    if (cat.Name == "" || cat.Name == " " || cat.Gender == "" || cat.Gender == " " || cat.Age < 0)
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }
                    animals.Add(cat);
                }
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
