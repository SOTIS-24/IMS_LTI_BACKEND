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
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        public TestResultService(ITestResultRepository crudRepository, ITestRepository testRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _testResultRepository = crudRepository;
            _testRepository = testRepository;
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

        public TestResultDto GetForStudent(string username, long courseId)
        {
            return MapToDto<TestResultDto>(_testResultRepository.GetForStudent(username, courseId));
        }

        public TestStatisticsDto GetTestStatistics(long testId)
        {
            TestStatisticsDto result = new TestStatisticsDto();
            result.Test = _mapper.Map<TestDto>(_testRepository.GetById(testId));
            result.Test.Points = result.Test.Questions.Sum(x => x.Points);
            foreach (var question in result.Test.Questions) 
            { 
                foreach(var answer in question.Answers)
                {
                    AnswerStatisticsDto answerStatisticsDto = new AnswerStatisticsDto();
                    answerStatisticsDto.AnswerId = answer.Id;
                    answerStatisticsDto.TestId = testId;
                    answerStatisticsDto.QuestionId = question.Id;
                    answerStatisticsDto.PercentageOfStudents = GetPercentageOfStudentAnswers(testId, question.Id, answer.Id, result);
                    result.AnswerStatistics.Add(answerStatisticsDto);
                }
            }
            return result;
        }

        private double GetPercentageOfStudentAnswers(long testId, long questionId, long answerId, TestStatisticsDto testStatisticsDto)
        {
            double sum = 0;
            List<TestResult> testResults = _testResultRepository.GetByTestId(testId);
            if (testResults != null)
            {
                foreach(var testResult in testResults)
                {
                    QuestionResult questionResult = testResult.QuestionResults.Find(q => q.Question.Id == questionId);
                    var answer = questionResult.Answers.Find(a => a.QuestionId == questionId && a.Id == answerId);
                    if (answer != null)
                        sum++;
                }
            }
            else
            {
                testStatisticsDto.NumberOfStudents = 0;
                return sum;
            }
            testStatisticsDto.NumberOfStudents = testResults.Count;
            if(testStatisticsDto.NumberOfStudents > 0)
                SetMaxAndMinResult(testStatisticsDto, testResults);
            return Math.Round(sum / testResults.Count * 100, 2);
        }
   
        private void SetMaxAndMinResult(TestStatisticsDto testStatisticsDto, List<TestResult> results)
        {
            testStatisticsDto.MaxResult = results.Max(a => a.Points);
            testStatisticsDto.MinResult = results.Min(a => a.Points);
        }
    }
}
