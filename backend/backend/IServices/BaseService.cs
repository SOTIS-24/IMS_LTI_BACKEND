using backend.Model;
using AutoMapper;
using FluentResults;

namespace backend.IServices
{
    public abstract class BaseService<TDto, TDomain> where TDomain : Entity
    {
        private readonly IMapper _mapper;

        protected BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected TDomain MapToDomain(TDto dto)
        {
            return _mapper.Map<TDomain>(dto);
        }

        protected List<TDomain> MapToDomain(List<TDto> dtos)
        {
            return dtos.Select(dto => _mapper.Map<TDomain>(dto)).ToList();
        }

        protected TDto MapToDto(TDomain result)
        {
            return _mapper.Map<TDto>(result);
        }

        protected List<TDto> MapToDto(List<TDomain> result)
        {
            return result.Select(_mapper.Map<TDto>).ToList();
        }
    }
}
