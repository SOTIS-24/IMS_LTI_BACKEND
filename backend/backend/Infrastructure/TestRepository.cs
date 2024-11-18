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
            return _dbSet.Include(n => n.Questions)
                         .ThenInclude(s => s.Answers)
                         .ToList();
        }

        public Test GetById(long id)
        {
            return _dbSet.Include(n => n.Questions)
                         .ThenInclude(s => s.Answers)
                         .FirstOrDefault(n => n.Id == id);

        }
    }
}
