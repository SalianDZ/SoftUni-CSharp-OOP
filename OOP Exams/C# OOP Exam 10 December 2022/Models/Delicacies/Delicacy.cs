using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy : IDelicacy
    {
        private string name;

        protected Delicacy(string name, double price) // This can be a problem
        {
            Name = name;
            Price = price;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string result = string.Format(ExceptionMessages.NameNullOrWhitespace);
                    throw new ArgumentException(result);
                }
                name = value;
            }
        }

        public double Price { get; private set; }

        public override string ToString()
        {
            return $"{Name} - {Price:F2} lv";
        }
    }
}
