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

        public void Add(Course entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Course GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(Course entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Course> IRepository<Course>.GetAll()
        {
            return _context.Courses;
        }
    }
}
