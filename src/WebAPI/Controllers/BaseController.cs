using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UtterlyComplete.ApplicationCore.Models.Common;
using UtterlyComplete.ApplicationCore.Interfaces.Services;
using ErrorOr;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase
        where T : EntityDto
    {
        private readonly IService<T> _service;

        public BaseController(IService<T> service)
        {
            _service = service;
        }

        /// <returns>Erroneous ActionResult from supplied Error</returns>
        protected ActionResult ErrorOf(Error error)
        {
            return error.Type switch
            {
                ErrorType.NotFound => NotFound(),
                ErrorType.Forbidden => UnprocessableEntity(), // todo: maybe here should be forbidden
                ErrorType.Unauthorized => Unauthorized(),
                ErrorType.Validation => BadRequest(ModelState),
                _ => Problem(
                        type: error.Code,
                        title: error.Description,
                        detail: (error.Metadata?.TryGetValue("details", out object? details) ?? false) ? details.ToString() : null)
            };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<T>>> FindAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<T>> FindById(int id)
        {
            ErrorOr<T> dtoOrError = await _service.GetByIdAsync(id);

            if (dtoOrError.IsError)
                return ErrorOf(dtoOrError.FirstError);

            return dtoOrError.Value;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<T>> Create([FromBody] T dto)
        {
            // todo: figure out the validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Create), new { dto.Id }, dto);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<T>> Update(int id, [FromBody] T dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ErrorOr<T> dtoOrError = await _service.UpdateAsync(id, dto);

            if (dtoOrError.IsError)
                return ErrorOf(dtoOrError.FirstError);

            return dtoOrError.Value;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            ErrorOr<Deleted> deletedOrError = await _service.DeleteAsync(id);

            if (deletedOrError.IsError)
                return ErrorOf(deletedOrError.FirstError);

            return NoContent();
        }
    }
}
