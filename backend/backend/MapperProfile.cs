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
            CreateMap<AnswerCreateDto, Answer>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<QuestionCreateDto, Question>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<TestCreateDto, Test>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<QuestionResultCreateDto, QuestionResult>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<TestResultCreateDto, TestResult>().ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
