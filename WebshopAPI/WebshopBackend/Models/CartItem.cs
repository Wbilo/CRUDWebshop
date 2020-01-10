using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopBackend.Models
{
    public class CartItem
    {
        public int cartDetailID { get; set; }
        public int cartID { get; set; }
        public int productID { get; set; }
        public int quantity { get; set; }
        public DateTime dateAdded { get; set; }

        public CartItem(int id, int product_ID)
        {
            this.cartID = id;
            productID = product_ID;
            quantity = 1;
            dateAdded = DateTime.Now;
        }

    }
}