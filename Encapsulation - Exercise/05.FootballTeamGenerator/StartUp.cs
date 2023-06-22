using System.Security.Cryptography.X509Certificates;

namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teams = new();

            while (true)
            {
                try
                {
                    string[] command = Console.ReadLine().Split(";");
                    if (command[0] == "END")
                    {
                        break;
                    }
                    else if (command[0] == "Add")
                    {
                        string currentTeamName = command[1];

                        if (!teams.Any(x => x.Name == currentTeamName))
                        {
                            throw new ArgumentException($"Team {currentTeamName} does not exist.");
                        }

                        string playerName = command[2];
                        Stats stats = new(int.Parse(command[3]), int.Parse(command[4]), int.Parse(command[5]), int.Parse(command[6]), int.Parse(command[7]));
                        Player player = new(playerName, stats);
                        teams.FirstOrDefault(x => x.Name == currentTeamName).AddPlayer(player);
                    }
                    else if (command[0] == "Remove")
                    {
                        string currentTeamName = command[1];

                        if (!teams.Any(x => x.Name == currentTeamName))
                        {
                            throw new ArgumentException($"Team {currentTeamName} does not exist.");
                        }
                        string playerName = command[2];
                        teams.FirstOrDefault(x => x.Name == currentTeamName).RemovePlayer(playerName);
                    }
                    else if (command[0] == "Rating")
                    {
                        string currentTeamName = command[1];

                        if (!teams.Any(x => x.Name == currentTeamName))
                        {
                            throw new ArgumentException($"Team {currentTeamName} does not exist.");
                        }

                        Console.WriteLine(teams.FirstOrDefault(x => x.Name == currentTeamName).ShowStats());
                    }
                    else if (command[0] == "Team")
                    {
                        string teamName = command[1];
                        Team team = new(teamName);
                        teams.Add(team);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}