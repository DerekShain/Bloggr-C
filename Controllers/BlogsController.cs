using System.Collections.Generic;
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
  public class BlogsController : ControllerBase
  {
    private readonly BlogsService _blogsService;
    private readonly CommentsService _commentsService;
    public BlogsController(BlogsService blogsService, CommentsService commentsService)
    {
      _blogsService = blogsService;
      _commentsService = commentsService;
    }

    [HttpGet]
    public ActionResult<List<Blog>> GetBlogs()
    {
      try
      {
        var blogs = _blogsService.GetAll();
        return Ok(blogs);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{blogId}")]
    public ActionResult<Blog> GetBlogById(int blogId)
    {
      try
      {
        var blog = _blogsService.GetById(blogId);
        return Ok(blog);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Blog>> PostAsync([FromBody] Blog blogData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        blogData.CreatorId = userInfo.Id;
        Blog blog = _blogsService.Post(blogData);
        blog.Creator = userInfo;
        return Ok(blog);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{blogId}/comments")]
    public ActionResult<List<Comment>> GetBlogComment(int blogId)
    {
      try
      {
        var comments = _commentsService.GetBlogComment(blogId);
        return Ok(comments);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{blogId}")]
    [Authorize]
    public ActionResult<Blog> Edit(int blogId, [FromBody] Blog blogData)
    {
      try
      {
        var blog = _blogsService.Edit(blogId, blogData);
        return Ok(blog);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{blogId}")]
    [Authorize]
    public ActionResult<Blog> Delete(int blogId)
    {
      try
      {
        var blog = _blogsService.Delete(blogId);
        return Ok(blog);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}