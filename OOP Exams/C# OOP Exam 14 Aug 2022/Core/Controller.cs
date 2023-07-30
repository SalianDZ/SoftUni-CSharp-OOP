using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;

        public Controller()
        {
            planets = new PlanetRepository();
        }
        public string AddUnit(string unitTypeName, string planetName)
        {
            string result;
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                result = String.Format(ExceptionMessages.UnexistingPlanet, planetName);
                throw new InvalidOperationException(result);
            }

            if (unitTypeName != nameof(AnonymousImpactUnit) &&
                unitTypeName != nameof(SpaceForces) &&
                unitTypeName != nameof(StormTroopers))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
            {
                result = String.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName);
                throw new InvalidOperationException(result);
            }

            if (unitTypeName == "StormTroopers")
            {
                IMilitaryUnit unit = new StormTroopers();
                planet.Spend(unit.Cost);
                planet.AddUnit(unit);
            }
            else if (unitTypeName == "SpaceForces")
            {
                IMilitaryUnit unit = new SpaceForces();
                planet.Spend(unit.Cost);
                planet.AddUnit(unit);
            }
            else
            {
                IMilitaryUnit unit = new AnonymousImpactUnit();
                planet.Spend(unit.Cost);
                planet.AddUnit(unit);
            }

            result = String.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
            return result;
        } // done

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            string result;
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                result = String.Format(ExceptionMessages.UnexistingPlanet, planetName);
                throw new InvalidOperationException(result);
            }

            if (weaponTypeName != nameof(BioChemicalWeapon) &&
                weaponTypeName != nameof(NuclearWeapon) &&
                weaponTypeName != nameof(SpaceMissiles))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            if (planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
            {
                result = String.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName);
                throw new InvalidOperationException(result);
            }

            if (weaponTypeName == "BioChemicalWeapon")
            {
                IWeapon weapon = new BioChemicalWeapon(destructionLevel);
                planet.Spend(weapon.Price);
                planet.AddWeapon(weapon);
            }
            else if (weaponTypeName == "NuclearWeapon")
            {
                IWeapon weapon = new NuclearWeapon(destructionLevel);
                planet.Spend(weapon.Price);
                planet.AddWeapon(weapon);
            }
            else
            {
                IWeapon weapon = new SpaceMissiles(destructionLevel);
                planet.Spend(weapon.Price);
                planet.AddWeapon(weapon);
            }

            result = String.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
            return result;
        } // done

        public string CreatePlanet(string name, double budget)
        {
            string result;
            if (planets.Models.Any(x => x.Name == name))
            {
                result = String.Format(OutputMessages.ExistingPlanet, name);
                throw new ArgumentException(result);
            }
            planets.AddItem(new Planet(name, budget));
            result = String.Format(OutputMessages.NewPlanet, name);
            return result;
        } // done

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in planets.Models.OrderByDescending(x => x.MilitaryPower).ThenBy(x => x.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }
            return sb.ToString().TrimEnd();
        } // done

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = planets.FindByName(planetOne);
            IPlanet secondPlanet = planets.FindByName(planetTwo);

            double firstPlanetHalfBudget = firstPlanet.Budget / 2;
            double secondPlanetHalfBudget = secondPlanet.Budget / 2;

            double firstCalculatedValue = firstPlanet.Army.Sum(x => x.Cost) +
                                            firstPlanet.Weapons.Sum(y => y.Price);

            double secondCalculatedValue = secondPlanet.Army.Sum(x => x.Cost) +
                                            secondPlanet.Weapons.Sum(y => y.Price);

            double firstPowerRatio = firstPlanet.MilitaryPower;
            double secondPowerRatio = secondPlanet.MilitaryPower;

            bool firstHasNuclear = firstPlanet.Weapons
                .Any(w => w.GetType().Name == nameof(NuclearWeapon));

            bool secondHasNuclear = secondPlanet.Weapons
                .Any(w => w.GetType().Name == nameof(NuclearWeapon));

            var firstNuclear = firstPlanet.Weapons
                .FirstOrDefault(w => w.GetType().Name == nameof(NuclearWeapon));
            var secondNuclear = secondPlanet.Weapons
                .FirstOrDefault(w => w.GetType().Name == nameof(NuclearWeapon));

            if (firstPowerRatio > secondPowerRatio)
            {
                firstPlanet.Spend(firstPlanetHalfBudget);
                firstPlanet.Profit(secondPlanetHalfBudget);
                firstPlanet.Profit(secondCalculatedValue);

                planets.RemoveItem(secondPlanet.Name);
                return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
            }
            else if (firstPowerRatio < secondPowerRatio)
            {
                secondPlanet.Spend(secondPlanetHalfBudget);
                secondPlanet.Profit(firstPlanetHalfBudget);
                secondPlanet.Profit(firstCalculatedValue);

                planets.RemoveItem(firstPlanet.Name);
                return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
            }
            else
            {
                if (firstNuclear != null && secondNuclear != null)
                {
                    //if (firstNuclear.DestructionLevel > secondNuclear.DestructionLevel)
                    //{
                    //    firstPlanet.Spend(firstPlanetHalfBudget);
                    //    firstPlanet.Profit(secondPlanetHalfBudget);
                    //    firstPlanet.Profit(secondCalculatedValue);

                    //    planets.RemoveItem(secondPlanet.Name);
                    //    return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
                    //}
                    //else if (firstNuclear.DestructionLevel < secondNuclear.DestructionLevel)
                    //{
                    //    secondPlanet.Spend(secondPlanetHalfBudget);
                    //    secondPlanet.Profit(firstPlanetHalfBudget);
                    //    secondPlanet.Profit(firstCalculatedValue);

                    //    planets.RemoveItem(firstPlanet.Name);
                    //    return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
                    //}
                    //else
                    //{
                    firstPlanet.Spend(firstPlanetHalfBudget);
                    secondPlanet.Spend(secondPlanetHalfBudget);
                    return string.Format(OutputMessages.NoWinner);
                    //}
                }
                else if (firstNuclear != null)
                {
                    firstPlanet.Spend(firstPlanetHalfBudget);
                    firstPlanet.Profit(secondPlanetHalfBudget);
                    firstPlanet.Profit(secondCalculatedValue);

                    planets.RemoveItem(secondPlanet.Name);
                    return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
                }
                else if (secondNuclear != null)
                {
                    secondPlanet.Spend(secondPlanetHalfBudget);
                    secondPlanet.Profit(firstPlanetHalfBudget);
                    secondPlanet.Profit(firstCalculatedValue);

                    planets.RemoveItem(firstPlanet.Name);
                    return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
                }
                else
                {
                    firstPlanet.Spend(firstPlanetHalfBudget);
                    secondPlanet.Spend(secondPlanetHalfBudget);
                    return string.Format(OutputMessages.NoWinner);
                }
            }
        }
        // done

        public string SpecializeForces(string planetName)
        {
            string result;
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                result = String.Format(ExceptionMessages.UnexistingPlanet, planetName);
                throw new InvalidOperationException(result);
            }

            if (planet.Army.Count == 0)
            {
                result = String.Format(ExceptionMessages.NoUnitsFound);
                throw new InvalidOperationException(result);
            }

            planet.TrainArmy();
            planet.Spend(1.25);
            result = String.Format(OutputMessages.ForcesUpgraded, planetName);
            return result;
        }  // done
    }
}
