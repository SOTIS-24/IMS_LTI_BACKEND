namespace backend.Model
{
    public class Test: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
        public long CourseId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
    }
}
