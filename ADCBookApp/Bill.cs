using System;
using System.Collections.Generic;

namespace ADCBookApp
{
    public class Bill
    {
        public int idBill { get; set; }
        public int Custommer_idCustommer { get; set; }
        public int Discount_idDiscount { get; set; }
        public float PayTotal { get; set; }
        public float PayAfterDiscount { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Book> books { get; set; }
    }
}
