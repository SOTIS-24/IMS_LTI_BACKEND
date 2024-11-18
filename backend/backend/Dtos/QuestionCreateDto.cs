using backend.Model;

namespace backend.Dtos
{
    public class QuestionCreateDto
    {   
        public string Text { get; set; }
        public float Points { get; set; }
        public List<AnswerCreateDto> Answers { get; set; }
    }
}
