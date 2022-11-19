using System;

namespace ADCBookApp
{
    public class StatCustomer
    {
        public Customer Customer { get; set; }
        public long TotalAmount { get; set; }

        public StatCustomer()
        {
        }

        public StatCustomer(Customer customer, long totalAmount)
        {
            Customer = customer;
            TotalAmount = totalAmount;
        }

        public int CompareTo(StatCustomer other)
        {
            return other.TotalAmount.CompareTo(TotalAmount);
        }
    }
}
