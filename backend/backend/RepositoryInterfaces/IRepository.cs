namespace backend.RepositoryInterfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(long id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(long id);
    }
}
