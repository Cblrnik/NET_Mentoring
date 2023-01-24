using System.Collections.Generic;

namespace OOP_Fundamentals.Interfaces
{
    public interface IPrinter
    {
        void Print<T>(IEnumerable<T> entities);
    }
}
