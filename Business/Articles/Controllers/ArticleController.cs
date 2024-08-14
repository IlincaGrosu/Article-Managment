using Application.Articles.Commands.Create;
using Application.Articles.Commands.Delete;
using Application.Articles.Commands.Update;
using Application.Articles.Queries.GetAll;
using Application.Articles.Queries.GetById;
using AutoMapper;
using Business.Articles.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Business.Articles.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ArticleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var articles = await _mediator.Send(new GetAllArticleQuery(page, pageSize), cancellationToken);
            return Ok(articles);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new GetArticleByIdQuery(id), cancellationToken);
            return res == null ? NotFound() : Ok(res);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleRequest articleDto, CancellationToken cancellationToken)
        {
            var article = await _mediator.Send(new CreateArticleCommand(articleDto.Title, articleDto.Content, articleDto.PublishedDate), cancellationToken);
            return CreatedAtAction(nameof(Create), article, articleDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateArticleRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateArticleCommand
            {
                Id = request.Id,
                Title = request.Title,
                Content = request.Content,
                PublishedDate = request.PublishedDate
            };

            var article = await _mediator.Send(command, cancellationToken);
            return Ok(article);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] DeleteArticleRequest request, CancellationToken cancellationToken)
        {
            var command = new DeleteArticleCommand
            {
                Id = request.Id
            };
            var res = await _mediator.Send(command, cancellationToken);
            return Ok(res);
        }
    }
}
