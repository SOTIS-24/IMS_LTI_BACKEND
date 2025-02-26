namespace backend.Model
{
    public class Question: Entity
    {
        public string Text { get; set; }
        public float Points { get; set; }
        public List<Answer> Answers { get; set; }
        public long TestId { get; set; }

        public bool IsValidForPublish()
        {
            bool isValid = Text != null && Text != "" && Points > 0;

            double answersSumPoints = 0;
            if (Answers == null || Answers.Count < 2)
                return false;
            else
            {
                foreach (Answer answer in Answers)
                {
                    isValid = isValid && answer.IsValidForPublish();
                    answersSumPoints += answer.Points;
                }
            }

            if (answersSumPoints != Points)
                return false;

            return isValid;
        }
    }
}
