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
    public class ShoppingCartController : ApiController
    {

        ShoppingCartManager dataAccess = new ShoppingCartManager();

        // GET bliver brugt til at request data og læse det   
        [HttpGet]
        [Route("api/ShoppingCartController/GetAllCartItems")]
        public HttpResponseMessage GetCartItems(string storedProc = "GetAllCartItems", int custID = 1, string idParameterName = "@CustomerID")
        {
            return dataAccess.GetData(storedProc, custID, idParameterName);
        }


        // PUT/Update bliver bl.a. brugt til at sende data til en server og opdatere en resource 
        [HttpPut]
        [Route("api/ShoppingCartController/UpdateItemQuan/{cartItemEntry}")]
        public BaseResponse UpdateCartItemQuan(int cartItemEntry, [FromBody] string quan)
        {
            return dataAccess.QuantityUpdateAction(cartItemEntry, int.Parse(quan));
        }


        // POST/Create bliver brugt til at sende data til en server og skabe en resource
        [HttpPost]
        [Route("api/ShoppingCartController/AddItemToCart")]
        public bool AddItemToCart([FromBody] CartDetailItem cartObj)
        {
            return dataAccess.AddItemHandler(cartObj.cartID, cartObj.productID);
        }



        // Delete bliver brugt til at slette en specifik resource 
        [HttpDelete]
        [Route("api/ShoppingCartController/RemoveItemFromCart/{cartItemEntry}")]
        public bool RemoveItemFromCart(int cartItemEntry)
        {
            return dataAccess.RemoveItemFromCart(cartItemEntry);
        }

        [HttpGet]
        [Route("api/ShoppingCartController/GetSumOfCartItems/{cartID}")]
        public int GetSumOfCartItems(int cartID)
        {
            return dataAccess.GetSumOfCartItems(cartID);
        }



    }
}
