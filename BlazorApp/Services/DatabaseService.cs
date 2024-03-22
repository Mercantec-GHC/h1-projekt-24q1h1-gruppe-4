using Domain_Models;
using Npgsql;
using System.Buffers;
using System;

namespace BlazorApp.Services
{
    public class DatabaseService
    {
        public string connectionString;

        public DatabaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<UsedBooks> GetAllUsedBooks()
        {
            List<UsedBooks> allProducts = new List<UsedBooks>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT Id, Title, Author, Condition, Category, Price, ImagePath, Language, ReleaseDate, Format, ISBN, Weight, Pages, Description, Stars, Type";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allProducts.Add(new UsedBooks(
                                reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader["Title"].ToString(),
                                reader.IsDBNull(reader.GetOrdinal("Author")) ? null : reader["Author"].ToString(),
                                reader.IsDBNull(reader.GetOrdinal("Condition")) ? null : reader["Condition"].ToString(),
                                reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader["Category"].ToString(),
                                reader.IsDBNull(reader.GetOrdinal("Price")) ? 0.0m : Convert.ToDecimal(reader["Price"]),
                                reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader["ImagePath"].ToString(),
                                reader.IsDBNull(reader.GetOrdinal("Language")) ? null : reader["Language"].ToString(),
                                reader.IsDBNull(reader.GetOrdinal("ReleaseDate")) ? null : reader["ReleaseDate"].ToString(),
                                reader.IsDBNull(reader.GetOrdinal("Format")) ? null : reader["Format"].ToString(),
                                reader.IsDBNull(reader.GetOrdinal("ISBN")) ? 0L : Convert.ToInt64(reader["ISBN"]),
                                reader.IsDBNull(reader.GetOrdinal("Weight")) ? 0.0f : Convert.ToSingle(reader["Weight"]),
                                reader.IsDBNull(reader.GetOrdinal("Pages")) ? 0 : Convert.ToInt32(reader["Pages"]),
                                reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString(),
                                reader.IsDBNull(reader.GetOrdinal("Stars")) ? 0.0f : Convert.ToSingle(reader["Stars"]),
                                reader.IsDBNull(reader.GetOrdinal("Type")) ? null : reader["Type"].ToString()
                                ));
                            
                        }
                        
                    }
                    return allProducts;
                }

            }
        }
    }
}
