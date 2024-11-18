using backend.Dtos;
using backend.Model;
using backend.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Course> _dbSet;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Course>();
        }
        public List<Course> GetAll()
        {
            return _dbSet.ToList();
        }


    }
}
