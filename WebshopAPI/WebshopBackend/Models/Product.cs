using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopBackend.Models
{

    public class Product
    {

        public int product_id { get; set; }
        public string product_name { get; set; }
        public Nullable<double> product_price { get; set; }
        public Nullable<bool> product_inStock { get; set; }
        public string product_brand { get; set; }
        public string product_info { get; set; }
        public string product_img { get; set; }
    }
}