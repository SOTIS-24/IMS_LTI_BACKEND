using backend.Model;
using AutoMapper;
using FluentResults;

namespace backend.IServices
{
    public abstract class BaseService<TDomain> where TDomain : Entity
    {
        private readonly IMapper _mapper;

        protected BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected TDomain MapToDomain<TDto>(TDto dto)
        {
            return _mapper.Map<TDomain>(dto);
        }

        protected List<TDomain> MapToDomain<TDto>(List<TDto> dtos)
        {
            return dtos.Select(dto => _mapper.Map<TDomain>(dto)).ToList();
        }

        protected TDto MapToDto<TDto>(TDomain result)
        {
            return _mapper.Map<TDto>(result);
        }

        protected List<TDto> MapToDto<TDto>(List<TDomain> result)
        {
            return result.Select(_mapper.Map<TDto>).ToList();
        }
    }
}
