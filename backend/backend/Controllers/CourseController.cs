using backend.Dtos;
using backend.IServices;
using backend.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        public CourseController(ICourseService service)
        {
            _service = service;
        }
        [HttpGet]
        public List<CourseSimpleDto> Get()
        {
            return _service.GetAll();
        }

    }

}
