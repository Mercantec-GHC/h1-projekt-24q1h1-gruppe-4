using Domain_Models;
using Microsoft.AspNetCore.Mvc;
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
                                ReleaseDate = reader["ReleaseDate"].ToString(),
                                Format = reader["Format"].ToString(),
                                ISBN = reader.IsDBNull(reader.GetOrdinal("ISBN")) ? 0L : Convert.ToInt64(reader["ISBN"]),
                                Weight = reader.IsDBNull(reader.GetOrdinal("Weight")) ? 0.0f : Convert.ToSingle(reader["Weight"]),
                                Pages = reader.IsDBNull(reader.GetOrdinal("Pages")) ? 0 : Convert.ToInt32(reader["Pages"]),
                                Description = reader["Description"].ToString(),
                                //Stars = reader.IsDBNull(reader.GetOrdinal("Stars")) ? 0.0f : Convert.ToSingle(reader["Stars"]),
                                Type = reader["Type"].ToString(),
                                //Reviews = reader["Reviews"] == DBNull.Value ? new List<string>() : ((string[])reader["Reviews"]).ToList(),
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
                                ReleaseDate = reader["ReleaseDate"].ToString(),
                                Format = reader["Format"].ToString(),
                                ISBN = reader.IsDBNull(reader.GetOrdinal("ISBN")) ? 0L : Convert.ToInt64(reader["ISBN"]),
                                Weight = reader.IsDBNull(reader.GetOrdinal("Weight")) ? 0.0f : Convert.ToSingle(reader["Weight"]),
                                Pages = reader.IsDBNull(reader.GetOrdinal("Pages")) ? 0 : Convert.ToInt32(reader["Pages"]),
                                Description = reader["Description"].ToString(),
                                //Stars = reader.IsDBNull(reader.GetOrdinal("Stars")) ? 0.0f : Convert.ToSingle(reader["Stars"]),
                                Type = reader["Type"].ToString(),
                                //Reviews = reader["Reviews"] == DBNull.Value ? new List<string>() : ((string[])reader["Reviews"]).ToList(),
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
                                ReleaseDate = reader["ReleaseDate"].ToString(),
                                Format = reader["Format"].ToString(),
                                ISBN = reader.IsDBNull(reader.GetOrdinal("ISBN")) ? 0L : Convert.ToInt64(reader["ISBN"]),
                                Weight = reader.IsDBNull(reader.GetOrdinal("Weight")) ? 0.0f : Convert.ToSingle(reader["Weight"]),
                                Pages = reader.IsDBNull(reader.GetOrdinal("Pages")) ? 0 : Convert.ToInt32(reader["Pages"]),
                                Description = reader["Description"].ToString(),
                                //Stars = reader.IsDBNull(reader.GetOrdinal("Stars")) ? 0.0f : Convert.ToSingle(reader["Stars"]),
                                Type = reader["Type"].ToString(),
                                Reviews = reader["Reviews"] == DBNull.Value ? new List<string>() : ((string[])reader["Reviews"]).ToList(),
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void AddBook(Book book)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand(@"INSERT INTO Books(Title, Author, Condition, Category, Price, ImagePath, Language, ReleaseDate, ISBN, Pages, Description, UserID)
                 VALUES (@Title, @Author, @Condition, @Category, @Price, @ImagePath, @Language, @ReleaseDate, @ISBN, @Pages, @Description, @UserID)", connection))
                {
                    cmd.Parameters.AddWithValue("Title", book.Title);
                    cmd.Parameters.AddWithValue("Author", book.Author);
                    cmd.Parameters.AddWithValue("Condition", book.Condition);
                    cmd.Parameters.AddWithValue("Category", book.Category);
                    cmd.Parameters.AddWithValue("Price", book.Price);
                    cmd.Parameters.AddWithValue("ImagePath", book.ImagePath);
                    cmd.Parameters.AddWithValue("Language", book.Language);
                    cmd.Parameters.AddWithValue("ReleaseDate", book.ReleaseDate);
                    cmd.Parameters.AddWithValue("ISBN", book.ISBN);
                    cmd.Parameters.AddWithValue("Pages", book.Pages);
                    cmd.Parameters.AddWithValue("Description", book.Description);
                    cmd.Parameters.AddWithValue("UserID", 1); // Erstat med den faktiske bruger-ID
                    cmd.ExecuteNonQuery();
                }
            }
   
        }
    }


}




