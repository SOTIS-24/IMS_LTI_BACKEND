using backend.Dtos;

namespace backend.IServices
{
    public interface ICourseService
    {
        public List<CourseSimpleDto> GetAll();
        
    }
}
