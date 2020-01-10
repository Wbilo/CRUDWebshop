using BrandsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrandsAPI.Controllers
{
    public class BrandsController : ApiController
    {
        BrandsDataAccess dataAccess = new BrandsDataAccess();

        [HttpGet]
        [Route("api/BrandsController/GetBrandInfo/{brandName}")]
        public Brand GetBrandInfo(string brandName)
        {
            return dataAccess.GetBrandInfo(brandName);
        }
    }
}
