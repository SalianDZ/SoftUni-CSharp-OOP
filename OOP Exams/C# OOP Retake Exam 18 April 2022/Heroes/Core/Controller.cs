using Heroes.Core.Contracts;
using Heroes.Models;
using Heroes.Models.Contracts;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository weapons;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = heroes.FindByName(heroName);

            if (hero == null)
            {
                throw new InvalidOperationException(String.Format(OutputMessages.HeroDoesNotExist, heroName));
            }

            IWeapon weapon = weapons.FindByName(weaponName);

            if (weapon == null)
            {
                throw new InvalidOperationException(String.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException(String.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            }
            hero.AddWeapon(weapon);
            weapons.Remove(weapon);
            return String.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());
        } // done

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.Models.Any(x => x.Name == name))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.HeroAlreadyExist, name));
            }

            IHero hero;

            if (type == "Barbarian")
            {
                hero = new Barbarian(name, health, armour);
                heroes.Add(hero);
                return String.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
            }
            else if (type == "Knight")
            {
                hero = new Knight(name, health, armour);
                heroes.Add(hero);
                return String.Format(OutputMessages.SuccessfullyAddedKnight, name);
            }
            else
            {
                throw new InvalidOperationException(String.Format(OutputMessages.HeroTypeIsInvalid));
            }
        } // done

        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.Models.Any(x => x.Name == name))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.WeaponAlreadyExists, name));
            }

            IWeapon weapon;

            if (type == "Claymore")
            {
                weapon = new Claymore(name, durability);
                weapons.Add(weapon);
                return String.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
            }
            else if (type == "Mace")
            {
                weapon = new Mace(name, durability);
                weapons.Add(weapon);
                return String.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
            }
            else
            {
                throw new InvalidOperationException(String.Format(OutputMessages.WeaponTypeIsInvalid));
            }
        } //done

        public string HeroReport()
        {
            var orderedHeroes = heroes.Models.OrderBy(h => h.GetType().Name)
                .ThenByDescending(h => h.Health)
                .ThenBy(h => h.Name);

            StringBuilder sb = new StringBuilder();

            foreach (var hero in orderedHeroes)
            {
                string weaponName = string.Empty;

                weaponName = hero.Weapon == null
                    ? "Unarmed"
                    : hero.Weapon.Name;

                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                sb.AppendLine($"--Weapon: {weaponName}");
            }

            return sb.ToString().TrimEnd();
        } // done

        public string StartBattle()
        {
            Map map = new Map();

            List<IHero> participants = heroes.Models.Where(h => h.IsAlive && h.Weapon != null).ToList();
            return map.Fight(participants);
        }
    }
}
