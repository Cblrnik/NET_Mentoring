using OOP_Fundamentals.Interfaces;
using OOP_Fundamentals.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace OOP_Fundamentals.Dao
{
    public class PatentFileDao : IEntityDao<Patent>
    {
        private readonly List<Patent> _patents;
        private static readonly PatentFileDao _instance;

        public PatentFileDao() 
        {
            _patents = LoaderService.Load<Patent>(typeof(Patent));
        }

        public static PatentFileDao GetInstance()
        {
            return _instance ?? new PatentFileDao();
        }

        public void Create(Patent entity)
        {
            _patents.Add(entity);

            Save(entity);
        }

        public void Delete(Patent entity)
        {
            _patents.Remove(entity);

            File.Delete($"{entity.GetType().Name}_#{entity.Id}.json");
        }

        public List<Patent> GetAll()
        {
            return new List<Patent>(_patents);
        }

        public Patent GetEntity(int id)
        {
            return _patents.Where(patent => patent.Id == id).FirstOrDefault();
        }

        public void Update(Patent entity)
        {
            var updatedBook = _patents.Find(patent => patent.Id == entity.Id);
            var index = _patents.IndexOf(updatedBook);

            _patents.Insert(index, entity);
            _patents.Remove(updatedBook);

            Save(entity);
        }

        private void Save(Patent patent)
        {
            //type_#{number}.json 
            var jsonString = JsonSerializer.Serialize(patent);
            File.WriteAllText($"{patent.GetType().Name}_#{patent.Id}.json", jsonString);
        }
    }
}
