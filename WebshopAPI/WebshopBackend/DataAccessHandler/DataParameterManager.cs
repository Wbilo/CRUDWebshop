using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebshopBackend.Models
{
    /* Lille bibliotek jeg fandt for at kunne lave database kald med helper methods, så jeg ikke behøvede at skrive de samme linjer kode om og om igen 
   */
    // Intet af koden i denne fil er min
    // Kilden er her: http://csharpdocs.com/generic-data-access-layer-in-c-using-factory-pattern/  
    public class DataParameterManager
    {
        public static IDbDataParameter CreateParameter(string providerName, string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            return CreateSqlParameter(name, value, dbType, direction);
        }


        public static IDbDataParameter CreateParameter(string providerName, string name, int size, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            IDbDataParameter parameter = null;

            return CreateSqlParameter(name, size, value, dbType, direction);


            //return parameter;
        }

        private static IDbDataParameter CreateSqlParameter(string name, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

        private static IDbDataParameter CreateSqlParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }


    }
}