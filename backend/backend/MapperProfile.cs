using AutoMapper;
using backend.Dtos;
using backend.Model;

namespace backend
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<TestDto, Test>().ReverseMap();
            CreateMap<CourseSimpleDto, Course>().ReverseMap();
        }
    }
}
