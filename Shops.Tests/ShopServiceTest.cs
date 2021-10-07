using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class ShopServiceTest
    {
        private IShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }
        
        [Test]
        public void AddProductsToShop()
        {
            Shop sh = _shopManager.RegisterShop("Дикси", "улица В д6");
            Product p1 = _shopManager.RegisterProduct("Молоко");
            Product p4 = _shopManager.RegisterProduct("НеМолоко");
            _shopManager.Delivery(sh, new List<ShopProduct> {new ShopProduct(p4, 90, 10), new ShopProduct(p1, 90, 10)});
            Assert.AreEqual(90, sh.FindShopProductInList(p4).ShopProductPrice);
        }
        [Test]
        public void BuyNoProduct_ThrowException()
        {
            Assert.Catch<ShopException>(() =>
            {
                Shop sh = _shopManager.RegisterShop("Пятера", "улица Г д26");
                Product pr = _shopManager.RegisterProduct("Батон");
                Customer cust = new Customer("Петя", 100, new List<CustomerProduct> {new CustomerProduct(pr, 1)});
                _shopManager.BuyProduct(sh, cust.ShoppingList[0], cust);
            });
        }
        
        
        [Test]
        public void BuyTooMany_ThrowException()
        {
            Assert.Catch<ShopException>(() =>
            {    
                Shop sh = _shopManager.RegisterShop("Пятера", "улица Г д26");
                Product pr = _shopManager.RegisterProduct("Батон");
                Customer cust = new Customer("Петя", 100, new List<CustomerProduct> {new CustomerProduct(pr, 1)});
                _shopManager.BuyProduct(sh, cust.ShoppingList[0], cust);
                _shopManager.AddProduct(pr, 10, 5, sh);
                List<CustomerProduct> shoppingList = new List<CustomerProduct> {new CustomerProduct(pr, 25)};
                Customer cust2 = new Customer("Ваня", 100, shoppingList);
                sh.BuyProduct(shoppingList[0], cust2);
            });
        }
        
        [Test]
        public void BuyTooManySoNoMoney_ThrowException()
        {
            Assert.Catch<ShopException>(() =>
            {
                Shop sh = _shopManager.RegisterShop("Пятера", "улица Г д26");
                Product pr = _shopManager.RegisterProduct("Батон");
                Customer cust = new Customer("Петя", 100, new List<CustomerProduct> {new CustomerProduct(pr, 10)});
               _shopManager.AddProduct(pr, 10, 5, sh);
               _shopManager.ChangeProductPriceInShop(sh, pr,1000);
               sh.BuyProduct(cust.ShoppingList[0], cust);
            });
            
        }
        
        [Test]
        public void FindShopWithBestPrice()
        {
            Shop s1 = _shopManager.RegisterShop("М", "1");
            Shop s2 = _shopManager.RegisterShop("А", "2");
            _shopManager.RegisterShop("Г", "3");
            _shopManager.RegisterShop("А", "4");
            _shopManager.RegisterShop("З", "5");
            Product p1 = _shopManager.RegisterProduct("х1");
            var shopP1 = new ShopProduct(p1, 50, 10);
            Product p2 = _shopManager.RegisterProduct("х1");
            var shopP2 = new ShopProduct(p2, 55, 10);
            _shopManager.AddProduct(shopP1, s2);
            _shopManager.AddProduct(shopP2, s1);
            Assert.AreEqual(_shopManager.FindShopWithBestPrice(new List<CustomerProduct>{new CustomerProduct(p2, 10)}), s1);
            }
    }
}