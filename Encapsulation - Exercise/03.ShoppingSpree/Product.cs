using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree
{
    public class Product
    {
		private string name;
		private decimal cost;

		public Product(string name, decimal cost)
		{
			Cost = cost;
			Name = name;
		}

		public decimal Cost
		{
			get { return cost; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Money cannot be negative");
				}
				cost = value;
			}
		}


		public string Name
		{
			get { return name; }
			set 
			{
				if (value is null || value == "" || value == " " || value == string.Empty)
				{
                    throw new ArgumentException("Name cannot be empty");
                }
				name = value; 
			}
		}

		public override string ToString()
		{
			return $"{Name}";
		}
	}
}
