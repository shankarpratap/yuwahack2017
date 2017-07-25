using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OneWebApp.SqlLibrary
{
    public class TargetDB
    {
        static private List<int> TransientErrorNumbers = new List<int> { 4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001 };

        private int totalNumberOfTimesToTry = 4;

        public int TotalNumberOfTimesToTry
        {
            get { return totalNumberOfTimesToTry; }
            set { totalNumberOfTimesToTry = value; }
        }

        private string server;
        private string databaseName;
        private string username;
        private string password;
        private string connectionString;

        public TargetDB(string aDatabaseName, string aServer = null, string aUsername = null, string aPassword = null)
        {
            this.server = aServer;
            this.databaseName = aDatabaseName;
            this.username = aUsername;
            this.password = aPassword;
        }

        private string GetConnectionString()
        {
            return string.Format(@"Server = tcp:{0},1433; 
                        Initial Catalog = {1}; 
                        Persist Security Info = False; 
                        User ID = {2}; 
                        Password = {3}; 
                        MultipleActiveResultSets = False; 
                        Encrypt = True; 
                        TrustServerCertificate = False; 
                        Connection Timeout = 30;", server, databaseName, username, password);
        }

        private void SafeRollBack(SqlTransaction transaction)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Supressing exception while rolling back transaction  {0}", e.ToString()));
            }
        }

        public ReturnType Execute<ReturnType>(string command, Func<SqlCommand, ReturnType> executeFunc, bool rollBack = false)
        {
            ReturnType result = default(ReturnType);
            SqlTransaction transaction = null;
            string transactionName;
            for (int tries = 1; tries <= totalNumberOfTimesToTry; tries++)
            {
                try
                {
                    transaction = null;
                    transactionName = Guid.NewGuid().ToString().Substring(0, 32);
                    using (var sqlConnection = new SqlConnection
                        (GetConnectionString()))
                    {
                        using (SqlCommand dbCommand = sqlConnection.CreateCommand())
                        {
                            dbCommand.CommandText = command;
                            dbCommand.CommandTimeout = 180; // 3 minutes
                            sqlConnection.Open();
                            if (rollBack)
                            {
                                transaction = sqlConnection.BeginTransaction(transactionName);
                                dbCommand.Transaction = transaction;
                            }

                            result = executeFunc(dbCommand);

                            if (rollBack)
                            {
                                SafeRollBack(transaction);
                            }
                            break;
                        }
                    }
                }
                catch (SqlException sqlExc)
                {
                    if (rollBack)
                    {
                        SafeRollBack(transaction);
                    }
                    if (TransientErrorNumbers.Contains(sqlExc.Number))
                    {
                        Console.WriteLine("{0}: transient error occurred. Will retry", sqlExc.Number);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("{0}: transient(!) error occurred too many times. Won't retry", sqlExc.Number);
                        throw;
                    }
                }
                catch (Exception e)
                {
                    if (rollBack)
                    {
                        SafeRollBack(transaction);
                    }
                    Console.WriteLine("You are screwed, This happened " + Environment.NewLine + e.ToString());
                    throw;
                }
            }
            return result;
        }

        public void ExecuteNonQuery(string command, bool rollBack = false)
        {
            bool success = Execute<bool>(command, (dbCommand) =>
            {
                dbCommand.ExecuteNonQuery();
                return true;
            }, rollBack);
        }

        public List<DataTable> ExecuteQuery(string command, bool rollBack = false)
        {
            return Execute<List<DataTable>>(command, (dbCommand) =>
            {
                List<DataTable> result = new List<DataTable>();

                SqlDataAdapter adapter = new SqlDataAdapter(dbCommand);
                DataSet set = new DataSet();
                adapter.SelectCommand = dbCommand;
                adapter.Fill(set);

                foreach (DataTable t in set.Tables)
                {
                    result.Add(t);
                }
                return result;
            }, rollBack);
        }
    }
}
