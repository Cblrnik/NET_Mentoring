using Fundametals.Models;
using OOP_Fundamentals.Entities;
using System.Collections.Generic;

namespace OOP_Fundamentals.Interfaces
{
    public interface IStorage
    {
        void SaveDocument(Document document);

        void UpdateDocument(Document document);

        void DeleteDocument(Document document);

        List<Document> GetAllDocuments(DocumentTypes type);
    }
}
