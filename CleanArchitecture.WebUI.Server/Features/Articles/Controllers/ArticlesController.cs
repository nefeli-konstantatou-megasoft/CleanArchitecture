
using CleanArchitecture.Application.Articles;
using CleanArchitecture.Application.Articles.GetArticlesByCurrentUser;
using CleanArchitecture.Application.Articles.GetArticlesByUserName;
using CleanArchitecture.Application.Articles.UpdateArticlePublish;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Infrastructure.Authentication.Authorization;
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
    [AuthorizePermissions(RolePermissionFlags.ManageArticles)]
    public async Task<ActionResult<Result<List<ArticleResponse>>>> GetArticlesByCurrentUser()
    {
        var result = await _sender.Send(new GetArticlesByCurrentUserQuery());
        return result.Success? Ok(result) : BadRequest(result);
    }

    [HttpGet("by-username/{username}")]
    [AuthorizePermissions(RolePermissionFlags.ManageArticles)]
    public async Task<ActionResult<Result<List<ArticleResponse>>>> GetArticlesByUserName([FromRoute] string username)
    {
        var result = await _sender.Send(new GetArticlesByUserNameQuery(username));
        return result.Success? Ok(result) : BadRequest(result);
    }

    [HttpPatch("set-article-publish-state/{articleId}/{isPublished}")]
    [AuthorizePermissions(RolePermissionFlags.ManageArticles)]
    public async Task<ActionResult<Result>> SetArticlePublishState([FromBody] UpdateArticlePublishCommand publishCommand)
    {
        var result = await _sender.Send(publishCommand);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
