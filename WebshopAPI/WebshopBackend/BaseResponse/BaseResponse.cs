using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopBackend.Models
{
    // kilde: https://softwareengineering.stackexchange.com/questions/161702/dealing-with-api-and-error-messages 
    // kilde: https://www.flickr.com/services/api/flickr.activity.userComments.html
    // kilde: https://medium.com/@shazow/how-i-design-json-api-responses-71900f00f2db 
    public class BaseResponse
    {
        // ID'et er unikt nummer som referere til en response type, f.eks. er 602 id'et på 
        // på den respons der bliver sendt tilbage til klienten når en vare bliver slettet fra kurven 
        public int id { get; set; }
        public object result { get; set; }
        public bool successful { get; set; }
        public string message { get; set; }

    }
}