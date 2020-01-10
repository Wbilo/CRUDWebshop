using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopBackend.Models
{
    public class Order
    {
        public int order_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public Nullable<System.DateTime> order_date { get; set; }

    }
}