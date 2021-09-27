using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        private float _money;
        public Customer(string name, float money)
        {
            Name = name;
            Money = money;
        }

        public string Name { get; }

        public float Money
        {
            get => _money;
            set
            {
                if (value < 0)
                    throw new ShopException("Money value less than 0");
                _money = value;
            }
        }

        public List<CustomerProduct> Products { get; set; } = new ();

        public void AddProducts(IEnumerable<CustomerProduct> products)
        {
            Products.AddRange(products);
        }
    }
}