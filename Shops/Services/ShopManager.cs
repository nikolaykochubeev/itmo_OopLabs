using System;
using System.Collections.Generic;
using System.Linq;
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

        public Product AddProductToShop(Shop shop, Product product, uint number, float price)
        {
            return !_products.ContainsKey(product.Id)
                ? throw new ShopException("This product is not register in ShopManager")
                : shop.Products[product.Id] = new ShopProduct(product, number, price);
        }

        public void SupplyToShop(Shop shop, Dictionary<Guid, Tuple<Product, uint, float>> products)
        {
            foreach ((Guid id, (Product value, uint number, float price)) in products)
            {
                if (!_products.ContainsKey(id))
                    throw new ShopException("Some product from the supply is not registered in ShopManager");
                if (shop.FindProduct(id) == null)
                    shop.Products.Add(id, new ShopProduct(value, number, price));
                else
                    shop.FindProduct(id).Number += number;
            }
        }

        public void SupplyToShop(Shop shop, Dictionary<Guid, ShopProduct> products)
        {
            foreach ((Guid id, ShopProduct value) in products)
            {
                if (!_products.ContainsKey(id))
                    throw new ShopException("Some product from the supply is not registered in ShopManager");
                if (shop.FindProduct(id) == null)
                    shop.Products.Add(id, value);
                else
                    shop.FindProduct(id).Number += value.Number;
            }
        }

        public Customer BuyInShop(Shop shop, Customer customer)
        {
            foreach (CustomerProduct product in customer.Products)
            {
                if (!_products.ContainsKey(product.Id))
                    throw new ShopException("Some product from the supply is not registered in ShopManager");
                if (customer.Money < shop.FindProduct(product.Id).Price * product.NumberOfProducts)
                    throw new ShopException("Customer hasn't got enough money");
                if (product.NumberOfProducts > shop.FindProduct(product.Id).Number)
                    throw new ShopException("Shop hasn't got enough products");
                shop.FindProduct(product.Id).Number -= product.NumberOfProducts;
                customer.Money -= shop.FindProduct(product.Id).Price * product.NumberOfProducts;
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

        public void ChangePrice(Shop shop, Product product, float newPrice)
        {
            _shops[shop.Id].FindProduct(product.Id).Price = newPrice;
        }

        public Shop FindShopWithCheapestProduct(Product product, uint number)
        {
            if (!_products.ContainsKey(product.Id))
                throw new ShopException("This product is not register in ShopManager");
            bool isAnyoneHasProduct = false;
            var checkId = Guid.NewGuid();
            var cheapestShop = new Shop("-", "-", checkId);
            AddProductToShop(cheapestShop, product, number, float.MaxValue);

            foreach (Shop shop in _shops.Values.Where(shop => shop.Products.ContainsKey(product.Id)))
            {
                isAnyoneHasProduct = true;
                if (shop.FindProduct(product.Id).Price <= cheapestShop.FindProduct(product.Id).Price
                    && cheapestShop.FindProduct(product.Id).Number >= number)
                    cheapestShop = shop;
            }

            if (cheapestShop.Id != checkId)
                return cheapestShop;
            if (isAnyoneHasProduct)
                throw new ShopException("Store with so many of the products does not exist");
            throw new ShopException("This product is not available in any of the stores");
        }

        // public Product CheckingProductRegistration(Guid idProduct)
        // {
        //     if (_products.ContainsKey(idProduct))
        //         return _products[idProduct];
        //     else
        //         throw new ShopException("Product" + _products[idProduct].Id + "is not registered in ShopManager");
        // }

        // foreach (Shop shop in _shops.Values.Where(shop => shop.Products.ContainsKey(product.Id))
        // .Where(shop => shop.Products[product.Id].Price <= shopWithCheapestProduct.Products[product.Id].Price
        // && shopWithCheapestProduct.Products[product.Id].Number >= number))
        // shopWithCheapestProduct = shop;
    }
}