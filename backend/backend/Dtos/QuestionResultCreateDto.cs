using backend.Model;

namespace backend.Dtos
{
    public class QuestionResultCreateDto
    {
        public QuestionDto Question { get; set; }
        public List<AnswerDto> Answers { get; set; }
        public int TestId { get; set; }
    }
}
