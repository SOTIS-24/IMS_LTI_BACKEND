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
        [HttpGet]
        public List<TestDto> Get()
        {
            return _service.GetAll();
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
    }

}
