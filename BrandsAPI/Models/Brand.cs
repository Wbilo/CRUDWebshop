using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandsAPI.Models
{
    public class Brand
    {
        public int brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_banner { get; set; }
        public string brand_info { get; set; }
    }
}