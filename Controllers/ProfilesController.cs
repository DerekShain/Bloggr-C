using System.Collections.Generic;
using Bloggr.Models;
using Bloggr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;
    private readonly BlogsService _blogsService;
    private readonly CommentsService _commentsService;
    public ProfilesController(ProfilesService profilesService, BlogsService blogsService, CommentsService commentsService)
    {
      _profilesService = profilesService;
      _blogsService = blogsService;
      _commentsService = commentsService;
    }
    [HttpGet("{profileId}")]
    public ActionResult<Profile> Get(string profileId)
    {
      try
      {
        Profile profile = _profilesService.Get(profileId);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/blogs")]
    public ActionResult<List<Blog>> GetBlogByProfile(string profileId)
    {
      try
      {
        var profile = _blogsService.GetBlogByProfile(profileId);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/comments")]
    public ActionResult<List<Blog>> GetCommentByProfile(string profileId)
    {
      try
      {
        var profile = _commentsService.GetCommentByProfile(profileId);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}