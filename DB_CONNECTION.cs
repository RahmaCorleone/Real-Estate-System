using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
namespace real_state_app1.classes
{
    internal class DB_CONNECTION
    {
        OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234");

        // Your class code goes here

        public OracleConnection getConnection
        {
            get
            {
                return connection;
            }
        }

        public void openConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
