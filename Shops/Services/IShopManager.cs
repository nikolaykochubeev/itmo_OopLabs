using System;
using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        public Shop AddShop(string name, string address);
        public Product AddProductToShop(Shop shop, Product product, uint number, float price);
        public void SupplyToShop(Shop shop, Dictionary<Guid, Tuple<Product, uint, float>> products);
        public void SupplyToShop(Shop shop, Dictionary<Guid, ShopProduct> products);
        public Customer BuyInShop(Shop shop, Customer customer);
        public Shop FindShop(Guid id);
        public Product RegisterProduct(string name);
        public void ChangePrice(Shop shop, Product product, float newPrice);
        public Shop FindShopWithCheapestProduct(Product product, uint number);
    }
}