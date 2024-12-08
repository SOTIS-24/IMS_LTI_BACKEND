using backend.Dtos;

namespace backend.IServices
{
    public interface ITestResultService
    {
        public bool FinishTest(TestResultCreateDto dto);
        public TestResultDto GetForStudent(string username, long courseId);
    }
}
