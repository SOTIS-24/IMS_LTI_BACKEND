using backend.Dtos;
using backend.Model;
using FluentResults;

namespace backend.IServices
{
    public interface ITestService
    {
        public List<TestDto> GetAll();
        public TestDto GetById(long id);

        public void Create<TestCreateDto>(TestCreateDto dto); 

        public void Update<TestDto>(TestDto dto);
        public void Delete<TestDto>(TestDto dto);
    }
}
