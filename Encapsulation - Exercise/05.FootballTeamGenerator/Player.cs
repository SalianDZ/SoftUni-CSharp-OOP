using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Player
    {
		private string name;
		private Stats playerStats;
		private double skillLevel;

		public Player(string name, Stats playerStats)
		{
			PlayerStats = playerStats;
			Name = name;
			SkillLevel = SkillLevel;
		}

		public double SkillLevel
        {
			get { return skillLevel; }
			set
			{
				skillLevel = playerStats.Endurance + playerStats.Sprint +
					playerStats.Dribble + playerStats.Passing + playerStats.Shooting;

				skillLevel /= 5;
			}
		}


		public Stats PlayerStats
		{
			get { return playerStats; }
			set { playerStats = value; }
		}


		public string Name
		{
			get { return name; }
			set
			{
				if (string.IsNullOrEmpty(value) || value == " ")
				{
					throw new ArgumentException("A name should not be empty.");
				}
				name = value;
			}
		}

	}
}
