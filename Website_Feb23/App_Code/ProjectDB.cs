using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/*This class will contain most of the methods 
 * for interacting with the database */
namespace database
{
    public class ProjectDB
    {
        public static SqlConnection connectToDB()
        {
            try
            {
                SqlConnection conn = new SqlConnection
                {
                    ConnectionString = @"Data Source=elkdbinstance.cwq52uxsepjm.us-east-1.rds.amazonaws.com;"
                    + "Initial Catalog=Lab4; User ID=elkHost; Password=484pwdCole;"
                };
                conn.Open();
                return conn;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }

    
}