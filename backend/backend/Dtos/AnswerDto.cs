namespace backend.Dtos
{
    public class AnswerDto
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public float Points { get; set; }
        public bool IsCorrect { get; set; }
        public long QuestionId { get; set; }
    }
}
