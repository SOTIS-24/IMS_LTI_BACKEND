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
    }
}
