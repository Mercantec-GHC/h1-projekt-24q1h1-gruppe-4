using Domain_Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<UsedBooks> GetBooksFromSql(string sql)
        {
            List<UsedBooks> allProducts = new List<UsedBooks>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allProducts.Add(new UsedBooks
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                Condition = reader["Condition"].ToString(),
                                Category = reader["Category"].ToString(),
                                Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0.0m : Convert.ToDecimal(reader["Price"]),
                                ImagePath = reader["ImagePath"].ToString(),
                                Language = reader["Language"].ToString(),
                                ReleaseDate = reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                                Format = reader["Format"].ToString(),
                                ISBN = reader.IsDBNull(reader.GetOrdinal("ISBN")) ? 0L : Convert.ToInt64(reader["ISBN"]),
                                Weight = reader.IsDBNull(reader.GetOrdinal("Weight")) ? 0.0f : Convert.ToSingle(reader["Weight"]),
                                Pages = reader.IsDBNull(reader.GetOrdinal("Pages")) ? 0 : Convert.ToInt32(reader["Pages"]),
                                Description = reader["Description"].ToString(),
                                Type = reader["Type"].ToString(),
                            });
                        }
                    }
                }
            }
            return allProducts;
        }


        public int ExecuteSql(string sql)
        {

            var rowsaffected = -1;
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        rowsaffected = Convert.ToInt32(result);
                    }
                }
            }
            return rowsaffected;
        }

        public List<UsedBooks> GetAllUsedBooks()
        {
            List<UsedBooks> allProducts = new List<UsedBooks>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Books";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allProducts.Add(new UsedBooks
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                Condition = reader["Condition"].ToString(),
                                Category = reader["Category"].ToString(),
                                Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0.0m : Convert.ToDecimal(reader["Price"]),
                                ImagePath = reader["ImagePath"].ToString(),
                                Language = reader["Language"].ToString(),
                                ReleaseDate = reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                                Format = reader["Format"].ToString(),
                                ISBN = reader.IsDBNull(reader.GetOrdinal("ISBN")) ? 0L : Convert.ToInt64(reader["ISBN"]),
                                Weight = reader.IsDBNull(reader.GetOrdinal("Weight")) ? 0.0f : Convert.ToSingle(reader["Weight"]),
                                Pages = reader.IsDBNull(reader.GetOrdinal("Pages")) ? 0 : Convert.ToInt32(reader["Pages"]),
                                Description = reader["Description"].ToString(),
                                Type = reader["Type"].ToString(),
                            });
                        }
                    }
                }
            }
            return allProducts;
        }

        public UsedBooks GetUsedBookById(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Books WHERE Id = @Id";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UsedBooks
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                Condition = reader["Condition"].ToString(),
                                Category = reader["Category"].ToString(),
                                Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0.0m : Convert.ToDecimal(reader["Price"]),
                                ImagePath = reader["ImagePath"].ToString(),
                                Language = reader["Language"].ToString(),
                                ReleaseDate = reader.IsDBNull(reader.GetOrdinal("ReleaseDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                                Format = reader["Format"].ToString(),
                                ISBN = reader.IsDBNull(reader.GetOrdinal("ISBN")) ? 0L : Convert.ToInt64(reader["ISBN"]),
                                Weight = reader.IsDBNull(reader.GetOrdinal("Weight")) ? 0.0f : Convert.ToSingle(reader["Weight"]),
                                Pages = reader.IsDBNull(reader.GetOrdinal("Pages")) ? 0 : Convert.ToInt32(reader["Pages"]),
                                Description = reader["Description"].ToString(),
                                Type = reader["Type"].ToString(),
                            };
                        }
                    }
                }
            }
            return null;
        }


        public List<UsedBooks> GetFirstTwelveBooks()
        {
            List<UsedBooks> books = new List<UsedBooks>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Books LIMIT 12";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new UsedBooks
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0.0m : Convert.ToDecimal(reader["Price"]),
                                ImagePath = reader["ImagePath"].ToString()
                            });
                        }
                    }
                }
            }
            return books;
        }

    }
}



