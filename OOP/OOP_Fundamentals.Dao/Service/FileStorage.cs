using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using FileDataAccess;
using Fundametals.Models;
using OOP_Fundamentals.Entities;
using OOP_Fundamentals.Interfaces;

namespace OOP_Fundamentals.Dao.Service
{
    public class FileStorage : IStorage
    {
        private readonly List<EntitiesFileDao> _entityDaos;

        private static readonly FileStorage _fileStorage;

        private FileStorage()
        {
            _entityDaos = new List<EntitiesFileDao>();
            Assembly mscorlib = typeof(BookFileDao).Assembly;

            foreach (var item in mscorlib.GetTypes().Where(t => !t.GetTypeInfo().IsDefined(typeof(CompilerGeneratedAttribute), true)))
            {
                if (!item.Name.Equals(GetType().Name))
                {
                    _entityDaos.Add((EntitiesFileDao)Activator.CreateInstance(item));
                }
            }
        }

        public static FileStorage GetInstance()
        {
            return _fileStorage ?? new FileStorage();
        }

        public void DeleteDocument(Document document)
        {
            _entityDaos.Find(dao => dao.GetType().Name.Contains(document.GetType().Name)).Delete(document);
        }

        public List<Document> GetAllDocuments(DocumentTypes type)
        {
            return _entityDaos.Find(dao => dao.GetType().Name.Contains(type.ToString())).GetAll();
        }

        public void SaveDocument(Document document)
        {
            _entityDaos.Find(dao => dao.GetType().Name.Contains(document.GetType().Name)).Create(document);
        }

        public void UpdateDocument(Document document)
        {
            _entityDaos.Find(dao => dao.GetType().Name.Contains(document.GetType().Name)).Update(document);
        }
    }
}
