using backend.Model;

namespace backend.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public float Points { get; set; }
        public List<Answer> Answers { get; set; }
        public long TestId { get; set; }
    }
}
