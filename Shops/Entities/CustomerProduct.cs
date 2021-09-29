namespace Shops.Entities
{
    public class CustomerProduct : Product
    {
        public CustomerProduct(Product product, uint numberOfProducts)
            : base(product.Name, product.Id)
        {
            NumberOfProducts = numberOfProducts;
        }

        public uint NumberOfProducts { get; }
    }
}