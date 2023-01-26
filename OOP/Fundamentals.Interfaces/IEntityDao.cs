using OOP_Fundamentals.Entities;
using System.Collections.Generic;

namespace OOP_Fundamentals.Interfaces
{
    public interface IEntityDao
    {
        List<Document> GetAll();

        Document GetEntity(int id);

        void Create(Document entity);

        void Update(Document entity);

        void Delete(Document entity);
    }
}
