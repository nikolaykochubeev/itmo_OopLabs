using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        private decimal _money;
        public Customer(string name, decimal money)
        {
            Name = name;
            Money = money;
        }

        private Customer(Customer customer, decimal money, List<CustomerProduct> products)
            : this(customer.Name, money)
        {
            Products = products;
        }

        public string Name { get; }

        public decimal Money
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

        public Customer ChangeMoneyValue(decimal money)
        {
            return new Customer(this, money, Products);
        }
    }
}