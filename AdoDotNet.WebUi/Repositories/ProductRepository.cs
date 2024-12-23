using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoDotNet.WebUi.Helpers;
using AdoDotNet.WebUi.Models;
using Microsoft.Data.Sqlite;

namespace AdoDotNet.WebUi.Repositories
{
    public class ProductRepository
    {
        public IEnumerable<Product> GetAllAsync()
        {
            var products = new List<Product>();

            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, Name, Price, Description FROM Products;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3)
                        });
                    }
                    
                }
                connection.Close();
            }

            return products;
        }
    }
}