using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Team
    {
		private string name;
		private double rating;
		private List<Player> players;

		public Team(string name)
		{
			Name = name;
			players = new List<Player>();
		}

		public IReadOnlyList<Player> Players
		{
			get { return players.AsReadOnly(); }
		}


		public double Rating
		{
			get { return rating; }
			private set 
			{
				rating = players.Sum(x => x.SkillLevel);
			}
		}


		public string Name
		{
			get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value) || value == " ")
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }

		public void AddPlayer(Player player)
		{
			players.Add(player);
		}

		public void RemovePlayer(string name)
		{
			if (players.Any(x => x.Name == name))
			{
				players.Remove(players.FirstOrDefault(x => x.Name == name));
			}
			else
			{
				throw new ArgumentException($"Player {name} is not in {Name} team.");
			}
		}

		public string ShowStats()
		{
			Rating = Rating;
			return $"{Name} - {Math.Round(Rating)}";
		}
	}
}
