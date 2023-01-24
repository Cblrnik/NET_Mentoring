using OOP_Fundamentals.Interfaces;
using OOP_Fundamentals.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace OOP_Fundamentals.Dao
{
    public class LocalizedBookFileDao : IEntityDao<LocalizedBook>
    {
        private readonly List<LocalizedBook> _localizedBooks;
        private static readonly LocalizedBookFileDao _instance;

        private LocalizedBookFileDao()
        {
            _localizedBooks = LoaderService.Load<LocalizedBook>(typeof(LocalizedBook));
        }

        public static LocalizedBookFileDao GetInstance()
        {
            return _instance ?? new LocalizedBookFileDao();
        }

        public void Create(LocalizedBook entity)
        {
            _localizedBooks.Add(entity);

            Save(entity);
        }

        public void Delete(LocalizedBook entity)
        {
            _localizedBooks.Remove(entity);

            File.Delete($"{entity.GetType().Name}_#{entity.Id}.json");
        }

        public List<LocalizedBook> GetAll()
        {
            return new List<LocalizedBook>(_localizedBooks);
        }

        public LocalizedBook GetEntity(int id)
        {
            return _localizedBooks.Where(localizedBook => localizedBook.Id == id).FirstOrDefault();
        }

        public void Update(LocalizedBook entity)
        {
            var updatedBook = _localizedBooks.Find(localizedBook => localizedBook.Id == entity.Id);
            var index = _localizedBooks.IndexOf(updatedBook);

            _localizedBooks.Insert(index, entity);
            _localizedBooks.Remove(updatedBook);

            Save(entity);
        }

        private void Save(LocalizedBook localizedBook)
        {
            //type_#{number}.json 
            var jsonString = JsonSerializer.Serialize(localizedBook);
            File.WriteAllText($"{localizedBook.GetType().Name}_#{localizedBook.Id}.json", jsonString);
        }
    }
}
