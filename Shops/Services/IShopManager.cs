using System;
using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        public Shop AddShop(string name, string address);
        public ShopProduct AddProductToShop(Guid shopId, ShopProduct shopProduct);
        public ShopProduct AddProductToShop(Guid shopId, Guid productId, uint number, double price);
        public void SupplyToShop(Guid shopId, List<ShopProduct> products);
        public Customer BuyInShop(Guid shopId, Customer customer);
        public Shop FindShop(Guid id);
        public Product RegisterProduct(string name);
        public void ChangePrice(Guid shopId, Guid productId, double newPrice);
        public Shop FindShopWithCheapestProduct(Guid productId, uint number);
    }
}