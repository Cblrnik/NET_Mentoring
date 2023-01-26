using ConsoleTables;
using OOP_Fundamentals.Interfaces;
using OOP_Fundamentals.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace OOP_Fundamentals.Services
{
    public class ConsolePrintService : IPrinter
    {
        public void Print(IEnumerable<Document> entities)
        {
            if (entities is null || !entities.Any())
            {
                Console.WriteLine($"Nothing to print");
                return;
            }

            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }

            Console.WriteLine();
        }
    }
}
