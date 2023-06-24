using System.Xml;

namespace PersonInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            List<IPerson> people = new();
            IPerson person;

            for (int i = 0; i < count; i++)
            {
                string[] tokens = Console.ReadLine().Split();

                if (tokens.Length == 3)
                {
                    person = new Rebel(tokens[0], int.Parse(tokens[1]), tokens[2]);
                    people.Add(person);
                }
                else if (tokens.Length == 4)
                {
                    person = new Citizen(tokens[0], int.Parse(tokens[1]), tokens[2], tokens[3]);
                    people.Add(person);
                }
            }

            int foodCounter = 0;

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End")
                {
                    break;
                }

                if (people.Any(x => x.Name == command))
                {
                    foodCounter += people.FirstOrDefault(x => x.Name == command).BuyFood();
                }
            }

            Console.WriteLine(foodCounter);
        }
    }
}