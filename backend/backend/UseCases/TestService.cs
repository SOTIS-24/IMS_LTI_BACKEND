using AutoMapper;
using backend.Dtos;
using backend.IServices;
using backend.Model;
using backend.RepositoryInterfaces;
using FluentResults;

namespace backend.UseCases
{
    public class TestService: BaseService<TestDto, Test>, ITestService 
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestService(ITestRepository repository, IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
            _testRepository = repository;
        }

        public List<TestDto> GetAll()
        {
            return MapToDto(_testRepository.GetAll().ToList());
        }

        public TestDto GetById(long id)
        {
            var test = _testRepository.GetById(id);
            if (test == null)
            {
               throw new KeyNotFoundException("Test with ID - {id} not found.");
            }
            return MapToDto(test);
        }
    }
}
