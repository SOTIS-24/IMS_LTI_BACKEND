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
        private readonly IRepository<TestResult> _testResultRepository;
        private readonly IMapper _mapper;
        public TestResultService(IRepository<TestResult> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _testResultRepository = crudRepository;
            _mapper = mapper;
        }

        public bool FinishTest(TestResultCreateDto dto)
        {
            TestResult result = _mapper.Map<TestResult>(dto);
            if(result.isValid())
            {
                result.StudentUsername = "anja";
                result.DateTime = DateTime.UtcNow;
                result.setPoints();
                _testResultRepository.Add(result);
                return true;
            }
            return false;
        }
    }
}
