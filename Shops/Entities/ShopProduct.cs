using System;

namespace Shops.Entities
{
    public class ShopProduct : IEquatable<ShopProduct>
    {
        private int _amount;
        private int _price;
        public ShopProduct(Product product, int price, int amount)
        {
            ProductOfShop = new Product(product);
            ShopProductPrice = price;
            ShopProductAmount = amount;
        }

        public Product ProductOfShop { get; }

        public int ShopProductPrice
        {
            get => _price;
            set => _price = value;
        }

        public int ShopProductAmount
        {
            get => _amount;
            set => _amount = value;
        }

        public bool Equals(ShopProduct other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _amount == other._amount && _price == other._price && Equals(ProductOfShop, other.ProductOfShop);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ShopProduct)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_amount, _price, ProductOfShop);
        }
    }
}