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

        private Customer(Customer customer, decimal money)
            : this(customer.Name, money)
        {
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

        public Customer ChangeMoneyValue(decimal money)
        {
            return new Customer(this, money);
        }
    }
}