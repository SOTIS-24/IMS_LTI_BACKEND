using AutoMapper;
using backend.Dtos;
using backend.IServices;
using backend.Model;
using backend.RepositoryInterfaces;
using FluentResults;

namespace backend.UseCases
{
    public class CourseService : BaseService<CourseSimpleDto, Course>, ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository repository, IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
            _courseRepository = repository;
        }

        public List<CourseSimpleDto> GetAll()
        {
            return MapToDto(_courseRepository.GetAll().ToList());
        }
    }
}
