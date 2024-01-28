using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace real_state_app1.classes
{
    internal class property_type
    {
        func func = new func();

        
        public bool InsertType(string name,string description)
        {
            OracleCommand command = new OracleCommand("INSERT INTO PROPERTY_TYPE (PROPERTY_NAME, PROPERTY_DESCRIPTION) VALUES (:NAME, :DESCP)");

            // Assuming that you have appropriate property values to insert
            command.Parameters.Add(":NAME", OracleDbType.Varchar2).Value = name;
            command.Parameters.Add(":DESCP", OracleDbType.Clob).Value = description;

            return func.InsertData(command);
        }
        public DataTable GetAllType()
        {
            OracleCommand command = new OracleCommand("SELECT * FROM property_type");
            return func.GetData(command);
        }
        public static DataTable GetAllOwner(string tableName)
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
        public static DataTable GetApartOwner(string tableName)
        {
            DataTable dataTable = new DataTable();

            using (OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;"))
            {
                string query = $"SELECT owner_id,owner_firstname,owner_last_name FROM {tableName}";
                using (OracleDataAdapter adapter = new OracleDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        public static DataTable Getimageproperty(string tableName)
        {
            DataTable dataTable = new DataTable();

            using (OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;"))
            {
                string query = $"SELECT property_id,property_type,property_address FROM {tableName}";
                using (OracleDataAdapter adapter = new OracleDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        public DataTable GetAllproperty()
        {
            OracleCommand command = new OracleCommand("SELECT * FROM the_property");
            return func.GetData(command);
        }
        
        public DataTable getPropertyById(string id)
        {
            OracleCommand command = new OracleCommand("SELECT * FROM the_property where property_id = :id");
          
            return func.GetData(command);
        }

    }

}
