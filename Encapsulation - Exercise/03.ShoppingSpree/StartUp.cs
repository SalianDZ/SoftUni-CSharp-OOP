using System.Text;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new();
            List<Product> products = new();
            string[] peopleTokens = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < peopleTokens.Length; i++)
            {
                string currentToken = peopleTokens[i];
                string[] internalTokens = currentToken.Split("=");
                try
                {
                    Person person = new(internalTokens[0], int.Parse(internalTokens[1]));
                    people.Add(person);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            string[] productTokens = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < productTokens.Length; i++)
            {
                string currentToken = productTokens[i];
                string[] internalTokens = currentToken.Split("=");
                try
                {
                    Product product = new(internalTokens[0], int.Parse(internalTokens[1]));
                    products.Add(product);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            while (true)
            {
                string[] command = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (command[0] == "END")
                {
                    break;
                }

                string person = command[0];
                string product = command[1];

                if (people.Any(x => x.Name == person) && products.Any(x => x.Name == product))
                {
                    if (people.FirstOrDefault(x => x.Name == person).Money >= products.FirstOrDefault(x => x.Name == product).Cost)
                    {
                        Console.WriteLine($"{person} bought {product}");
                        people.FirstOrDefault(x => x.Name == person).AddProduct(products.FirstOrDefault(x => x.Name == product));
                    }
                    else
                    {
                        Console.WriteLine($"{person} can't afford {product}");
                    }
                }
            }

            foreach (var person in people)
            {
                if (person.BagOfProducts.Count == 0)
                {
                    Console.WriteLine($"{person.Name} - Nothing bought");
                }
                else
                {
                    string result = string.Empty;
                    int counter = 0;
                    foreach (var product in person.BagOfProducts)
                    {
                        if (counter == person.BagOfProducts.Count - 1)
                        {
                            result += $"{product.Name}";
                            break;
                        }
                        counter++;
                        result += $"{product.Name}, ";
                    }
                    Console.WriteLine($"{person.Name} - {result}");
                }
            }
        }
    }
}