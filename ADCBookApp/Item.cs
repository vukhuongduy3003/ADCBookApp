using System;

namespace ADCBookApp
{
    /// <summary>
    /// Lớp mô tả thông tin mặt hàng
    /// </summary>
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Price { get; set; }
        public Discount Discount { get; set; }

        public Item() { }

        public Item(int itemId, string itemName, string itemType,
            int quantity, string brand, DateTime releaseDate, int price,
            Discount discount) : this()
        {
            ItemId = itemId;
            ItemName = itemName;
            ItemType = itemType;
            Quantity = quantity;
            Brand = brand;
            ReleaseDate = releaseDate;
            Price = price;
            Discount = discount;
        }

        public Item(string itemName, string itemType,
            int quantity, string brand, DateTime releaseDate, int price,
            Discount discount)
        {
            ItemName = itemName;
            ItemType = itemType;
            Quantity = quantity;
            Brand = brand;
            ReleaseDate = releaseDate;
            Price = price;
            Discount = discount;
        }

        public int CompareTo(Item other)
        {
            return ItemId - other.ItemId;
        }

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   ItemId == item.ItemId;
        }

        public override int GetHashCode()
        {
            return -2113648141 + ItemId.GetHashCode();
        }
    }
}
