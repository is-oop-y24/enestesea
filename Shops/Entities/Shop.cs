using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;
namespace Shops.Entities
{
    public class Shop : IEquatable<Shop>
    {
        private List<ShopProduct> _shopProducts;
        public Shop(string name, string address, Guid id)
        {
            ShopName = name;
            ShopId = id;
            ShopAddress = address;
            _shopProducts = new List<ShopProduct>();
        }

        public string ShopName { get; }
        public Guid ShopId { get; }
        public string ShopAddress { get; }

        public void AddProduct(ShopProduct shopProduct)
        {
            if (_shopProducts.Contains(shopProduct))
            {
                ShopProduct p = FindShopProductInList(shopProduct);
                p.ShopProductAmount += shopProduct.ShopProductAmount;
                p.ShopProductPrice = shopProduct.ShopProductPrice;
            }
            else
            {
                _shopProducts.Add(shopProduct);
            }
        }

        public List<ShopProduct> GetShopProducts()
        {
            return _shopProducts.ToList();
        }

        public void ChangePrice(Product product, int value)
        {
            ShopProduct p = FindShopProductInList(product);
            p.ShopProductPrice = value;
        }

        public int? PriceForCustomerProduct(CustomerProduct customerProduct)
        {
            try
            {
                ShopProduct currentProduct = FindShopProductInList(customerProduct);
                if (currentProduct.ShopProductAmount < customerProduct.CustomerProductAmount)
                    return null;
                return customerProduct.CustomerProductAmount * currentProduct.ShopProductPrice;
            }
            catch
            {
                return null;
            }
        }

        public void BuyProduct(CustomerProduct product, Customer customer)
        {
            int? productPrice = PriceForCustomerProduct(product);
            if (productPrice == null)
                throw new LackOfProductException("Sorry, there is not enough products in this shop");
            if (productPrice > customer.Money)
                throw new LackOfMoneyException("Sorry, you don't have enough money to buy it");
            customer.Money -= productPrice.Value;
            RemoveProductFromShop(product);
        }

        public void BuyAllShoppingList(List<CustomerProduct> shoppingList, Customer customer)
        {
            if (customer == null)
                throw new ShopException("penis");
            int? total = 0;
            foreach (CustomerProduct product in shoppingList)
            {
                int? curTotal = PriceForCustomerProduct(product);
                if (curTotal == null)
                    throw new LackOfProductException("Sorry, there is not enough products in this shop");
                total += curTotal.Value;
            }

            if (total > customer.Money)
                throw new LackOfMoneyException("Sorry, you don't have enough money to buy it");
            customer.Money -= total.Value;
            RemoveProductsFromShop(shoppingList);
        }

        public bool Equals(Shop other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ShopName == other.ShopName && ShopId.Equals(other.ShopId) && Equals(_shopProducts, other._shopProducts);
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
            return HashCode.Combine(ShopName, ShopId, _shopProducts);
        }

        public ShopProduct FindShopProductInList(ShopProduct product)
        {
            ShopProduct curProduct =
                _shopProducts.FirstOrDefault(newProduct => newProduct.ProductOfShop.Equals(product.ProductOfShop));
            return curProduct;
        }

        public ShopProduct FindShopProductInList(CustomerProduct product)
        {
            ShopProduct curProduct = _shopProducts.FirstOrDefault(newProduct => newProduct.ProductOfShop.Equals(product.ProductOfCustomer));
            return curProduct;
        }

        public ShopProduct FindShopProductInList(Product product)
        {
            ShopProduct curProduct = _shopProducts.FirstOrDefault(newProduct => newProduct.ProductOfShop.Equals(product));
            return curProduct;
        }

        private void RemoveProductFromShop(CustomerProduct customerProduct)
        {
            ShopProduct product = FindShopProductInList(customerProduct);
            if (product.ShopProductAmount == customerProduct.CustomerProductAmount)
            {
                _shopProducts.Remove(product);
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