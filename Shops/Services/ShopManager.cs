using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private readonly Dictionary<Guid, Shop> _shops = new ();
        private readonly Dictionary<Guid, Product> _products = new ();

        public ShopManager()
        {
        }

        public Shop AddShop(string name, string address)
        {
            var guid = Guid.NewGuid();
            return _shops[guid] = new Shop(name, address, guid);
        }

        public ShopProduct AddProductToShop(Guid shopId, ShopProduct shopProduct)
        {
            return !_products.ContainsKey(shopProduct.Id)
                ? throw new ShopException("This product is not register in ShopManager")
                : _shops[shopId].AddProduct(shopProduct);
        }

        public ShopProduct AddProductToShop(Guid shopId, Guid productId, uint number, double price)
        {
            return !_products.ContainsKey(productId)
               ? throw new ShopException("This product is not register in ShopManager")
               : _shops[shopId].AddProduct(new ShopProduct(_products[productId], number, price));
        }

        public void SupplyToShop(Guid shopId, List<ShopProduct> products)
        {
            foreach (ShopProduct shopProduct in products)
            {
                if (!_products.ContainsKey(shopProduct.Id))
                    throw new ShopException("Some product from the supply is not registered in ShopManager");
                if (_shops[shopId].FindProduct(shopProduct.Id) == null)
                    _shops[shopId].AddProduct(shopProduct);
                else
                    _shops[shopId].FindProduct(shopProduct.Id).ChangeNumber((int)shopProduct.Number);
            }
        }

        public Customer BuyInShop(Guid shopId, Customer customer)
        {
            foreach (CustomerProduct product in customer.Products)
            {
                if (!_products.ContainsKey(product.Id))
                    throw new ShopException("Some product from the supply is not registered in ShopManager");
                if (customer.Money < _shops[shopId].FindProduct(product.Id).Price * product.NumberOfProducts)
                    throw new ShopException("Customer hasn't got enough money");
                if (product.NumberOfProducts > _shops[shopId].FindProduct(product.Id).Number)
                    throw new ShopException("Shop hasn't got enough products");
                _shops[shopId].FindProduct(product.Id).ChangeNumber(-(int)product.NumberOfProducts);
                customer = customer.ChangeMoneyValue(_shops[shopId].FindProduct(product.Id).Price * product.NumberOfProducts);
            }

            return customer;
        }

        public Shop FindShop(Guid id)
        {
            return _shops[id];
        }

        public Product RegisterProduct(string name)
        {
            var guid = Guid.NewGuid();
            return _products[guid] = new Product(name, guid);
        }

        public void ChangePrice(Guid shopId, Guid productId, double newPrice)
        {
            _shops[shopId].Products[productId] = _shops[shopId].FindProduct(productId).ChangePrice(newPrice);
        }

        public Shop FindShopWithCheapestProduct(Guid productId, uint number)
        {
            if (!_products.ContainsKey(productId))
                throw new ShopException("This product is not register in ShopManager");
            bool isAnyoneHasProduct = false;
            var checkId = Guid.NewGuid();
            var cheapestShop = new Shop("-", "-", checkId);
            AddProductToShop(cheapestShop.Id, productId, number, double.MaxValue);

            foreach (Shop shop in _shops.Values.Where(shop => shop.Products.ContainsKey(productId)))
            {
                isAnyoneHasProduct = true;
                if (shop.FindProduct(productId).Price <= cheapestShop.FindProduct(productId).Price
                    && cheapestShop.FindProduct(productId).Number >= number)
                    cheapestShop = shop;
            }

            if (cheapestShop.Id != checkId)
                return cheapestShop;
            if (isAnyoneHasProduct)
                throw new ShopException("Store with so many of the products does not exist");
            throw new ShopException("This product is not available in any of the stores");
        }
    }
}