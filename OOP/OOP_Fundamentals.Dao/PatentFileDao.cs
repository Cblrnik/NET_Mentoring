using OOP_Fundamentals.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FileDataAccess;

namespace OOP_Fundamentals.Dao
{
    public class PatentFileDao : EntitiesFileDao
    {
        public PatentFileDao()
        {
            _documents = new List<Document>(LoaderService.Load<Patent>(typeof(Patent)));
        }

        protected override void Save(Document doc)
        {
            var patent = doc as Patent;
            var jsonString = JsonSerializer.Serialize(patent);
            File.WriteAllText($"{patent.GetType().Name}_#{patent.Id}.json", jsonString);
        }
    }
}
