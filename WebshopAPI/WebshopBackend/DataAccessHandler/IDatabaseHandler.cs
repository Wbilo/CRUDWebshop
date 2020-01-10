using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebshopBackend.Models
{
    /* Lille bibliotek jeg fandt for at kunne lave database kald med helper methods, så jeg ikke behøvede at skrive de samme linjer kode om og om igen 
    */
    // Intet af koden i denne fil er min
    // Kilden er her: http://csharpdocs.com/generic-data-access-layer-in-c-using-factory-pattern/  
    public interface IDatabaseHandler
    {
        IDbConnection CreateConnection();

        void CloseConnection(IDbConnection connection);

        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);

        IDataAdapter CreateAdapter(IDbCommand command);

        IDbDataParameter CreateParameter(IDbCommand command);
    }
}