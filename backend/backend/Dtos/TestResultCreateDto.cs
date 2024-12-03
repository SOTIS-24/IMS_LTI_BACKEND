namespace backend.Dtos
{
    public class TestResultCreateDto
    {
        public int TestId { get; set; }
        public List<QuestionResultCreateDto> QuestionResults { get; set; }
    }
}
