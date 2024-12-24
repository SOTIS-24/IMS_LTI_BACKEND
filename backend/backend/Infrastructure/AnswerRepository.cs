using backend.Model;
using backend.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Answer> _dbSet;

        public AnswerRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Answer>();
        }
        public void DeleteByQuestionsId(List<long> ids)
        {
            var answers = _dbSet.Where(q => ids.Contains(q.QuestionId)).ToList();
            _dbSet.RemoveRange(answers);
            _context.SaveChanges();
        }
        public List<Answer> GetByIds(List<long> ids)
        {
            return _dbSet.Where(a => ids.Contains(a.Id)).ToList();

        }
    }
}
