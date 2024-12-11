namespace backend.Dtos
{
    public class StudentsDto
    {
        public long TestId { get; set; }
        public List<string> Students { get; set; } = new List<string>();
    }
}
