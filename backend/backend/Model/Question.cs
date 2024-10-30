namespace backend.Model
{
    public class Question: Entity
    {
        public string Text { get; set; }
        public float Points { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
