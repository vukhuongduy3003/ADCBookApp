using System;

namespace ADCBookApp
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DiscounType { get; set; }
        public int DiscountPriceAmount { get; set; }
        public int DiscountPercent { get; set; }

        public Discount() { }

        public Discount(string name, DateTime startTime,
            DateTime endTime, string discounType, int discountPriceAmount,
            int discountPercent)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            DiscounType = discounType;
            DiscountPriceAmount = discountPriceAmount;
            DiscountPercent = discountPercent;
        }

        public override bool Equals(object obj)
        {
            return obj is Discount discount &&
                   DiscountId == discount.DiscountId;
        }

        public override int GetHashCode()
        {
            return 1574009819 + DiscountId.GetHashCode();
        }

    }
}
