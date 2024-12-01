namespace backend.Model
{
    public class TestResult : Entity
    {
        public long Id { get; set; }
        public float Points { get; set; }
        public string StudentUsername { get; set; }
        public DateTime DateTime { get; set; }
        public long TestId { get; set; }
        public List<QuestionResult> QuestionResults { get; set; }

        public TestResult() { }
        public bool isValid()
        {
            if(QuestionResults.Count == 0)
                return false;
            foreach (var result in QuestionResults)
            {
                if(result.Answers.Count == 0)
                    return false;
            }
            return true;
        }

        public void setPoints()
        {
            foreach(var result in QuestionResults)
            {
                Points += result.calculatePoints();
            }
        }
    }
}
