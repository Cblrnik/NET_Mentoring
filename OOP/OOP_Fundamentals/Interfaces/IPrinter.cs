using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Fundamentals.Interfaces
{
    public interface IPrinter
    {
        void Print<T>(List<T> entities);
    }
}
