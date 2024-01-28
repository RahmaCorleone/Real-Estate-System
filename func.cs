using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Transactions;
using System.Data.Common;

namespace real_state_app1.classes
{
    internal class func
    {
        
        private OracleConnection connection;
        private string connectionString = "Data Source=localhost;User Id=hr;Password=1234;";

        public func()
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

        // Insert data into the database
        // Add these methods to handle transactions
        private OracleTransaction transaction; // Add this line

        // ... other methods ...

        // Add this method to begin a transaction
        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }
        private void CommitTransaction()
        {
            if (transaction != null)
            {
                transaction.Commit();
                transaction = null;
            }
        }

        private void RollbackTransaction()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }
        }
        public Boolean InsertData(OracleCommand command)
        {
            command.Connection = connection;

            try
            {
                connection.Open();

                // Begin the transaction
                BeginTransaction();

                // Execute the insert command
                command.ExecuteNonQuery();

                // Commit the transaction
                CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                // Rollback the transaction in case of an exception
                RollbackTransaction();

                return false;
            }
            finally
            {
                connection.Close();
            }
        }



        // Update data in the database
        public void UpdateData(int userId, string newPassword)
        {
            try
            {
                openConnection();
                string query = "UPDATE user_Data SET password = :newPassword WHERE user_id = :userId";
                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    cmd.Parameters.Add(":newPassword", OracleDbType.Varchar2).Value = newPassword;
                    cmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                closeConnection();
            }
        }

        // Delete data from the database
        public void DeleteData(int userId)
        {
            try
            {
                openConnection();
                string query = "DELETE FROM user_Data WHERE user_id = :userId";
                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    cmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                closeConnection();
            }
        }

        // Get data from the database
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

    }
}
    
