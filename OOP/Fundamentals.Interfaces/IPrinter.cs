using OOP_Fundamentals.Entities;
using System.Collections.Generic;

namespace OOP_Fundamentals.Interfaces
{
    public interface IPrinter
    {
        void Print(IEnumerable<Document> entities);
    }
}
