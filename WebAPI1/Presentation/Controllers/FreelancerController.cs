using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI1.Application.Commands;
using WebAPI1.Application.Queries;

namespace WebAPI1.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreelancerController(IMediator mediator) : ControllerBase
    {
        // GET /api/freelancer
        // GET /api/freelancer/search?keyword=...
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllFreelancersQuery query)
        {
            var result = await mediator.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(e => e.Message));
            }

            return Ok(result.Value);
        }

        // GET /api/freelancer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await mediator.Send(new GetFreelancerByIdQuery { Id = id });
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors.Select(e => e.Message));
        }
        
        // POST /api/freelancer
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFreelancerCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(e => e.Message));
            }
            Guid id = result.Value;
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        // PUT /api/freelancer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFreelancerCommand command)
        {
            command.Id = id;
            var result = await mediator.Send(command);
            return result.IsSuccess ? NoContent() : NotFound(result.Errors.Select(e => e.Message));
        }

        // DELETE /api/freelancer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFreelancerCommand { Id = id };
            var result = await mediator.Send(command);
            return result.IsSuccess ? NoContent() : NotFound(result.Errors.Select(e => e.Message));
        }

        // PATCH /api/freelancer/5/archive
        [HttpPatch("{id}/archive")]
        public async Task<IActionResult> Archive(Guid id, [FromBody] ArchiveFreelancerCommand command)
        {
            command.Id = id;
            var result = await mediator.Send(command);
            return result.IsSuccess ? NoContent() : NotFound(result.Errors.Select(e => e.Message));
        }
    }
}