
using CleanArchitecture.Application.Articles;
using CleanArchitecture.Application.Articles.GetArticlesByUserId;
using CleanArchitecture.Application.Articles.UpdateArticlePublish;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Client.Features.Articles.Controllers;

[Route("api/articles")]
[ApiController]
public class ArticlesController(
    ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("by-current-user")]
    public async Task<ActionResult<List<ArticleResponse>>> GetArticlesByCurrentUser()
    {
        var result = await _sender.Send(new GetArticlesByCurrentUserQuery());
        return Ok(result.Value);
    }

    [HttpGet("set-article-publish-state/{articleId}/{isPublished}")]
    public async Task<ActionResult> SetArticlePublishState([FromRoute] int articleId, [FromRoute] bool isPublished)
    {
        var result = await _sender.Send(new UpdateArticlePublishCommand(articleId, isPublished));
        return result.Success ? Ok() : BadRequest();
    }
}
