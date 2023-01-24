using OOP_Fundamentals.Dao;
using OOP_Fundamentals.Models;
using OOP_Fundamentals.Services;
using System;

namespace OOP_Fundamentals
{
    public class Program
    {
        static void Main(string[] args)
        {
            var book = new Book()
            {
                Id = 1,
                ISBN = "9 780997 025491",
                Authors = new string[] { "Some", "Author" },
                Title = "123564",
                DatePublished = DateTime.Now,
                NumberOfPages = 10,
                Publisher = "Awesome"
            };

            var localizedBook = new LocalizedBook()
            {
                Id = 1,
                ISBN = "9 780997 025491",
                Authors = new string[] { "Some", "Author" },
                Title = "123564",
                DatePublished = DateTime.Now,
                NumberOfPages = 15,
                OriginalPublisher = "Publisher",
                CountryOfLocalization = "CountryOfLocalization",
                LocalPublisher = "LocalPublisher"
            };

            var storage = FileStorage.GetInstance();
            var consolePrinter = new ConsolePrintService();

            //storage.SaveDocument(localizedBook);

            var lib = new Library(storage, consolePrinter);

            lib.PrintAllEntities();

            var docs = SearchService.SearchDocumentsByNumber(2);

            Console.WriteLine("Search:");
            consolePrinter.Print(docs);

        }
    }
}
