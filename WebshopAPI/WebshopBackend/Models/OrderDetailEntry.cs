using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopBackend.Models
{
    public class OrderDetailEntry
    {
        public int order_detail_entry_ID { get; set; }
        public Nullable<int> order_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<double> combined_price { get; set; }


    }
}