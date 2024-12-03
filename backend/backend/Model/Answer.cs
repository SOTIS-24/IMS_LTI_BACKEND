namespace backend.Model
{
    public class Answer: Entity
    {
        public string Text { get; set; }
        public float Points { get; set; }
        public bool IsCorrect { get; set; }
        public long QuestionId {  get; set; }

        public bool IsValidForPublish()
        {
            return Text != null && Points >= 0;
        }
    }
}
