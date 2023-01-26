using OOP_Fundamentals.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FileDataAccess;

namespace OOP_Fundamentals.Dao
{
    public class BookFileDao : EntitiesFileDao 
    {
        public BookFileDao()
        {
            _documents = new List<Document>(LoaderService.Load<Book>(typeof(Book)));
        }

        protected override void Save(Document doc)
        {
            var book = doc as Book;
            var jsonString = JsonSerializer.Serialize(book);
            File.WriteAllText($"{book.GetType().Name}_#{book.Id}.json", jsonString);
        }
    }
}
