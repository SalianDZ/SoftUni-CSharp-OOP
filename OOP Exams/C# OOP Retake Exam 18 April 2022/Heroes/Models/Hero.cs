using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;

namespace Heroes.Models
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        protected Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.HeroNameNull));
                }
                name = value;
            }
        }

        public int Health
        { 
            get => health;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.HeroHealthBelowZero));
                }
                health = value;
            }
        }

        public int Armour
        {
            get => armour;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.HeroArmourBelowZero));
                }
                armour = value;
            }
        }

        public IWeapon Weapon
        {
            get => weapon;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.WeaponNull));
                }
                weapon = value;
            }
        }

        public bool IsAlive => Health > 0;

        public void AddWeapon(IWeapon weapon) 
        {
            Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            int leftoverPoints = 0;

            armour -= points;
            if (armour <= 0)
            {
                leftoverPoints = Math.Abs(Armour);
                armour = 0;
            }

            if (leftoverPoints > 0)
            {
                health -= leftoverPoints;

                if (health < 0)
                {
                    health = 0;
                }
            }
        }
    }
}
