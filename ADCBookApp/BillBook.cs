namespace ADCBookApp
{
    public class BillBook
    {
        public int idBill { get; set; }
        public int idBook { get; set; }
        public Bill bill { get; set; }
        public Book book { get; set; }
    }
}
