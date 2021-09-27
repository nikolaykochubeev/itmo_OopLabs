using Shops.Tools;

namespace Shops.Entities
{
    public class ShopProduct : Product
    {
        private float _price;
        public ShopProduct(Product product, uint number, float price)
            : base(product.Name, product.Id)
        {
            Number = number;
            Price = price;
        }

        public float Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ShopException("Price less than 0");
                _price = value;
            }
        }

        public uint Number { get; set; }
    }
}