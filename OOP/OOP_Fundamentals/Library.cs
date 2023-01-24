using OOP_Fundamentals.Interfaces;

namespace OOP_Fundamentals
{
    public class Library
    {
        public IPrinter _printer;
        public IStorage _storage;

        public Library(IStorage storage, IPrinter printer)
        {
            _storage = storage;
            _printer = printer;
        }

        public void PrintAllEntities()
        {
            _printer.Print(_storage.GetAllDocuments());
        }
    }
}
