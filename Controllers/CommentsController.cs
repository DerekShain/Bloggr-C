using System.Threading.Tasks;
using Bloggr.Models;
using Bloggr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CommentsController : ControllerBase
  {
    private readonly CommentsService _commentsService;
    public CommentsController(CommentsService commentsService)
    {
      _commentsService = commentsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Comment>> Post([FromBody] Comment commentData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        commentData.CreatorId = userInfo.Id;
        Comment comment = _commentsService.Post(commentData);
        comment.Creator = userInfo;
        return Ok(comment);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{commentId}")]
    [Authorize]
    public ActionResult<Comment> Edit(int commentId, [FromBody] Comment commentData)
    {
      try
      {
        var comment = _commentsService.Edit(commentId, commentData);
        return Ok(comment);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{commentId}")]
    [Authorize]
    public ActionResult<Comment> Delete(int commentId)
    {
      try
      {
        var comment = _commentsService.Delete(commentId);
        return Ok(comment);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}