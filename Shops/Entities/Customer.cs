using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        private double _money;
        public Customer(string name, double money)
        {
            Name = name;
            Money = money;
        }

        public Customer(Customer customer, List<CustomerProduct> products)
            : this(customer.Name, customer.Money)
        {
            Products = products;
        }

        public string Name { get; }

        public double Money
        {
            get => _money;
            private set
            {
                if (value < 0)
                    throw new ShopException("Money value less than 0");
                _money = value;
            }
        }

        public List<CustomerProduct> Products { get; } = new ();

        public void AddProducts(IEnumerable<CustomerProduct> products)
        {
            Products.AddRange(products);
        }

        public Customer ChangeMoneyValue(double money)
        {
            return new Customer(this, Products);
        }
    }
}