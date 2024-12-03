namespace backend.Model
{
    public class QuestionResult: Entity
    {
        public long Id { get; set; }
        public float Points { get; set; }
        public bool Passed { get; set; }
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
        public long TestResultId { get; set; }

        public float calculatePoints()
        {
            foreach (var answer in Answers)
            {
                if(answer.IsCorrect)
                    Points += answer.Points;
            }
            return Points;
        }
    }
}
