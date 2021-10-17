namespace Shops.Entities
{
    public class CustomerProduct : Product
    {
        public CustomerProduct(Product product, uint numberOfProducts)
            : base(product.Id, product.Name)
        {
            NumberOfProducts = numberOfProducts;
        }

        public uint NumberOfProducts { get; }
    }
}