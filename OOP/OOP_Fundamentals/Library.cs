using Fundamentals.Interfaces;
using Fundametals.Models;
using OOP_Fundamentals.Entities;
using OOP_Fundamentals.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Fundamentals
{
    public class Library
    {
        private readonly IPrinter _printer;
        private readonly IStorage _storage;
        private readonly ISearchService _searchService;

        public Library(IStorage storage, IPrinter printer, ISearchService searchService)
        {
            _storage = storage;
            _printer = printer;
            _searchService = searchService;
        }

        public void PrintAllEntities()
        {
            _printer.Print(_storage.GetAllDocuments(DocumentTypes.Book));
        }

        public List<Document> Search(int id)
        {
            return _searchService.SearchDocumentsByNumber(id).ToList();
        }
    }
}
