using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace WebshopBackend.Models
{
    public class ShoppingCartManager
    {
        private static string connecString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        // General GetData metoder der retunere resultat fra en stored procedure   
        public HttpResponseMessage GetData(string storedProcedure, int id = -1, string idParameterName = "")
        {
            // navnet på Output parameter som den er defineret i stored procedure i DB. 
            string jsonOutputParam = "@jsonOutput";
            // Output fra databasen gemmes her  
            string outPut;
            // Laver en ny instans 
            var resp = new HttpResponseMessage();

            using (SqlConnection con = new SqlConnection(connecString))
            {


                SqlCommand cmd = new SqlCommand(storedProcedure, con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                if (id != -1)
                {
                    cmd.Parameters.Add(idParameterName, SqlDbType.Int).Value = id;
                }

                // Skaber output parameter. "-1" bliver brugt til nvarchar(max)
                cmd.Parameters.Add(jsonOutputParam, SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                // Execute command
                cmd.ExecuteNonQuery();

                // Henter værdien 
                outPut = cmd.Parameters[jsonOutputParam].Value.ToString();

                // Responsens content skal være outputtet fra stored procedure 
                resp.Content = new StringContent(outPut);
                // Bestemmer at det skal være json datatype format  
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            }
            return resp;
        }

        // Håndtere request om at opdatere item quantity   
        public BaseResponse QuantityUpdateAction(int cartItemEntry, int quan)
        {
            int currentQuan = 0;
            int stock = 0;
            using (SqlConnection con = new SqlConnection(connecString))
            {
                // Stored procedure navn 
                string proc = "GetQuanAndStockAmount";

                SqlCommand cmd = new SqlCommand(proc, con);

                // Sørger for at vores command string bliver fortolket som en stored procedure  
                cmd.CommandType = CommandType.StoredProcedure;
                // Følgende forhindrer SQL injections, ved at specificere hvilken datatype parameterværdien skal fortolkes som   
                cmd.Parameters.Add("@cartDetailID", SqlDbType.Int).Value = cartItemEntry;
                con.Open();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            currentQuan = (int)reader["quantity"];
                            stock = (int)reader["stock_amount"];

                        }
                    }
                    else
                    {
                        return new BaseResponse
                        {
                            id = 599,
                            message = "The item doesnt exist"
                        };
                    }



                }
                // Der skete en fejl ved read
                catch (Exception ex)
                {
                    return new BaseResponse
                    {
                        id = 600,
                        message = ex.Message
                    };
                }
                // Ikke muligt at tilføje mere 
                if (stock - (currentQuan + quan) <= -1)
                {
                    return new BaseResponse
                    {
                        id = 601,
                        message = "Cant add the specified amount"
                    };
                }
                // Hvis quantity bliver 0 eller under 0, så slet varen fra cart        
                else if (quan + currentQuan <= 0)
                {
                    RemoveItemFromCart(cartItemEntry);
                    return new BaseResponse
                    {
                        id = 602,
                        message = "The item got deleted",
                        successful = true,
                        result = currentQuan
                    };
                }

                // Ellers kan godt ændre quantity.   
                else
                {
                    int quanAfterUpdate = UpdateItemQuan(cartItemEntry, quan);
                    return new BaseResponse
                    {
                        id = 603,
                        message = "The item quantity has been updated",
                        successful = true,
                        result = quanAfterUpdate
                    };
                }


            }


        }


        // Fjerner vare fra kurv 
        public bool RemoveItemFromCart(int cartItemEntry)
        {

            DBManager db = new DBManager(connecString);

            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(db.CreateParameter("@cartDetailID", cartItemEntry, DbType.Int32));

            db.Delete("RemoveItemFromCart", CommandType.StoredProcedure, parameters.ToArray());
            return true;
        }

        // Håndtere request om at tilføje vare til kurv  
        public bool AddItemHandler(int cartID, int procuctId)
        {
            DBManager db = new DBManager(connecString);

            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(db.CreateParameter("@cartID", cartID, DbType.Int32));
            parameters.Add(db.CreateParameter("@productID", procuctId, DbType.Int32));

            // check hvis item allerede eksistere i Customers cart   
            var cartItemEntry = db.GetScalarValue("CheckIfItemExistsInCart",
                CommandType.StoredProcedure, parameters.ToArray());

            // hvis cartItemEntry ikke er null så eksistere allerede og vi opdater quantity istedet for at tilføje ny item.     
            if (cartItemEntry != null)
            {
                QuantityUpdateAction((int)cartItemEntry, 1);
            }
            else
            {
                AddItemToCart(cartID, procuctId);
            }
            return true;
        }

        // Tilføjer ny vare til kurv 
        public static bool AddItemToCart(int cartID, int procuctId, string storedProc = "AddItemToCart")
        {
            DBManager db = new DBManager(connecString);
            CartItem item = new CartItem(cartID, procuctId);

            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(db.CreateParameter("@cartID", item.cartID, DbType.Int32));
            parameters.Add(db.CreateParameter("@productID", item.productID, DbType.Int32));
            parameters.Add(db.CreateParameter("@quan", item.quantity, DbType.Int32));
            parameters.Add(db.CreateParameter("@dateAdded", item.dateAdded, DbType.DateTime));



            db.Insert(storedProc, CommandType.StoredProcedure, parameters.ToArray());
            return true;
        }

        // Opdatere item quantity  
        public static int UpdateItemQuan(int cartItemEntry, int quan)
        {

            DBManager db = new DBManager(connecString);

            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(db.CreateParameter("@Quan", quan, DbType.Int32));
            parameters.Add(db.CreateParameter("@cartEntryID", cartItemEntry, DbType.Int32));

            int i = (int)db.GetScalarValue("UpdateCartItemQuan", CommandType.StoredProcedure, parameters.ToArray());

            return i;
        }

        // Henter summen af items i customers cart   
        public int GetSumOfCartItems(int cartID, string storedProc = "GetSumOfCartItems")
        {
            DBManager db = new DBManager(connecString);

            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(db.CreateParameter("@cartID", cartID, DbType.Int32));


            int i = 0;
            object valReturned = db.GetScalarValue(storedProc, CommandType.StoredProcedure, parameters.ToArray());

            if (valReturned.GetType() != typeof(DBNull))
            {

                i = (int)valReturned;
            }

            return i;
        }
    }
}