using System.Collections.Generic;

namespace ADCBookApp
{
    public class Cart
    {
        public int CartId { get; set; }
        public Customer Customer { get; set; }
        public List<SelectedItem> SelectedItems { get; set; } = new List<SelectedItem>();
        public int TotalItems { get; set; }

        public Cart()
        {
        }

        public Cart(Customer customer, 
            List<SelectedItem> selectedItems, int totalItems)
        {
            Customer = customer;
            SelectedItems = selectedItems;
            TotalItems = totalItems;
        }

        public override bool Equals(object obj)
        {
            return obj is Cart cart &&
                   CartId == cart.CartId;
        }

        public override int GetHashCode()
        {
            return -1568810734 + CartId.GetHashCode();
        }

        public int CompareTo(Cart other)
        {
            return CartId - other.CartId;
        }

    }
}
