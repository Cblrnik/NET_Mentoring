using OOP_Fundamentals.Entities;
using OOP_Fundamentals.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FileDataAccess
{
    public class EntitiesFileDao : IEntityDao
    {
        protected List<Document> _documents;

        public void Create(Document entity)
        {
            _documents.Add(entity);

            Save(entity);
        }

        public void Delete(Document entity)
        {
            _documents.Remove(entity);

            File.Delete($"{entity.GetType().Name}_#{entity.Id}.json");
        }

        public List<Document> GetAll()
        {
            return new List<Document>(_documents);
        }

        public Document GetEntity(int id)
        {
            return _documents.Where(book => book.Id == id).FirstOrDefault();
        }

        public void Update(Document entity)
        {
            var updatedBook = _documents.Find(book => book.Id == entity.Id);
            var index = _documents.IndexOf(updatedBook);

            _documents.Insert(index, entity);
            _documents.Remove(updatedBook);

            Save(entity);
        }

        protected virtual void Save(Document doc)
        {
            var jsonString = JsonSerializer.Serialize(doc);
            File.WriteAllText($"{doc.GetType().Name}_#{doc.Id}.json", jsonString);
        }
    }
}
