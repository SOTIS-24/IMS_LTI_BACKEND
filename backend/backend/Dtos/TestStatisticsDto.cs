namespace backend.Dtos
{
    public class TestStatisticsDto
    {
        public TestDto Test {  get; set; }
        public List<AnswerStatisticsDto> AnswerStatistics {  get; set; }
        public int NumberOfStudents { get; set; }
        public float MaxResult { get; set; }
        public float MinResult { get; set; }
    }
}
