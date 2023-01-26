using OOP_Fundamentals.Entities;
using System.Collections.Generic;

namespace Fundamentals.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<Document> SearchDocumentsByNumber(int id);
    }
}
