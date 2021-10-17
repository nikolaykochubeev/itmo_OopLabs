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
                Shop shop = _shopManager.RegisterShop(new Shop(Guid.NewGuid(), "okey", "spb"));
                var product = new Product(Guid.NewGuid(), "Bread");
                var customer = new Customer("Tema", 10);
                
                shop.AddProduct(product, 10, 20);
                shop.Buy(new List<CustomerProduct>() {new(product, 2)}, customer);
            });
        }
        
        [Test]
        public void SupplyProductsToShopWithEqualNamesPricesNumbers_AndCheckOnAvailabilityInTheShop()
        {
            Shop shop = _shopManager.RegisterShop(new Shop(Guid.NewGuid(), "okey", "spb"));
            
            uint amount = 10;
            decimal price = 15;
            var product1 = new ShopProduct(new Product(Guid.NewGuid(), "Bread"), amount, price);
            var product2 = new ShopProduct(new Product(Guid.NewGuid(), "Bread"), amount, price);
            
            shop.Supply(new List<ShopProduct> {product1, product2});
            CollectionAssert.Contains(shop.Products, product1);
            CollectionAssert.Contains(shop.Products, product2); 
        }

        [Test]
        public void SetPriceAndChangePriceInShop()
        {
            Shop shop = _shopManager.RegisterShop(new Shop(Guid.NewGuid(), "okey", "spb"));
            
            decimal priceBefore = 10;
            decimal priceAfter = 15;
            var product = new ShopProduct(new Product(Guid.NewGuid(), "Bread"), 10, priceBefore);
            
            shop.AddProduct(product);
            Assert.AreEqual(shop.FindProduct(product.Id).Price, priceBefore);
            shop.ChangePrice(product.Id, priceAfter);
            Assert.AreEqual(shop.FindProduct(product.Id).Price, priceAfter);
        }
        [Test]
        public void FindShopWithCheapestPrice_CorrectWork()
        {
            Shop cheapestShop = _shopManager.RegisterShop(new Shop(Guid.NewGuid(), "shopWithCheapestPrice", "spb"));
            Shop middleShop = _shopManager.RegisterShop(new Shop(Guid.NewGuid(), "shopWithMiddlePrice", "spb"));
            Shop expensiveShop = _shopManager.RegisterShop(new Shop(Guid.NewGuid(), "shopWithExpensivePrice", "spb"));

            var productId = Guid.NewGuid();
            var cheapestProduct = new ShopProduct(new Product(productId, "Bread"), 4, 5);
            var middleProduct = new ShopProduct(new Product(productId, "Bread"), 4, 10);
            var expensiveProduct = new ShopProduct(new Product(productId, "Bread"), 4, 15);

            cheapestShop.AddProduct(cheapestProduct);
            middleShop.AddProduct(middleProduct);
            expensiveShop.AddProduct(expensiveProduct);
            
            Assert.AreEqual(_shopManager.FindShopWithCheapestProduct(productId, 2), cheapestShop);
        }
        
        [Test]
        public void FindShopWithCheapestPrice_CheapestShopHasAnInsufficientAmountOfProducts()
        {
            Shop cheapestShop = _shopManager.RegisterShop(new Shop(Guid.NewGuid(), "shopWithCheapestPrice", "spb"));
            Shop middleShop = _shopManager.RegisterShop(new Shop(Guid.NewGuid(), "shopWithMiddlePrice", "spb"));
            Shop expensiveShop = _shopManager.RegisterShop(new Shop(Guid.NewGuid(), "shopWithExpensivePrice", "spb"));

            var productId = Guid.NewGuid();
            var cheapestProduct = new ShopProduct(new Product(productId, "Bread"), 1, 5);
            var middleProduct = new ShopProduct(new Product(productId, "Bread"), 4, 10);
            var expensiveProduct = new ShopProduct(new Product(productId, "Bread"), 4, 15);

            cheapestShop.AddProduct(cheapestProduct);
            middleShop.AddProduct(middleProduct);
            expensiveShop.AddProduct(expensiveProduct);
            
            Assert.AreEqual(_shopManager.FindShopWithCheapestProduct(productId, 2), middleShop);
        }
    }
}