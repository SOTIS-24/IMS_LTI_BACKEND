using backend.Dtos;
using backend.IServices;
using backend.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/tests")]
    public class TestController: ControllerBase
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


    }

}
