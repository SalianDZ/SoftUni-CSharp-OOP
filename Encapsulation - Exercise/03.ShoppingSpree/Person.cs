using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree
{
    public class Person
    {
		private string name;
		private decimal money;
		private List<Product> bagOfProducts;

        public Person(string name, decimal money)
        {
            Money = money;
            Name = name;
            bagOfProducts = new List<Product>();
        }

        public List<Product> BagOfProducts
		{
			get { return bagOfProducts; }
		}


		public decimal Money
		{
			get { return money; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
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

        public void AddProduct(Product product)
        {
            bagOfProducts.Add(product);
            Money -= product.Cost;
        }

	}
}
