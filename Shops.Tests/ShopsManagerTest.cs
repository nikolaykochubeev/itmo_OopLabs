using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;
        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void TryToBuyProductWithoutEnoughAmountOfMoney_ThrowException()
        {
            Assert.Catch<ShopException>(() =>
            {
                var customer = new Customer("Tema", 10);
                Shop shop1 = _shopManager.AddShop("okey", "spb");
                Product product1 = _shopManager.RegisterProduct("Bread");
                customer.AddProducts(new List<CustomerProduct>() {new CustomerProduct(product1, 2)});
                _shopManager.AddProductToShop(shop1, product1, 10, 100);
                _shopManager.BuyInShop(shop1, customer);
            });
        }
        [Test]
        public void TryToBuyProductWithoutEnoughNumberOfProduct_ThrowException()
        {
            Assert.Catch<ShopException>(() =>
            {
                var customer = new Customer("Tema", 1000);
                Shop shop1 = _shopManager.AddShop("okey", "spb");
                Product product1 = _shopManager.RegisterProduct("Bread");
                customer.AddProducts(new List<CustomerProduct>() {new CustomerProduct(product1, 10)});
                _shopManager.AddProductToShop(shop1, product1, 1, 1);
                _shopManager.BuyInShop(shop1, customer);
            });
        }
        [Test]
        public void TryToAddProductProductToShopWithoutRegistration_ThrowException()
        {
            Assert.Catch<ShopException>(() =>
            {
                Shop shop1 = _shopManager.AddShop("okey", "spb");
                var product1 = new Product("Smuggling bread", Guid.NewGuid());
                _shopManager.AddProductToShop(shop1, product1, 10, 100);
            });
        }
        [Test]
        public void SupplyProductsToShopWithEqualNamesPricesNumbers()
        {
            Shop shop1 = _shopManager.AddShop("okey", "spb");
            var product1 = new ShopProduct(_shopManager.RegisterProduct("Milk"), 10, 15);
            var product2 = new ShopProduct(_shopManager.RegisterProduct("Milk"), 10, 15);
            _shopManager.SupplyToShop(shop1, new Dictionary<Guid, ShopProduct>() {{product1.Id, product1}, {product2.Id, product2}});
            Assert.Contains(product1, shop1.Products.Values);
            Assert.Contains(product2, shop1.Products.Values);
        }

        [Test]
        public void CheckingInstallationAndPriceChangesInShop()
        {
            float priceBefore = 10;
            float priceAfter = 15;
            
            Shop shop = _shopManager.AddShop("okey", "spb");
            Product product = _shopManager.RegisterProduct("Bread");
            _shopManager.AddProductToShop(shop, product, 10, priceBefore);
            Assert.AreEqual(shop.FindProduct(product.Id).Price, priceBefore);
            _shopManager.ChangePrice(shop, product, priceAfter);
            Assert.AreEqual(shop.FindProduct(product.Id).Price, priceAfter);
        }
    }
}