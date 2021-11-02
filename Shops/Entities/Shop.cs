using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private readonly List<ShopProduct> _products;
        public Shop(Guid id, string shopName, string address, List<ShopProduct> products = null)
        {
            ShopName = shopName;
            Id = id;
            Address = address;
            _products = products ?? new List<ShopProduct>();
        }

        public IReadOnlyList<ShopProduct> Products => _products;
        public Guid Id { get; }
        public string ShopName { get; }
        public string Address { get; }

        public ShopProduct FindProduct(Guid productId)
        {
            return _products.FirstOrDefault(product => product.Id == productId);
        }

        public ShopProduct AddProduct(ShopProduct shopProduct)
        {
            if (FindProduct(shopProduct.Id) is not null)
                throw new ShopException("the product is already in the shop");
            _products.Add(shopProduct);
            return _products.Last();
        }

        public ShopProduct AddProduct(Product product, uint number, decimal price)
        {
            if (FindProduct(product.Id) is not null)
                throw new ShopException("the product is already in the shop");
            _products.Add(new ShopProduct(product, number, price));
            return _products.Last();
        }

        public void Supply(List<ShopProduct> products)
        {
            foreach (ShopProduct shopProduct in products)
            {
                ShopProduct currentProduct = FindProduct(shopProduct.Id);
                if (currentProduct is not null)
                {
                    _products.Remove(currentProduct);
                    AddProduct(currentProduct.ChangeNumber((int)shopProduct.Amount));
                }
                else
                {
                    AddProduct(shopProduct);
                }
            }
        }

        public Customer Buy(IEnumerable<CustomerProduct> products, Customer customer)
        {
            foreach (CustomerProduct product in products)
            {
                if (_products.FirstOrDefault(shopProduct => shopProduct.Id == product.Id) is null)
                    throw new ShopException("Some product from the supply is not contains in ShopManager");
                if (customer.Money < FindProduct(product.Id).Price * product.NumberOfProducts)
                    throw new ShopException("Customer hasn't got enough money");
                if (product.NumberOfProducts > FindProduct(product.Id).Amount)
                    throw new ShopException("Shop hasn't got enough products");
                FindProduct(product.Id).ChangeNumber(-(int)product.NumberOfProducts);
                customer = customer.ChangeMoneyValue(FindProduct(product.Id).Price * product.NumberOfProducts);
            }

            return customer;
        }

        public void ChangePrice(Guid productId, decimal newPrice)
        {
            ShopProduct shopProduct = FindProduct(productId);
            if (shopProduct is null)
                throw new ShopException("Product");
            _products.Remove(shopProduct);
            _products.Add(shopProduct.ChangePrice(newPrice));
        }
    }
}
