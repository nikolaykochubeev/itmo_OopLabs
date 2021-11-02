using Shops.Tools;

namespace Shops.Entities
{
    public class ShopProduct : Product
    {
        private decimal _price;
        public ShopProduct(Product product, uint amount, decimal price)
            : base(product.Id, product.Name)
        {
            Amount = amount;
            Price = price;
        }

        public decimal Price
        {
            get => _price;
            private set
            {
                if (value < 0)
                    throw new ShopException("Price less than 0");
                _price = value;
            }
        }

        public uint Amount { get; }
        public ShopProduct ChangePrice(decimal price)
        {
            return new ShopProduct(new Product(Id, Name), Amount, price);
        }

        public ShopProduct ChangeNumber(int number)
        {
            return new ShopProduct(new Product(Id, Name), (uint)(number + Amount), Price);
        }
    }
}