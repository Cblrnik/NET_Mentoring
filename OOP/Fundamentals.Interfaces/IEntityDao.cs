using System.Collections.Generic;

namespace OOP_Fundamentals.Interfaces
{
    public interface IEntityDao<T> where T : class
    {
        List<T> GetAll();

        T GetEntity(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
