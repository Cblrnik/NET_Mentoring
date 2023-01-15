using OOP_Fundamentals.Dao;
using OOP_Fundamentals.Models;
using OOP_Fundamentals.Services;
using System;
using System.Collections.Generic;

namespace OOP_Fundamentals
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var dao = new BookDao();
            var book = new Book()
            {
                Id = 1,
                ISBN = "fiopqufp",
                Authors = new string[] { "Some", "Author" },
                Title = "123564",
                DatePublished = DateTime.Now,
                NumberOfPages = 10,
                Publisher = "Awesome"
            };

            ////dao.Create(book);

            ////dao.Update(book);

            //dao.Delete(book);
            var results = SearchService.SearchDocumentsByNumber(1);
            new PrintService().Print(results);
        }
    }
}
