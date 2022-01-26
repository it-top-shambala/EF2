using System;
using System.Linq;
using EF2.DB_Lib;

namespace EF2.App
{
    internal static class Program
    {
        private static void Main()
        {
            var db = new BookStore();

            foreach (var book in db.TabBooks.ToList())
            {
                Console.WriteLine($"#{book.Id}: {book.Title}, {book.IdAuthorNavigation.LastName} {book.IdAuthorNavigation.FirstName}, {book.IdGenreNavigation.Name}");
            }
        }
    }
}