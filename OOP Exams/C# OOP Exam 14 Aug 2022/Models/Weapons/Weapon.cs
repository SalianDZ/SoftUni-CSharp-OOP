using PlanetWars.Models.Weapons.Contracts;
using System;
using PlanetWars.Utilities.Messages;

namespace PlanetWars.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private int destructionLevel;

        protected Weapon(int destructionLevel, double price)
        {
            DestructionLevel = destructionLevel;
            Price = price;
        }

        public double Price { get; private set; }

        public int DestructionLevel
        {
            get { return destructionLevel; }
            private set
            {
                if (value < 1)
                {
                    string result = String.Format(ExceptionMessages.TooLowDestructionLevel);
                    throw new ArgumentException(result);
                }
                else if (value > 10)
                {
                    string result = String.Format(ExceptionMessages.TooHighDestructionLevel);
                    throw new ArgumentException(result);
                }
                destructionLevel = value;
            }
        }
    }
}
