using ConsoleTables;
using OOP_Fundamentals.Interfaces;
using OOP_Fundamentals.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OOP_Fundamentals.Services
{
    public class PrintService : IPrinter
    {
        private static void PrintAllEntities<T>(List<T> entities)
        {
            if (entities.Count == 0)
            {
                return;
            }

            var properties = typeof(T).GetProperties().Where((prop) => !prop.PropertyType.Name.Contains("List"));

            var table = new ConsoleTable(properties.Select((prop) => prop.Name == "CompititionId" ? "Compitition" : prop.Name).ToArray());

            foreach (var entity in entities)
            {
                var values = properties.Select((prop) => GetValue<T>(prop, entity)).ToArray();
                table.AddRow(values);
            }

            table.Write();
        }

        public void Print<T>(List<T> entities)
        {
           
            if (entities.Count == 0)
            {
                Console.WriteLine($"Nothing to print");
                return;
            }

            var books = entities.OfType<Book>().ToList();
            if (books.Count >= 1)
            {
                PrintAllEntities(books);
            }

            var localizedBooks = entities.OfType<LocalizedBook>().ToList();
            if (localizedBooks.Count >= 1)
            {
                PrintAllEntities(localizedBooks);
            }

            var patents = entities.OfType<Patent>().ToList();
            if (patents.Count >= 1)
            {
                PrintAllEntities(patents);
            }
        }

        private static object GetValue<T>(PropertyInfo property, T entity)
        {
            if (property.PropertyType.Name.Equals("DateTime"))
            {
                var time = (DateTime)property.GetValue(entity);
                return time.ToShortDateString();
            }
            else
            {
                return property.GetValue(entity);
            }
        }
    }
}
