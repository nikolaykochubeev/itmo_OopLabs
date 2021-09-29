using Shops.Tools;

namespace Shops.Entities
{
    public class ShopProduct : Product
    {
        private double _price;
        public ShopProduct(Product product, uint number, double price)
            : base(product.Name, product.Id)
        {
            Number = number;
            Price = price;
        }

        public double Price
        {
            get => _price;
            private set
            {
                if (value < 0)
                    throw new ShopException("Price less than 0");
                _price = value;
            }
        }

        public uint Number { get; }
        public ShopProduct ChangePrice(double price)
        {
            return new ShopProduct(new Product(Name, Id), Number, price);
        }

        public ShopProduct ChangeNumber(int number)
        {
            return new ShopProduct(new Product(Name, Id), (uint)(number + Number), Price);
        }
    }
}