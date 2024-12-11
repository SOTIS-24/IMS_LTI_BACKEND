namespace backend.Dtos
{
    public class AnswerStatisticsDto
    {
        public long TestId { get; set; }
        public long QuestionId { get; set; }
        public long AnswerId { get; set; }
        public double PercentageOfStudents { get; set; }
    }
}
