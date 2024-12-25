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
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        public TestResultService(ITestResultRepository crudRepository, ITestRepository testRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _testResultRepository = crudRepository;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public bool FinishTest(TestResultCreateDto dto)
        {
            TestResult result = _mapper.Map<TestResult>(dto);
            List<QuestionResult> questionResults = new List<QuestionResult>();
            foreach (var q in dto.QuestionResults)
            {
                var newQuestionResult = new QuestionResult();
                newQuestionResult.QuestionId = q.Question.Id;
                newQuestionResult.AnswersIds = q.Answers.Select(a => a.Id).ToList();
                newQuestionResult.Points = CalculatePoints(q);
                questionResults.Add(newQuestionResult);
            }
            result.QuestionResults = questionResults;
            if(result.IsValid() && !IsTestAlreadyTaken(dto.StudentUsername, dto.TestId))
            {
                result.TestId = dto.TestId;
                result.StudentUsername = dto.StudentUsername;
                result.DateTime = DateTime.UtcNow;
                result.Points = CalculatePoints(dto);
                _testResultRepository.Add(result);
                return true;
            }
            return false;
        }

        private float CalculatePoints(TestResultCreateDto testResultCreateDto)
        {
            float points = 0;
            foreach (var questionResult in testResultCreateDto.QuestionResults)
            {
                foreach(var answer in  questionResult.Answers)
                {
                    if (answer != null && answer.IsCorrect)
                        points += answer.Points;
                }
            }

            return points;
        }
        private float CalculatePoints(QuestionResultCreateDto questionResultCreateDto)
        {
            float points = 0;
            foreach (var answer in questionResultCreateDto.Answers)
            {
                if (answer != null && answer.IsCorrect)
                    points += answer.Points;
            }
            return points;            
        }

        private bool IsTestAlreadyTaken(string username, int testId)
        {
            return _testResultRepository.GetByUserAndTest(username, testId) != null;
        }

        public TestResultDto GetForStudent(string username, long courseId)
        {
            TestResult result = _testResultRepository.GetForStudent(username, courseId);
            TestResultDto resultDto = new TestResultDto();
            resultDto.Id = (int)result.Id;
            resultDto.DateTime = result.DateTime;
            resultDto.TestId = (int)result.TestId;
            resultDto.StudentUsername = result.StudentUsername;
            resultDto.Points = result.Points;
            resultDto.QuestionResults = new List<QuestionResultDto>();
            foreach(var qr in result.QuestionResults)
            {
                QuestionResultDto questionResultDto = new QuestionResultDto();
                questionResultDto.Question = _questionRepository.GetById(result.QuestionResults.Find(q => q.Id == qr.Id).QuestionId);
                questionResultDto.Answers = _answerRepository.GetByIds(result.QuestionResults.Find(q => q.Id == qr.Id).AnswersIds);
                questionResultDto.Points = qr.Points;
                resultDto.QuestionResults.Add(questionResultDto);
            }
            return resultDto;
        }

        public TestStatisticsDto GetTestStatistics(long testId)
        {
            TestStatisticsDto result = new TestStatisticsDto();
            result.Test = _mapper.Map<TestDto>(_testRepository.GetById(testId));
            result.Test.Points = result.Test.Questions.Sum(x => x.Points);
            result.AnswerStatistics = new List<AnswerStatisticsDto>();
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

        public StudentsDto GetStudentsByTestId(long testId)
        {
            StudentsDto students = new StudentsDto();
            students.TestId = testId;
            students.Students = _testResultRepository.GetStudentsByTestId(testId);
            return students;
        }

        private double GetPercentageOfStudentAnswers(long testId, long questionId, long answerId, TestStatisticsDto testStatisticsDto)
        {
            double sum = 0;
            List<TestResult> testResults = _testResultRepository.GetByTestId(testId);
            if (testResults != null)
            {
                foreach(var testResult in testResults)
                {
                    QuestionResult questionResult = testResult.QuestionResults.Find(q => q.QuestionId == questionId);
                    //Question question = _questionRepository.GetById(questionResult.QuestionId);
                    if(questionResult != null)
                    {
                        List<Answer> answers = _answerRepository.GetByIds(questionResult.AnswersIds);
                        var answer = questionResult.AnswersIds.Find(a => a == answerId);
                        if (answer != null && answer > 0)
                            sum++;
                    }
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
