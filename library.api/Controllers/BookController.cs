using library.api.Application.Books.Commands;
using library.api.Application.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            var query = new FindBookByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery]GetAllBooksQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("find-by-title/{title}")]
        public async Task<IActionResult> FilterByTitle(string title,
            [FromQuery] FindBooksByTitleQuery query)
        {
            query.QueryString = title;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("find-by-author/{author}")]
        public async Task<IActionResult> FilterByAuthor(string author,
            [FromQuery] FindBooksByTitleQuery query)
        {
            query.QueryString = author;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Successes[0].Message);
            }
            else
            {
                return BadRequest(result.Errors[0].Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteBookCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors[0].Message);
            }
        }

    }
}
