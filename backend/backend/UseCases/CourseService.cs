using AutoMapper;
using backend.Dtos;
using backend.IServices;
using backend.Model;
using backend.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace backend.UseCases
{
    public class CourseService : CrudService<CourseSimpleDto, Course>, ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(IRepository<Course> repository, ICourseRepository courseRepository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public List<CourseSimpleDto> GetAll()
        {
            var result = _courseRepository.GetAll();
            return MapToDto<CourseSimpleDto>(result.ToList());
        }
    }
}
