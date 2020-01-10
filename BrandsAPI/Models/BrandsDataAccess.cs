using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BrandsAPI.Models
{
    public class BrandsDataAccess
    {
        private static string connecString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        // Henter information om et brand 
        public Brand GetBrandInfo(string brandName, string storedProcedure = "GetBrandInfo")
        {
            using (SqlConnection con = new SqlConnection(connecString))
            {
                Brand brand = new Brand();

                SqlCommand cmd = new SqlCommand(storedProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BrandName", SqlDbType.VarChar).Value = brandName;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    brand.brand_id = (int)reader["brand_id"];
                    brand.brand_name = reader["brand_name"].ToString();
                    brand.brand_banner = reader["brand_banner"].ToString();
                    brand.brand_info = reader["brand_info"].ToString();
                }

                return brand;

            }
        }
    }

}