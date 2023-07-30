using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private UnitRepository units;
        private WeaponRepository weapons;
        private string name;
        private double budget;
        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            units = new UnitRepository();
            weapons = new WeaponRepository();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    string result = String.Format(ExceptionMessages.InvalidPlanetName);
                    throw new ArgumentException(result);
                }
                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    string result = String.Format(ExceptionMessages.InvalidBudgetAmount);
                    throw new ArgumentException(result);
                }
                budget = value;
            }
        }

        public double MilitaryPower => Math.Round(this.CalculateMilitaryPower(), 3);


        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            sb.Append("--Forces: ");
            if (Army.Count <= 0)
            {
                sb.AppendLine("No units");
            }
            else
            {
                int counter = 0;
                foreach (var force in units.Models)
                {
                    if (counter == units.Models.Count - 1)
                    {
                        sb.AppendLine(force.GetType().Name);
                    }
                    else
                    {
                        sb.Append($"{force.GetType().Name}, ");
                    }
                    counter++;
                }
            }

            sb.Append("--Combat equipment: ");
            if (Weapons.Count <= 0)
            {
                sb.AppendLine("No weapons");
            }
            else
            {
                int counter = 0;
                foreach (var weapon in weapons.Models)
                {
                    if (counter == weapons.Models.Count - 1)
                    {
                        sb.AppendLine(weapon.GetType().Name);
                    }
                    else
                    {
                        sb.Append($"{weapon.GetType().Name}, ");
                    }
                    counter++;
                }
            }
            sb.Append($"--Military Power: {MilitaryPower}");
            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                string result = String.Format(ExceptionMessages.UnsufficientBudget);
                throw new InvalidOperationException(result);
            }

            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var currentUnit in units.Models)
            {
                currentUnit.IncreaseEndurance();
            }
        }

        private double CalculateMilitaryPower()
        {
            double result = this.units.Models.Sum(x => x.EnduranceLevel) + this.weapons.Models.Sum(x => x.DestructionLevel);

            if (this.units.Models.Any(x => x.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                result *= 1.3;
            }
            if (this.weapons.Models.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
            {
                result *= 1.45;
            }

            return result;
        }
    }
}
