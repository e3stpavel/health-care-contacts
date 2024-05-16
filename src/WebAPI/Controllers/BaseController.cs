using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UtterlyComplete.Domain.Common;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.ApplicationCore.Models.Common;
using AutoMapper;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TEntity, TDto> : ControllerBase
        where TEntity : Entity
        where TDto : EntityDto
    {
        private readonly IMapper _mapper;

        private readonly ICrudRepository<TEntity> _repository;

        public BaseController(IMapper mapper, ICrudRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TDto>>> FindAll()
        {
            IReadOnlyList<TEntity> entities = await _repository.FindAllAsync();

            return _mapper.Map<List<TDto>>(entities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TDto>> FindById(int id)
        {
            TEntity? entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                return NotFound();

            return _mapper.Map<TDto>(entity);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TDto>> Create([FromBody] TDto dto)
        {
            // todo: figure out the validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TEntity entity = _mapper.Map<TEntity>(dto);

            entity = await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            dto = _mapper.Map<TDto>(entity);

            return CreatedAtAction(nameof(Create), new { dto.Id }, dto);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TDto>> Update(int id, [FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TEntity? entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                return NotFound();

            // ignoring `id` being updated
            entity = _mapper.Map(dto with { Id = entity.Id }, entity);

            _repository.Update(entity);
            await _repository.SaveAsync();

            return _mapper.Map<TDto>(entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            TEntity? entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                return NotFound();

            _repository.Remove(entity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
