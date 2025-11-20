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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllFreelancersQuery());
            return Ok(result);
        }

        // GET /api/freelancer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetFreelancerByIdQuery { Id = id };
            var result = await mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        // GET /api/freelancer/search?keyword=...
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var query = new SearchFreelancersQuery { Keyword = keyword };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // POST /api/freelancer
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFreelancerCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = id }, new { id = id });
        }

        // PUT /api/freelancer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFreelancerCommand command)
        {
            command.Id = id;
            var success = await mediator.Send(command);
            return success ? NoContent() : NotFound();
        }

        // DELETE /api/freelancer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteFreelancerCommand { Id = id };
            var success = await mediator.Send(command);
            return success ? NoContent() : NotFound();
        }

        // PATCH /api/freelancer/5/archive
        [HttpPatch("{id}/archive")]
        public async Task<IActionResult> Archive(int id, [FromBody] ArchiveFreelancerCommand command)
        {
            command.Id = id;
            var success = await mediator.Send(command);
            return success ? NoContent() : NotFound();
        }
    }
}