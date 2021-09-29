using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        public Shop(string shopName, string address, Guid id)
        {
            ShopName = shopName;
            Id = id;
            Address = address;
        }

        public Dictionary<Guid, ShopProduct> Products { get; } = new ();
        public Guid Id { get; }
        public string ShopName { get; }
        public string Address { get; }
        public ShopProduct AddProduct(ShopProduct shopProduct)
        {
            Products.Add(shopProduct.Id, shopProduct);
            return Products[shopProduct.Id];
        }

        public ShopProduct FindProduct(Guid id)
        {
            return Products.ContainsKey(id) ? Products[id] : null;
        }
    }
}
