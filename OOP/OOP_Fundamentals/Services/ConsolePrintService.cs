using ConsoleTables;
using OOP_Fundamentals.Interfaces;
using OOP_Fundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Fundamentals.Services
{
    public class ConsolePrintService : IPrinter
    {
        private void PrintAllEntities<T>(IEnumerable<T> entities)
        {
            if (entities.Count() == 0)
            {
                return;
            }

            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }

            Console.WriteLine();
        }

        public void Print<Document>(IEnumerable<Document> entities)
        {
            if (entities is null || entities.Count() == 0)
            {
                Console.WriteLine($"Nothing to print");
                return;
            }

            var books = entities.OfType<Book>();
            if (books.Count() >= 1)
            {
                PrintAllEntities(books);
            }

            var localizedBooks = entities.OfType<LocalizedBook>();
            if (localizedBooks.Count() >= 1)
            {
                PrintAllEntities(localizedBooks);
            }

            var patents = entities.OfType<Patent>();
            if (patents.Count() >= 1)
            {
                PrintAllEntities(patents);
            }
        }
    }
}
