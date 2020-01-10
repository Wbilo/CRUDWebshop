using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopBackend.Models
{
    public class Customer
    {
        public int customer_id { get; set; }
        public string customer_fname { get; set; }
        public string customer_lname { get; set; }
        public string customer_email { get; set; }
    }
}