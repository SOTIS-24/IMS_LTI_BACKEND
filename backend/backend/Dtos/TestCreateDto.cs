using backend.Model;

namespace backend.Dtos
{
    public class TestCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionCreateDto> Questions { get; set; }
        //public int? CourseId { get; set; }
    }
}
