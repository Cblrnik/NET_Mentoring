using OOP_Fundamentals.Dao;
using OOP_Fundamentals.Dao.Service;
using OOP_Fundamentals.Entities;
using OOP_Fundamentals.Services;
using System;

namespace OOP_Fundamentals
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var book = new Book()
            //{
            //    Id = 1,
            //    ISBN = "9 780997 025491",
            //    Authors = new string[] { "Some", "Author" },
            //    Title = "123564",
            //    DatePublished = DateTime.Now,
            //    NumberOfPages = 10,
            //    Publisher = "Awesome"
            //};

            //var localizedBook = new LocalizedBook()
            //{
            //    Id = 1,
            //    ISBN = "9 780997 025491",
            //    Authors = new string[] { "Some", "Author" },
            //    Title = "123564",
            //    DatePublished = DateTime.Now,
            //    NumberOfPages = 15,
            //    OriginalPublisher = "Publisher",
            //    CountryOfLocalization = "CountryOfLocalization",
            //    LocalPublisher = "LocalPublisher"
            //};

            var storage = FileStorage.GetInstance();
            var consolePrinter = new ConsolePrintService();
            var searchService = new SearchService(storage);

            var lib = new Library(storage, consolePrinter, searchService);

            lib.PrintAllEntities();
            foreach (var item in lib.Search(1))
            {
                Console.WriteLine(item);
            }
        }
    }
}
