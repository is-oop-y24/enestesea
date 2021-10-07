using System;
using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        Shop RegisterShop(string name, string address);
        Product RegisterProduct(string name);
        ShopProduct AddProduct(Product product, int price, int amount, Shop shop);
        void AddProduct(ShopProduct product, Shop shop);
        void ChangeProductPriceInShop(Shop shop, Product product, int price);
        void BuyProduct(Shop shop, CustomerProduct product, Customer customer);
        void BuyAllList(List<CustomerProduct> shoppingList, Shop shop, Customer customer);
        List<ShopProduct> AllProducts(Shop shop);
        void Delivery(Shop shop, List<ShopProduct> products);
        Shop FindShopWithBestPrice(List<CustomerProduct> listOfPruducts);
    }
}