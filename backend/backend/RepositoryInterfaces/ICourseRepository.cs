using backend.Dtos;
using backend.Model;

namespace backend.RepositoryInterfaces
{
    public interface ICourseRepository 
    {
        public List<Course> GetAll();
    }
}
