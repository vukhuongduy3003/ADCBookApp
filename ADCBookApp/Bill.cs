using System;

namespace ADCBookApp 
{
    public class Bill
    {
        public int BillId { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public DateTime CreatedTime { get; set; }
        public int TotalItem { get; set; } = 0;
        public long SubTotal { get; set; } = 0;
        public long TotalDiscountAmount { get; set; } = 0;
        public long TotalAmount { get; set; } = 0;
        public string Status { get; set; }

        public Bill()
        {
        }

        public Bill(Cart cart, DateTime createdTime, int totalItem, 
            long subTotal, long totalDiscountAmount, long totalAmount, string status)
        {
            Cart = cart;
            CreatedTime = createdTime;
            TotalItem = totalItem;
            SubTotal = subTotal;
            TotalDiscountAmount = totalDiscountAmount;
            TotalAmount = totalAmount;
            Status = status;
        }

        public int CompareTo(Bill other)
        {
            return BillId - other.BillId;
        }

        public override bool Equals(object obj)
        {
            return obj is Bill bill &&
                   BillId == bill.BillId;
        }

        public override int GetHashCode()
        {
            return 740390073 + BillId.GetHashCode();
        }

    }
}
