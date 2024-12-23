using backend.Dtos;
using backend.Model;
using FluentResults;

namespace backend.IServices
{
    public interface ITestService
    {
        public List<TestDto> GetByCourseId(long courseId);
        public TestDto GetById(long id);

        public void Create<TestCreateDto>(TestCreateDto dto); 

        public void Update<TestDto>(TestDto dto);
        public void Delete<TestDto>(TestDto dto);
        public TestDto Publish<TestDto>(TestDto dto);
        public List<TestDto> GetForStudent(string username, long courseId);
        public TestDto UpdateWithQuestions<TestDto>(TestDto dto);
    }
}
