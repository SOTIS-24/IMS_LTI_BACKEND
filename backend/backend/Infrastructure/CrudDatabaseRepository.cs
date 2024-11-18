using backend.Model;
using backend.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Explorer.BuildingBlocks.Infrastructure.Database;

public class CrudDatabaseRepository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : Entity
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;
    private readonly DbSet<TEntity> _dbSet;

    public CrudDatabaseRepository(TDbContext dbContext)
    {
        DbContext = dbContext;
        _dbSet = DbContext.Set<TEntity>();
    }

    public TEntity Get(long id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null) throw new KeyNotFoundException("Not found: " + id);
        return entity;
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
        DbContext.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        try
        {
            DbContext.Update(entity);
            DbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
    }

    public void Delete(long id)
    {
        var entity = Get(id);
        _dbSet.Remove(entity);
        DbContext.SaveChanges();
    }

    public List<TEntity> GetAllExpression(Expression<Func<TEntity, bool>> filter, string? include = null)
    {
        IQueryable<TEntity> query = _dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrEmpty(include))
        {
            foreach (var property in include.Split(","))
            {
                query = query.Include(property);
            }
        }
        var entities = query.ToList();
        return entities;
    }

   

    public TEntity GetExpression(Expression<Func<TEntity, bool>> filter, string? include = null)
    {
        IQueryable<TEntity> query = _dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrEmpty(include))
        {
            foreach (var property in include.Split(","))
            {
                query = query.Include(property);
            }
        }
        var entity = query.FirstOrDefault();
        if (entity == null) { throw new KeyNotFoundException("Not found."); }
        return entity;
    }

    public List<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public TEntity GetById(long id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        return entity;
    }




}