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
            //logika: ako odgovor nosi pozitivne poene - mora biti tacan.
            if (Points > 0 && !IsCorrect)
                return false;

            return Text != null && Text != "" && Points >= 0;
        }
    }
}
