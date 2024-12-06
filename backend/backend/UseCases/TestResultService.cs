using backend.Dtos;
using backend.IServices;
using Explorer.BuildingBlocks.Core.UseCases;
using backend.Model;
using backend.RepositoryInterfaces;
using AutoMapper;

namespace backend.UseCases
{
    public class TestResultService : CrudService<TestResultDto, TestResult>, ITestResultService
    {
        private readonly ITestResultRepository _testResultRepository;
        private readonly IMapper _mapper;
        public TestResultService(ITestResultRepository crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _testResultRepository = crudRepository;
            _mapper = mapper;
        }

        public bool FinishTest(TestResultCreateDto dto)
        {
            TestResult result = _mapper.Map<TestResult>(dto);
            if(result.isValid() && !IsTestAlreadyTaken(dto.StudentUsername, dto.TestId))
            {
                result.TestId = dto.TestId;
                result.StudentUsername = dto.StudentUsername;
                result.DateTime = DateTime.UtcNow;
                result.setPoints();
                _testResultRepository.Add(result);
                return true;
            }
            return false;
        }

        private bool IsTestAlreadyTaken(string username, int testId)
        {
            return _testResultRepository.GetByUserAndTest(username, testId) != null;
        }
    }
}
