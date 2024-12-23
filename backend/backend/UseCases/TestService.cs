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
        private readonly ITestResultRepository _testResultRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public TestService(IRepository<Test> repository, ITestRepository testRepository, ITestResultRepository testResultRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _testRepository = testRepository;
            _testResultRepository = testResultRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }
        public List<TestDto> GetByCourseId(long courseId)
        {
            var result = _testRepository.GetByCourseId(courseId);
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
            Test test = MapToDomain<TestDto>(dto);             
            if (test.IsPublished || !test.IsValidForPublish())
                return default;

            test.IsPublished = true;
            return MapToDto<TestDto>(_testRepository.UpdateWithoutQuestions(test));
        }

        public TestDto UpdateWithQuestions<TestDto>(TestDto dto)
        {
            Test test = MapToDomain<TestDto>(dto);             
            if (test.IsPublished)
                return default;

            _answerRepository.DeleteByQuestionsId(test.Questions.Select(q => q.Id).ToList());
            _questionRepository.DeleteByTestId(test.Id);
            return MapToDto<TestDto>(_testRepository.Update(test));
        }

        public List<TestDto> GetForStudent(string username, long courseId)
        {
            List<Test> publishedTests = _testRepository.GetPublishedByCourseId(courseId);
            List<Test> testsForStudent = new List<Test>();
            foreach (var test in publishedTests)
            {
                if(!IsTestAlreadyTaken(username, Convert.ToInt32(test.Id)))
                    testsForStudent.Add(test);
            }
            return MapToDto<TestDto>(testsForStudent);
        }

        private bool IsTestAlreadyTaken(string username, int testId)
        {
            return _testResultRepository.GetByUserAndTest(username, testId) != null;
        }
    }
}
