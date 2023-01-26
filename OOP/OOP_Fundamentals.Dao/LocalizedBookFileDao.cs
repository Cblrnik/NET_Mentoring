using OOP_Fundamentals.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FileDataAccess;

namespace OOP_Fundamentals.Dao
{
    public class LocalizedBookFileDao : EntitiesFileDao
    {
        public LocalizedBookFileDao()
        {
            _documents = new List<Document>(LoaderService.Load<LocalizedBook>(typeof(LocalizedBook)));
        }

        protected override void Save(Document doc)
        {
            var localizedBook = doc as LocalizedBook;
            var jsonString = JsonSerializer.Serialize(localizedBook);
            File.WriteAllText($"{localizedBook.GetType().Name}_#{localizedBook.Id}.json", jsonString);
        }
    }
}
