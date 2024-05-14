using AutoMapper;
using ErrorOr;
using UtterlyComplete.ApplicationCore.Errors;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.ApplicationCore.Interfaces.Services;
using UtterlyComplete.ApplicationCore.Models.Common;
using UtterlyComplete.Domain.Common;

namespace UtterlyComplete.ApplicationCore.Services
{
    public abstract class BaseService<TEntity, TDto> : IService<TDto>
        where TEntity : Entity
        where TDto : EntityDto
    {
        private readonly IMapper _mapper;

        private readonly ICrudRepository<TEntity> _repository;

        public BaseService(IMapper mapper, ICrudRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ErrorOr<TDto>> GetByIdAsync(int id)
        {
            TEntity? entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                return UserErrors.EntityNotFound($"{typeof(TEntity).Name} was not found ('{id}')");

            return _mapper.Map<TDto>(entity);
        }

        public async Task<IReadOnlyList<TDto>> GetAllAsync()
        {
            IReadOnlyList<TEntity> entities = await _repository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TDto>>(entities);
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);

            entity = await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return _mapper.Map<TDto>(entity);
        }

        public async Task<ErrorOr<TDto>> UpdateAsync(int id, TDto dto)
        {
            TEntity? entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                return UserErrors.EntityNotFound($"{typeof(TEntity).Name} was not found ('{id}')");

            if (dto.Id != id)
                return UserErrors.IllegalOperation($"You are not allowed to modify {typeof(TEntity).Name} id");

            entity = _mapper.Map(dto, entity);

            _repository.Update(entity);
            await _repository.SaveAsync();

            return _mapper.Map<TDto>(entity);
        }

        public async Task<ErrorOr<Deleted>> DeleteAsync(int id)
        {
            TEntity? entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                return UserErrors.EntityNotFound($"{typeof(TEntity).Name} was not found ('{id}')");

            _repository.Remove(entity);
            await _repository.SaveAsync();

            return Result.Deleted;
        }
    }
}
