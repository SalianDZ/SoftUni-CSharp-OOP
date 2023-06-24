namespace PersonInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List <IIdentifiable> citizens = new();
            IIdentifiable currentCitizen;

            while (true)
            {
                string[] tokens = Console.ReadLine().Split();

                if (tokens[0] == "End")
                {
                    break;
                }
                else if (tokens.Length == 2)
                {
                    currentCitizen = new Robot(tokens[0],tokens[1]);
                    citizens.Add(currentCitizen);
                }
                else if (tokens.Length == 3)
                {
                    currentCitizen = new Citizen(tokens[0], int.Parse(tokens[1]) ,tokens[2]);
                    citizens.Add (currentCitizen);
                }
            }
            string lastDigits = Console.ReadLine();

            foreach (var creature in citizens.Where(x => x.Id.EndsWith(lastDigits)))
            {
                Console.WriteLine(creature.Id);
            }
        }
    }
}