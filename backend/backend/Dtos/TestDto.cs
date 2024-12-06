using backend.Model;

namespace backend.Dtos
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }  
        public int CourseId { get; set; }
        public bool IsPublished { get; set; }
    }
}
