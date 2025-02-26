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
        public string TeacherUsername { get; set; }

        public bool IsValidForPublish()
        {
            bool isValid = Name != null && Description != null && Name != "" && Description != "" ;

            if (Questions == null || Questions.Count == 0)
                return false;
            else
            {
                foreach (Question question in Questions)
                {
                    isValid = isValid && question.IsValidForPublish();
                }
            }
            return isValid;
        }
    }
}
