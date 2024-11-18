namespace backend.Dtos
{
    public class AnswerCreateDto
    {
        public string Text { get; set; }
        public float Points { get; set; }
        public bool IsCorrect { get; set; }
    }
}
