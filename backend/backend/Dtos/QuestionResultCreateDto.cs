using backend.Model;

namespace backend.Dtos
{
    public class QuestionResultCreateDto
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
        public int TestId { get; set; }
    }
}
