using Domain_Models;
using Npgsql;
using System.Buffers;
using System;

namespace Domain_Models
{
    public class UsedBooks
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Condition { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
        public string? Language { get; set; }
        public string? ReleaseDate { get; set; } 
        public string? Format { get; set; }
        public long ISBN { get; set; }
        public float Weight { get; set; }
        public int Pages { get; set; }
        public string? Description { get; set; }
        public float Stars { get; set; }
        public List<string> Reviews { get; set; } = new List<string>(); 
        public string? Type { get; set; }

        public UsedBooks(string? Title, string? Author, string? Condition, string? Category, decimal Price, string? ImagePath, string? Language, string? ReleaseDate, string? Format, long ISBN, float Weight, int Pages, string? Description, float Stars, string? Type)
        {
            this.Title = Title;
            this.Author = Author;
            this.Condition = Condition;
            this.Category = Category;
            this.Price = Price;
            this.ImagePath = ImagePath;
            this.Language = Language;
            this.ReleaseDate = ReleaseDate;
            this.Format = Format;
            this.ISBN = ISBN;
            this.Weight = Weight;
            this.Pages = Pages;
            this.Description = Description;
            this.Stars = Stars;
            this.Type = Type;
        }

    }
    public class user
    {
        string name;
        int id;
        string login;
        string email;
    }

    public class Book
    {
        int id;
        int owner_id;
        string wear;
        double price;
        string name;
        string genre;
    }

}