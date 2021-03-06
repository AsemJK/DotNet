using System.Collections.Generic;

namespace BlazorTicketSystem.Server.Storage
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAll();
        T Get(int id);
    }
}
