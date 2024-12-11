using backend.Dtos;

namespace backend.IServices
{
    public interface ITestResultService
    {
        public bool FinishTest(TestResultCreateDto dto);
        public TestResultDto GetForStudent(string username, long courseId);
        public TestStatisticsDto GetTestStatistics(long testId);
        public StudentsDto GetStudentsByTestId(long testId);
    }
}
