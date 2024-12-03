using backend.Dtos;
using backend.Model;
using backend.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure
{
    public class TestRepository: ITestRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Test> _dbSet;

        public TestRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Test>();
        }

        public List<Test> GetAll()
        {
            return _dbSet.Where(t => !t.IsDeleted)
                         .Include(n => n.Questions)
                         .ThenInclude(s => s.Answers)
                         .ToList();
        }

        public Test? GetById(long id)
        {
            return _dbSet.Where(t => !t.IsDeleted)
                         .Include(n => n.Questions)
                         .ThenInclude(s => s.Answers)
                         .FirstOrDefault(n => n.Id == id);

        }

        public Test Update(Test test)
        {
            try
            {
                _dbSet.Update(test);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return test;
        }

        public List<Test> GetPublished()
        {
            return _dbSet.Where(t => !t.IsDeleted && t.IsPublished)
                         .Include(n => n.Questions)
                         .ThenInclude(s => s.Answers)
                         .ToList();
        }
    }
}
