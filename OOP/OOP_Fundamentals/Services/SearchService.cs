using OOP_Fundamentals.Dao;
using OOP_Fundamentals.Interfaces;
using OOP_Fundamentals.Models;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Fundamentals.Services
{
    public static class SearchService
    {
        private static readonly FileStorage _fileStorage = FileStorage.GetInstance();
        public static IEnumerable<Document> SearchDocumentsByNumber(int number)
        {
            var documents = _fileStorage.GetAllDocuments();
            
            return documents.Where(doc => doc.Id == number);
        }
    }
}
