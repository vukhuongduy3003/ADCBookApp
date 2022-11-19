﻿using System;

namespace ADCBookApp
{
    public class SelectedItem : Item
    {
        public int NumberOfSelectedItem { get; set; }
        public int PriceAfterDiscount { get; set; }

        public SelectedItem() {
            CalculatePriceAfterDiscount();
        }

        //public SelectedItem(Item item) : base(item.ItemName, item.ItemType, 
        //    item.Quantity, item.Brand, item.ReleaseDate, item.Price, item.Discount)
        //{
        //    CalculatePriceAfterDiscount();
        //}

        public SelectedItem(int numberOfSelectedItem)
        {
            NumberOfSelectedItem = numberOfSelectedItem;
            CalculatePriceAfterDiscount();
        }

        private void CalculatePriceAfterDiscount()
        {
            if(Discount == null)
            {
                PriceAfterDiscount = Price;
            } else
            {
                var currentTime = DateTime.Now;
                if(currentTime >= Discount.StartTime && currentTime <= Discount.EndTime)
                {
                    if(Discount.DiscountPercent > 0)
                    {
                        PriceAfterDiscount = (int)(Price * (1 - 1.0f * Discount.DiscountPercent / 100));
                    }
                    if(Discount.DiscountPriceAmount > 0)
                    {
                        PriceAfterDiscount = Price - Discount.DiscountPriceAmount;
                    }
                }
            }
        }

        public SelectedItem(int itemId, string itemName, string itemType,
            int quantity, string brand, DateTime releaseDate, int price,
            Discount discount, int numberOfSelectedItem) :
            base(itemName, itemType, quantity, brand, releaseDate, price, discount)
        {
            NumberOfSelectedItem = numberOfSelectedItem;
        }
    }
}
