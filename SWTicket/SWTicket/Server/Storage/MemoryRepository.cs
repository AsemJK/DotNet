using System.Collections.Generic;

namespace SWTicket.Server.Storage
{
    public class MemoryRepository<T> : IRepository<T>
    {
        private readonly IList<T> _entities;
        public MemoryRepository()
        {
            _entities = new List<T>();
        }
        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public T Get(int id)
        {
            return _entities[id];
        }

        public System.Collections.Generic.IEnumerable<T> GetAll()
        {
            return _entities;
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void Update(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
