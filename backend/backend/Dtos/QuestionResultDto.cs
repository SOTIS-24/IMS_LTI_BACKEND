using backend.Model;

namespace backend.Dtos
{
    public class QuestionResultDto
    {
        public long Id { get; set; }
        public float Points { get; set; }
        public bool Passed { get; set; }
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
        public long TestResultId { get; set; }
    }
}
