using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UtterlyComplete.Domain.Common;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase where T : Entity
    {
        private readonly ICrudRepository<T> _repository;

        public BaseController(ICrudRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<T>>> FindAll()
        {
            IReadOnlyList<T> entities = await _repository.FindAllAsync();

            return entities.ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<T>> FindById(int id)
        {
            T? entity = await _repository.FindByIdAsync(id);

            return entity == null ? NotFound() : entity;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<T>> Create([FromBody] T entity)
        {
            // todo: figure out the validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entity = await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return CreatedAtAction(nameof(Create), new { entity.Id }, entity);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, [FromBody] T entity)
        {
            // todo: make it better
            if (id != entity.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Update(entity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            T? entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                return NotFound();

            _repository.Remove(entity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
