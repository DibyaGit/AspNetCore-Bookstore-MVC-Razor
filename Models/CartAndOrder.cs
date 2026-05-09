using System.Collections.Generic;

namespace BookstoreApp.Models
{
    public class CartItem
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItem> OrderItems { get; set; } = new List<CartItem>();
    }
}
