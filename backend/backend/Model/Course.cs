namespace backend.Model
{
    public class Course: Entity
    {
        public string Name { get; set; }
        public List<Test> Tests { get; set; }
    }
}
