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

        public bool ValidateUser(string username, string password)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    var result = (long)command.ExecuteScalar();
                    return result > 0;
                }
            }
        }
        public User GetUserByUsername(string username)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT UserID, Username, Email FROM Users WHERE Username = @Username";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString(),

                            };
                        }
                    }
                }
            }
            return null;
        }

    }
}



