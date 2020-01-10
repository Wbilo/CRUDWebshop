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
    public class WebshopDataAccess
    {
        private static string connecString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        // Henter alt produkt information 
        public List<Product> GetAllProductInfo(string storedProcedure = "GetAllProdInfo")
        {
            List<Product> prodList = new List<Product>();

            using (SqlConnection con = new SqlConnection(connecString))
            {


                SqlCommand cmd = new SqlCommand(storedProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product prod = new Product();
                    prod.product_id = (int)reader["product_id"];
                    prod.product_name = reader["product_name"].ToString();
                    prod.product_price = (double)reader["product_price"];
                    prod.product_inStock = Convert.ToBoolean(reader["product_inStock"]);
                    prod.product_brand = reader["product_brand"].ToString();
                    prod.product_img = reader["product_img"].ToString();
                    prodList.Add(prod);
                }

                return prodList;

            }
        }

        // Henter alt information om brands 
        public List<Brand> GetAllBrandsInfo(string storedProcedure = "GetAllBrandsInfo")
        {
            List<Brand> brandsList = new List<Brand>();
            //Using sørge for at lukke og dispose forbindelsen  
            using (SqlConnection con = new SqlConnection(connecString))
            {


                SqlCommand cmd = new SqlCommand(storedProcedure, con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Brand brand = new Brand();
                    brand.brand_id = (int)reader["brand_id"];
                    brand.brand_name = (string)reader["brand_name"];

                    brandsList.Add(brand);
                }
                return brandsList;

            }
        }


        // Henter alle produkter fra specificeret brand 
        public List<Product> GetProductsByBrand(string brandName, string storedProcedure = "GetProductsByBrand")
        {
            List<Product> prodList = new List<Product>();
            //Using sørge for at lukke og dispose forbindelsen  
            using (SqlConnection con = new SqlConnection(connecString))
            {


                SqlCommand cmd = new SqlCommand(storedProcedure, con);
                // benytter parameterized SqlCommand for at ungå sql injection 
                cmd.Parameters.Add("@BrandName", SqlDbType.NVarChar).Value = brandName;
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Product prod = new Product();
                    prod.product_id = (int)reader["product_id"];
                    prod.product_name = reader["product_name"].ToString();
                    prod.product_price = (double)reader["product_price"];
                    prod.product_inStock = (bool)reader["product_inStock"];
                    prod.product_brand = reader["product_brand"].ToString();
                    prod.product_img = reader["product_img"].ToString();
                    prodList.Add(prod);
                }

                return prodList;

            }
        }

    }
}