using OOP_Fundamentals.Interfaces;
using OOP_Fundamentals.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace OOP_Fundamentals.Dao
{
    public class BookDao : IEntityDao<Book>
    {
        private readonly List<Book> _books;
        private static readonly BookDao _instance;

        private BookDao()
        {
            _books = LoaderService.Load<Book>(typeof(Book));
        }

        public static BookDao GetInstance()
        {
            return _instance ?? new BookDao();
        }

        public void Create(Book entity)
        {
            _books.Add(entity);

            Save(entity);
        }

        public void Delete(Book entity)
        {
            _books.Remove(entity);

            File.Delete($"{entity.GetType().Name}_#{entity.Id}.json");
        }

        public List<Book> GetAll()
        {
            return new List<Book>(_books);
        }

        public Book GetEntity(int id)
        {
            return _books.Where(book => book.Id == id).FirstOrDefault();
        }

        public void Update(Book entity)
        {
            var updatedBook = _books.Find(book => book.Id == entity.Id);
            var index = _books.IndexOf(updatedBook);

            _books.Insert(index, entity);
            _books.Remove(updatedBook);

            Save(entity);
        }

        private void Save(Book book)
        {
            //type_#{number}.json 
            var jsonString = JsonSerializer.Serialize(book);
            File.WriteAllText($"{book.GetType().Name}_#{book.Id}.json", jsonString);
        }
    }
}
