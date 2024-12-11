using backend.Dtos;
using backend.IServices;
using backend.Model;
using backend.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/testResults")]
    public class TestResultController : ControllerBase
    {
        private readonly ITestResultService _service;
        public TestResultController(ITestResultService service)
        {
            _service = service;
        }

        [HttpPost("finish")]
        public ActionResult FinishTest([FromBody] TestResultCreateDto request)
        {
            try
            {
                if (_service.FinishTest(request))
                    return Ok();
                else
                    return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("result-details/{username}/{testId}")]
        public ActionResult<TestResultDto> GetResultDetails(string username, long testId)
        {
            try
            {
                var test = _service.GetForStudent(username, testId);
                return Ok(test);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("result-statistics/{testId}")]
        public ActionResult<TestStatisticsDto> GetResultStatistics(long testId)
        {
            try
            {
                var test = _service.GetTestStatistics(testId);
                return Ok(test);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("students/{testId}")]
        public ActionResult<StudentsDto> GetStudentsByTestId(long testId)
        {
            try
            {
                var students = _service.GetStudentsByTestId(testId);
                return Ok(students);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

}
