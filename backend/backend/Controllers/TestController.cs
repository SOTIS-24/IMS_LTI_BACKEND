using backend.Dtos;
using backend.IServices;
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
    }

}
