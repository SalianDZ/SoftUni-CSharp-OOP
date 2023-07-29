using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
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
        private double militaryPower;
        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            MilitaryPower = MilitaryPower;
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

        public double MilitaryPower
        {
            get => militaryPower;
            private set
            {
                militaryPower = CalculateMilitaryPower();
            }
        }

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
            if (Army.Count <= 0)
            {
                sb.AppendLine("No units");
            }
            else
            {
                sb.AppendLine(String.Join(", ", Army));
            }

            if (Weapons.Count <= 0)
            {
                sb.AppendLine("No weapons");
            }
            else
            {
                sb.AppendLine(String.Join(", ", Weapons));
            }
            sb.AppendLine($"--Military Power: {MilitaryPower}");
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
            double totalSum = 0;
            foreach (var unit in units.Models)
            {
                totalSum += unit.EnduranceLevel;
            }

            foreach (var weapon in weapons.Models)
            {
                totalSum += weapon.DestructionLevel;
            }

            if (units.Models.Any(x => x.GetType().Name == "AnonymousImpactUnit"))
            {
                double increasedAmount = totalSum * 0.30;
                totalSum += increasedAmount;
            }

            if (weapons.Models.Any(x => x.GetType().Name == "NuclearWeapon"))
            {
                double increasedAmount = totalSum * 0.45;
                totalSum += increasedAmount;
            }

            return Math.Round(totalSum, 3);
        }
    }
}
