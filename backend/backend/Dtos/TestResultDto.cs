using backend.Model;

namespace backend.Dtos
{
    public class TestResultDto
    {
        public int Id { get; set; }
        public float Points { get; set; }
        public string StudentUsername { get; set; }
        public DateTime DateTime { get; set; }
        public int TestId { get; set; }
        public List<QuestionResult> QuestionResults { get; set; }
    }
}
