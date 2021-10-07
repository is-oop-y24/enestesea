using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;
namespace Shops.Entities
{
    public class Shop : IEquatable<Shop>
    {
        public Shop(string name, string address, Guid id)
        {
            ShopName = name;
            ShopId = id;
            ShopAddress = address;
            ShopProducts = new List<ShopProduct>();
        }

        public string ShopName { get; }
        public Guid ShopId { get; }
        public string ShopAddress { get; }
        public List<ShopProduct> ShopProducts { get;  }

        public void AddProduct(ShopProduct shopProduct)
        {
            if (ShopProducts.Contains(shopProduct))
            {
                ShopProduct p = FindShopProductInList(shopProduct);
                p.ShopProductAmount += shopProduct.ShopProductAmount;
                p.ShopProductPrice = shopProduct.ShopProductPrice;
            }
            else
            {
                ShopProducts.Add(shopProduct);
            }
        }

        public void ChangePrice(Product product, int value)
        {
            ShopProduct p = FindShopProductInList(product);
            p.ShopProductPrice = value;
        }

        public int PriceForCustomerProduct(CustomerProduct customerProduct)
        {
            try
            {
                ShopProduct currentProduct = FindShopProductInList(customerProduct);
                if (currentProduct.ShopProductAmount < customerProduct.CustomerProductAmount)
                    return int.MaxValue;
                return customerProduct.CustomerProductAmount * currentProduct.ShopProductPrice;
            }
            catch
            {
                return int.MaxValue;
            }
        }

        public void BuyProduct(CustomerProduct product, Customer customer)
        {
            int productPrice = PriceForCustomerProduct(product);
            if (productPrice == int.MaxValue)
                throw new LackOfProductException("Sorry, there is not enough products in this shop");
            if (productPrice > customer.Money)
                throw new LackOfMoneyException("Sorry, you don't have enough money to buy it");
            customer.Money -= productPrice;
            RemoveProductFromShop(product);
        }

        public void BuyAllShoppingList(List<CustomerProduct> shoppingList, Customer customer)
        {
            int total = 0;
            foreach (CustomerProduct product in shoppingList)
            {
                int curTotal = PriceForCustomerProduct(product);
                if (curTotal == int.MaxValue)
                    throw new LackOfProductException("Sorry, there is not enough products in this shop");
                total += curTotal;
            }

            if (total > customer.Money)
                throw new LackOfMoneyException("Sorry, you don't have enough money to buy it");
            customer.Money -= total;
            RemoveProductsFromShop(shoppingList);
        }

        public bool Equals(Shop other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ShopName == other.ShopName && ShopId.Equals(other.ShopId) && Equals(ShopProducts, other.ShopProducts);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Shop)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ShopName, ShopId, ShopProducts);
        }

        public ShopProduct FindShopProductInList(ShopProduct product)
        {
            ShopProduct curProduct = ShopProducts.FirstOrDefault(newProduct => newProduct.ProductOfShop.Equals(product.ProductOfShop));
            if (curProduct == null)
                throw new NoProductException("There is no such product");
            return curProduct;
        }

        public ShopProduct FindShopProductInList(CustomerProduct product)
        {
            ShopProduct curProduct = ShopProducts.FirstOrDefault(newProduct => newProduct.ProductOfShop.Equals(product.ProductOfCustomer));
            if (curProduct == null)
                throw new NoProductException("There is no such product");
            return curProduct;
        }

        public ShopProduct FindShopProductInList(Product product)
        {
            ShopProduct curProduct = ShopProducts.FirstOrDefault(newProduct => newProduct.ProductOfShop.Equals(product));
            if (curProduct == null)
                throw new NoProductException("There is no such product");
            return curProduct;
        }

        private void RemoveProductFromShop(CustomerProduct customerProduct)
        {
            ShopProduct product = FindShopProductInList(customerProduct);
            if (product.ShopProductAmount == customerProduct.CustomerProductAmount)
            {
                ShopProducts.Remove(product);
            }
            else if (product.ShopProductAmount > customerProduct.CustomerProductAmount)
            {
                product.ShopProductAmount -= customerProduct.CustomerProductAmount;
            }
        }

        private void RemoveProductsFromShop(List<CustomerProduct> shoppingList)
        {
            foreach (CustomerProduct customerProduct in shoppingList)
            {
                RemoveProductFromShop(customerProduct);
            }
        }
    }
}