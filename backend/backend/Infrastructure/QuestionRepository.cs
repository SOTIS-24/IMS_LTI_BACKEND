using backend.Model;
using backend.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Question> _dbSet;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Question>();
        }
        public void DeleteByTestId(long id)
        {
            var questions = _dbSet.Where(q => q.TestId == id).ToList();
            _dbSet.RemoveRange(questions);
            _context.SaveChanges();
        }

    }
}
