using backend.Model;

namespace backend.RepositoryInterfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity GetById(long id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(long id);
    }
}
