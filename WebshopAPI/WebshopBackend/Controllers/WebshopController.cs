using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebshopBackend.Models;

namespace WebshopBackend.Controllers
{
    public class WebshopController : ApiController
    {
        WebshopDataAccess dataAccess = new WebshopDataAccess();
        [HttpGet]
        [Route("api/Webshop/GetAllProductInfo")]
        public List<Product> GetAllProductInfo()
        {
            return dataAccess.GetAllProductInfo();
        }

        [HttpGet]
        [Route("api/Webshop/GetProductsByBrand/{brandName}")]
        public List<Product> GetProductsByBrand(string brandName)
        {
            return dataAccess.GetProductsByBrand(brandName);
        }

        [HttpGet]
        [Route("api/Webshop/GetAllBrandsInfo")]
        public List<Brand> GetAllBrandsInfo()
        {
            return dataAccess.GetAllBrandsInfo();
        }



    }
}