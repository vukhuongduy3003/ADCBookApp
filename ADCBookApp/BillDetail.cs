﻿using System;

namespace ADCBookApp
{
    public class BillDetail : Bill
    {
        public string PaymentMethod { get; set; }
        public string StaffName { get; set; }

        public BillDetail()
        {
        }

        public BillDetail(int id)  { }

        public BillDetail(string paymentMethod, string staffName)
        {
            PaymentMethod = paymentMethod;
            StaffName = staffName;
        }

        public BillDetail(Cart cart, DateTime createdTime, int totalItem,
            long subTotal, long totalDiscountAmount, long totalAmount, string status,
            string paymentMethod, string staffName) : 
            base(cart, createdTime, totalItem, subTotal, 
                totalDiscountAmount, totalAmount, status)
        {
            PaymentMethod = paymentMethod;
            StaffName = staffName;
        }

        public override bool Equals(object obj)
        {
            return obj is BillDetail detail &&
                   base.Equals(obj) &&
                   BillId == detail.BillId;
        }

        public override int GetHashCode()
        {
            int hashCode = -1170810969;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + BillId.GetHashCode();
            return hashCode;
        }
    }
}
