using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebshopBackend.Models
{
    /* Lille bibliotek jeg fandt for at kunne lave database kald med helper methods, så jeg ikke behøvede at skrive de samme linjer kode om og om igen 
   */
    // Intet af koden i denne fil er min
    // Kilden er her: http://csharpdocs.com/generic-data-access-layer-in-c-using-factory-pattern/  
    public class DatabaseHandlerFactory
    {
        private string connectionStringSettings;

        public DatabaseHandlerFactory(string connectionStringName)
        {
            connectionStringSettings = connectionStringName;
        }

        public IDatabaseHandler CreateDatabase()
        {
            IDatabaseHandler database = null;


            database = new SqlDataAccess(connectionStringSettings);

            return database;
        }

        public string GetProviderName()
        {
            return connectionStringSettings;
        }
    }
}