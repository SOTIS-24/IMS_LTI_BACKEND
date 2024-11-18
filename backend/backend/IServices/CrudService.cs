using AutoMapper;
using backend.Dtos;
using backend.IServices;
using backend.Model;
using backend.RepositoryInterfaces;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Explorer.BuildingBlocks.Core.UseCases;

/// <summary>
/// A base service class that offers CRUD methods for persisting TDomain objects, based on the passed TDto object.
/// </summary>
/// <typeparam name="TDto">Type of output data transfer object.</typeparam>
/// <typeparam name="TDomain">Type of domain object that maps to TDto</typeparam>
public abstract class CrudService<TDto, TDomain> : BaseService<TDomain> where TDomain : Entity
{
    protected readonly IRepository<TDomain> CrudRepository;
    private IMapper mapper;

    protected CrudService(IRepository<TDomain> crudRepository, IMapper mapper) : base(mapper)
    {
        CrudRepository = crudRepository;
    }

    

    public TDto GetById(long id)
    {
        try
        {
            var result = CrudRepository.GetById(id);
            return MapToDto<TDto>(result);
        }
        catch (KeyNotFoundException e)
        {
            throw new Exception(e.Message);
        }
    }

    

    public virtual void Create<PDto>(PDto entity)
    {
        try
        {
            TDomain dom = MapToDomain<PDto>(entity);
            CrudRepository.Add(dom);
           
        }
        catch (ArgumentException e)
        {
            Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public virtual void Update<PDto>(PDto entity)
    {
        try
        {
            CrudRepository.Update(MapToDomain<PDto>(entity));
            
        }
        catch (KeyNotFoundException e)
        {
            Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public virtual Result Delete(long id)
    {
        try
        {
            CrudRepository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}