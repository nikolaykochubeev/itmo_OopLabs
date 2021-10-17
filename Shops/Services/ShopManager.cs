using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private readonly List<Shop> _shops;

        public ShopManager(List<Shop> shops = null)
        {
            _shops = shops ?? new List<Shop>();
        }

        public IReadOnlyList<Shop> Shops => _shops;
        public Shop RegisterShop(Shop shop)
        {
            if (shop is null)
                throw new ShopException("Shop cannot be the null");

            if (_shops.FirstOrDefault(shopM => shopM.Id == shop.Id) is not null)
                throw new ShopException("Shop already registered");

            _shops.Add(shop);
            return _shops.Last();
        }

        public Shop FindShop(Guid id)
        {
            return _shops.FirstOrDefault(shop => shop.Id == id);
        }

        public Shop FindShopWithCheapestProduct(Guid productId, uint amount)
        {
            return _shops.Where(shop => shop.FindProduct(productId) is not null)
                         .Where(shop => shop.FindProduct(productId).Amount >= amount)
                         .OrderBy(shop => shop.FindProduct(productId).Price).First();
        }
    }
}