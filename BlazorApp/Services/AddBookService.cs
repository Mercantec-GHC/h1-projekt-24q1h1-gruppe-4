/*using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace BlazorApp.Services;


using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Domain_Models;
using NpgsqlTypes;
using System.Collections;

public class AddBookService : Controller
{
    private readonly string _connectionString = "Host=ep-royal-dust-a22iq0yc.eu-central-1.aws.neon.tech;Port=5432;Username=UsedBooks_owner;Password=7UO0kgQJFsLP;Database=UsedBooks;SSLMode=Require";


    [HttpPost]
    public IActionResult Addbook(UsedBooks book)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (var cmd = new NpgsqlCommand(@"INSERT INTO Books(Title, Author, Condition, Category, Price, ImagePath, Language, ReleaseDate, ISBN, Pages, Description, UerID)
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
                //cmd.Parameters.AddWithValue("Format", book.Format);
                
                
               


                cmd.Parameters.AddWithValue("ISBN", book.ISBN);
                //cmd.Parameters.AddWithValue("Weight", book.Weight);
                cmd.Parameters.AddWithValue("Pages", book.Pages);
                cmd.Parameters.AddWithValue("Description", book.Description);
                cmd.Parameters.AddWithValue("UserID", 1);
                //cmd.Parameters.AddWithValue("Type", book.Type);
                cmd.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Home");
    }
}
*/
