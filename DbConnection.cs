using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TicketTest
{
    internal static class DbConnection
    {
        // Use Lazy<T> to initialize database connection when it is first accessed
        // Lazy<T> class is used to initialize a value lazily, ie it is only created when it is first accessed
        private static readonly Lazy<SqlConnection> lazyConnection = new Lazy<SqlConnection>(() => // lambda expression
        {
            SqlConnection connection = new SqlConnection(getConnectionString()); // create instance of connection
            connection.Open(); // open the connection
            return connection; // return single instance of connection
        });

        // Method to construct and return the connection, string for SQL server database connection
        private static string getConnectionString()
        {
            // Define the values of server host, database name, username, and password.  
            // Values maybe different for different individual's system
            string server = "Server=localhost;";
            string dbName = "Database=yourDB;";
            string username = "User=username;";
            string password = "Password=1234;"; // key in your SQL server password

            // Combine the strings above database connection string
            string connectionString = string.Format("{0}{1}{2}{3}", server, dbName, username, password);
            return connectionString;
        }
        public static SqlConnection Connection => lazyConnection.Value; // access to the lazily initialized database connection

        // Method to be used in other classes for database connection
        public static SqlCommand CreateCommand()
        {
            return Connection.CreateCommand();
        }
    }
}
