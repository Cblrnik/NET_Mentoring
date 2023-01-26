using OOP_Fundamentals.Dao;
using OOP_Fundamentals.Interfaces;
using OOP_Fundamentals.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using Fundametals.Models;
using Fundamentals.Interfaces;

namespace OOP_Fundamentals.Services
{
    public class SearchService : ISearchService
    {
        private readonly IStorage _storage;

        public SearchService(IStorage storage)
        {
            _storage = storage;
        }
        
        public IEnumerable<Document> SearchDocumentsByNumber(int id)
        {
            var documents = new List<Document>();
            foreach (var item in Enum.GetValues(typeof(DocumentTypes)))
            {
                documents.AddRange(_storage.GetAllDocuments((DocumentTypes)item));
            }
            
            return documents.Where(doc => doc.Id == id);
        }
    }
}
