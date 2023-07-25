using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private double price;

        protected Cocktail(string name, string size, double price)
        {
            Name = name;
            Size = size;
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

        public string Size { get; private set; }

        public double Price
        {
            get => price;
            private set
            {
                if (Size == "Large")
                {
                    price = value;
                }
                else if (Size == "Middle")
                {
                    price = 2.0 / 3.0 * value;
                }
                else if (Size == "Small")
                {
                    price = 1.0 / 3.0 * value;
                }
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:F2} lv";
        }
    }
}
