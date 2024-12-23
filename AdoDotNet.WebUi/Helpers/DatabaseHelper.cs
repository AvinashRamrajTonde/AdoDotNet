using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace AdoDotNet.WebUi.Helpers
{
    public static class DatabaseHelper
    {
        private static string DatabaseFileName = "AppData.db"; // Normal String

        public static string ConnectionString = $"Data Source={DatabaseFileName};"; // String Interpolation

        public static void EnsureDatabase()
        {
            if (!File.Exists(DatabaseFileName))
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    // Verbatim String
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Products (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Price REAL NOT NULL,
                            Description TEXT
                        );
                    ";
                    command.ExecuteNonQuery();
                }
            }


            
        }

    }
}