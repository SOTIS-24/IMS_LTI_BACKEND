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

        public List<Test> GetByCourseId(long courseId)
        {
            return _dbSet.Where(t => !t.IsDeleted && t.CourseId == courseId)
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

        public Test UpdateWithoutQuestions(Test test)
        {
            try
            {
                List<Question> questions = new List<Question>();
                questions.AddRange(test.Questions);
                test.Questions.Clear();
                _dbSet.Update(test);
                _context.SaveChanges();
                test.Questions = questions;
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return test;
        }

        public List<Test> GetPublishedByCourseId(long courseId)
        {
            return _dbSet.Where(t => !t.IsDeleted && t.IsPublished && t.CourseId == courseId)
                         .Include(n => n.Questions)
                         .ThenInclude(s => s.Answers)
                         .ToList();
        }
    }
}
