using backend.Model;
using backend.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace backend.Infrastructure
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TestResult> _dbSet;

        public TestResultRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TestResult>();
        }

        public void Add(TestResult entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public TestResult GetById(long id)
        {
            return _dbSet.Where(t => t.Id == id)
                        .FirstOrDefault();
        }

        public TestResult? GetByUserAndTest(string username, long testId)
        {
            return _dbSet.Where(t => t.StudentUsername.Equals(username) && t.TestId== testId)
                         .FirstOrDefault();

        }

        public void Update(TestResult entity)
        {
            try
            {
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }
    }
}
