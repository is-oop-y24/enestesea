using System;
using System.Collections.Generic;
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
            return _shops[id] = new Shop(name, address, id);
        }

        public Product RegisterProduct(string name)
        {
            Guid id = Guid.NewGuid();
            return _products[id] = new Product(name, id);
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
            return shop.ShopProducts;
        }

        public void Delivery(Shop shop, List<ShopProduct> products)
        {
            foreach (ShopProduct product in products)
            {
                if (shop.ShopProducts.Contains(product))
                {
                    ShopProduct p = shop.FindShopProductInList(product);
                    p.ShopProductAmount += product.ShopProductAmount;
                }
                else
                {
                    shop.ShopProducts.Add(product);
                }
            }
        }

        public Shop FindShopWithBestPrice(List<CustomerProduct> listOfProducts)
        {
            int bestPrice = int.MaxValue;
            Shop cheapShop = null;
            foreach (Shop newShop in _shops.Values)
            {
                int price = 0;
                foreach (CustomerProduct customerProduct in listOfProducts)
                {
                    int productPrice = newShop.PriceForCustomerProduct(customerProduct);
                    if (productPrice == int.MaxValue)
                    {
                        price = int.MaxValue;
                        break;
                    }

                    price += productPrice;
                }

                if (price < bestPrice)
                {
                    bestPrice = price;
                    cheapShop = newShop;
                }
            }

            if (bestPrice == int.MaxValue)
                throw new NoProductException("No such products");
            return cheapShop;
        }
    }
}