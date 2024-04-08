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

        public async Task<bool> ValidateUserAsync(int userId, string password)
        {
            string sql = "SELECT password FROM Users WHERE userid = @userId";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();


                using (var command = new NpgsqlCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@userId", userId);


                    var result = await command.ExecuteScalarAsync();


                    if (result != null)
                    {

                        if (result.ToString() == password)
                        {
                            return true; // Passwords match
                        }
                    }
                }
            }

            return false; // No matching user or password
        }


        public List<Book> GetAllUserBooks(int UserID)
        {
            List<Book> userBooks = new List<Book>();


            string sql = "SELECT * FROM Books WHERE UserId = @UserId";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            Book userBook = new Book
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
                                //UserId = Convert.ToInt32(reader["userid"])
                               
                            };

                            
                            userBooks.Add(userBook);
                        }
                    }
                }
            }

            return userBooks;
        }

        public async Task UpdateBookAsync(Book itemToUpdate)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(@"UPDATE Books
                                                        SET Title = @Title, Author = @Author, Condition = @Condition, Category = @Category, Price = @Price, 
                                                        ImagePath = @ImagePath, Language = @Language, ReleaseDate = @ReleaseDate, ISBN = @ISBN, Pages = @Pages, 
                                                        Description = @Description
                                                        WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", itemToUpdate.Id);
                    cmd.Parameters.AddWithValue("@Title", itemToUpdate.Title);
                    cmd.Parameters.AddWithValue("@Author", itemToUpdate.Author);
                    cmd.Parameters.AddWithValue("@Condition", itemToUpdate.Condition);
                    cmd.Parameters.AddWithValue("@Category", itemToUpdate.Category);
                    cmd.Parameters.AddWithValue("@Price", itemToUpdate.Price);
                    cmd.Parameters.AddWithValue("@ImagePath", itemToUpdate.ImagePath);
                    cmd.Parameters.AddWithValue("@Language", itemToUpdate.Language);
                    cmd.Parameters.AddWithValue("@ReleaseDate", itemToUpdate.ReleaseDate);
                    cmd.Parameters.AddWithValue("@ISBN", itemToUpdate.ISBN);
                    cmd.Parameters.AddWithValue("@Pages", itemToUpdate.Pages);
                    cmd.Parameters.AddWithValue("@Description", itemToUpdate.Description);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public async Task DeleteBookAsync(int bookId)
        {

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(@"DELETE FROM Books WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", bookId); ;

                    cmd.ExecuteNonQuery();
                }
            }

        }

    }
}



