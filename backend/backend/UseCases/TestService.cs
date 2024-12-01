using AutoMapper;
using backend.Dtos;
using backend.IServices;
using backend.Model;
using backend.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace backend.UseCases
{
    public class TestService: CrudService<TestDto, Test>, ITestService 
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestService(IRepository<Test> repository, ITestRepository testRepository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _testRepository = testRepository;
        }
        public List<TestDto> GetAll()
        {
            var result = _testRepository.GetAll();
            return MapToDto<TestDto>(result.ToList());
        }

        public TestDto GetById(long id)
        {
            var test = _testRepository.GetById(id);
            if (test == null)
            {
                throw new KeyNotFoundException("Test with ID - {id} not found.");
            }
            return MapToDto<TestDto>(test);
        }

        public void Delete<TestDto>(TestDto dto)
        {
            Test test = MapToDomain<TestDto>(dto);
            test.IsDeleted = true;
            _testRepository.Update(test);
        }

        public TestDto Publish<TestDto>(TestDto dto)
        {
            Test test = MapToDomain<TestDto>(dto);             //Check if user is teacher!!!!! ADD VALIDATION
            if (test.IsPublished)
                return default;

            test.IsPublished = true;
            return MapToDto<TestDto>(_testRepository.Update(test));
        }
    }
}
