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

        // Get All Products
        public List<Product> GetAll()
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

        // Get Product By ID
        public Product? GetById(int productId)
        {
            Product? product = null;

            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                // Parameterized SQL Query
                command.CommandText = $"SELECT Id, Name, Price, Description FROM Products WHERE {productId};";

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3)
                        };
                    }

                }
                connection.Close();
            }

            return product ?? new Product();
        }

        // Add Product
        public void AddProduct(Product product)
        {
            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                // Parameterized SQL Query
                command.CommandText = @"INSERT INTO Products (Name, Price, Description) 
                                        VALUES (@Name, @Price, @Description))";

                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Description", product.Description);

                command.ExecuteNonQuery();
                connection.Close();
            }

        }


        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product)
        {
            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"UPDATE Products 
                                    SET Name = @Name, Price = @Price, Description = @Description
                                    WHERE Id = @Id";

                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Id", product.Id);

                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        /// <summary>
        /// Deletes a product from Database.
        /// </summary>
        /// <param name="productId">Represents ID of a product.</param>
        public void DeleteProduct(int productId)
        {
            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"DELETE FROM Products WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", productId);

                command.ExecuteNonQuery();
                connection.Close();
            }

        }



    }
}