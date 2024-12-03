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
            bool isValid = Text != null && Points > 0;
            if (Answers == null || Answers.Count < 2)
                return false;
            else
            {
                foreach (Answer answer in Answers)
                {
                    isValid = isValid && answer.IsValidForPublish();
                }
            }
            return isValid;
        }
    }
}
