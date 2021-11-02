using System;
using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        public Shop RegisterShop(Shop shop);
        public Shop FindShop(Guid id);
        public Shop FindShopWithCheapestProduct(Guid productId, uint amount);
    }
}