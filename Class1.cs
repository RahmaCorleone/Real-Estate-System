using real_state_app1.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Org.BouncyCastle.Asn1.Mozilla;
using System.IO;
using System.Windows.Forms;
namespace real_state_app1
{
    internal class Class1
    {
        private OracleConnection connection;
        private string connectionString = "Data Source=localhost;User Id=hr;Password=1234;";
        public Class1()
        {
            connection = new OracleConnection(connectionString);
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
        public DataTable GetAllType()
        {
            OracleCommand command = new OracleCommand("SELECT * FROM property_type");
            return GetData(command);
        }
        public DataTable GetAllsale()
        {
            OracleCommand command = new OracleCommand("SELECT * FROM property_sale");
            return GetData(command);
        }
        public DataTable GetData(OracleCommand command)
        {
            DataTable dataTable = new DataTable();
            try
            {
                openConnection();
                command.Connection = connection; // Remove .getConnection
                using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            finally
            {
                closeConnection();
            }

            return dataTable;
        }
        public DataTable getPropertyImages(int propertyID)
        {

            OracleCommand command = new OracleCommand("SELECT * FROM property_images where property_id=:id");
            command.Parameters.Add(":id", OracleDbType.Varchar2).Value = propertyID;
            return GetData(command);
        }
       
        public static DataTable GetAllOwner(string tableName)
        {
            DataTable dataTable = new DataTable();

            using (OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;"))
            {
                string query = $"SELECT  CLIENT_ID,CLIENT_FIRSTNAME,CLIENT_LAST_NAME FROM {tableName}";
                using (OracleDataAdapter adapter = new OracleDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        public static DataTable Getproperties(string tableName)
        {
            DataTable dataTable = new DataTable();

            using (OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;"))
            {
                string query = $"SELECT PROPERTY_ID,PROPERTY_OWNER_ID,PROPERTY_PRICE,PROPERTY_ADDRESS FROM {tableName}";
                using (OracleDataAdapter adapter = new OracleDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        public static DataTable Getsale(string tableName)
        {
            DataTable dataTable = new DataTable();

            using (OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;"))
            {
                string query = $"SELECT * FROM {tableName}";
                using (OracleDataAdapter adapter = new OracleDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
    }
}
