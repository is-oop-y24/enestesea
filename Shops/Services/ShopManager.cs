using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;
namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private Dictionary<Guid, Shop> _shops;
        private Dictionary<Guid, Product> _products;
        public ShopManager()
        {
            _shops = new Dictionary<Guid, Shop>();
            _products = new Dictionary<Guid, Product>();
        }

        public Shop RegisterShop(string name, string address)
        {
            Guid id = Guid.NewGuid();
            var shop = new Shop(name, address, id);
            _shops.Add(id, shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            Guid id = Guid.NewGuid();
            var product = new Product(name, id);
            _products.Add(id, product);
            return product;
        }

        public ShopProduct AddProduct(Product product, int price, int amount, Shop shop)
        {
            ShopProduct newProduct = new ShopProduct(product, price, amount);
            shop.AddProduct(newProduct);
            return newProduct;
        }

        public void AddProduct(ShopProduct product, Shop shop)
        {
            shop.AddProduct(product);
        }

        public void ChangeProductPriceInShop(Shop shop, Product product, int price)
        {
            shop.ChangePrice(product, price);
        }

        public void BuyProduct(Shop shop, CustomerProduct product, Customer customer)
        {
            shop.BuyProduct(product, customer);
        }

        public void BuyAllList(List<CustomerProduct> shoppingList, Shop shop, Customer customer)
        {
            shop.BuyAllShoppingList(shoppingList, customer);
        }

        public List<ShopProduct> AllProducts(Shop shop)
        {
            return shop.GetShopProducts();
        }

        public void Delivery(Shop shop, List<ShopProduct> products)
        {
            foreach (ShopProduct product in products)
            {
                if (shop.GetShopProducts().Contains(product))
                {
                    ShopProduct p = shop.FindShopProductInList(product);
                    p.ShopProductAmount += product.ShopProductAmount;
                }
                else
                {
                    shop.AddProduct(product);
                }
            }
        }

        public Shop FindShopWithBestPrice(List<CustomerProduct> listOfProducts)
        {
            int bestPrice = 0;
            Shop cheapShop = null;
            foreach (Shop newShop in _shops.Values)
            {
                int price = 0;
                foreach (int? productPrice in listOfProducts.Select(customerProduct => newShop.PriceForCustomerProduct(customerProduct)))
                {
                    if (productPrice == int.MaxValue)
                    {
                        price = int.MaxValue;
                        break;
                    }

                    if (productPrice != null) price += productPrice.Value;
                }

                if (bestPrice == 0)
                    bestPrice = price;
                if (price >= bestPrice) continue;
                bestPrice = price;
                cheapShop = newShop;
            }

            if (bestPrice == int.MaxValue)
                throw new NoProductException("No such products");
            return cheapShop;
        }
    }
}