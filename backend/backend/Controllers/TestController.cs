using backend.Dtos;
using backend.IServices;
using backend.Model;
using backend.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/tests")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _service;
        public TestController(ITestService service)
        {
            _service = service;
        }
        [HttpGet("list/{courseId}")]
        public List<TestDto> Get(long courseId)
        {
            return _service.GetByCourseId(courseId);
        }

        [HttpGet("{id}")]
        public ActionResult<TestDto> GetById(long id)
        {
            try
            {
                var test = _service.GetById(id);
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

        [HttpPost("add")]
        public ActionResult Create([FromBody] TestCreateDto request)
        {
            try
            {
                _service.Create(request);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("update")]
        public ActionResult Update([FromBody] TestDto request)
        {
            try
            {
                _service.Update(request);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("delete")]
        public ActionResult Delete([FromBody] TestDto request)
        {
            try
            {
                _service.Delete(request);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("publish")]
        public ActionResult Publish([FromBody] TestDto request)
        {
            try
            {
                TestDto response = _service.Publish(request);
                if(response != default)
                    return Ok();
                else 
                    return StatusCode(400);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("forStudent/{username}/{courseId}")]
        public ActionResult<TestDto> GetForStudent(string username, long courseId)
        {
            try
            {
                var test = _service.GetForStudent(username, courseId); //change username
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
    }

}
