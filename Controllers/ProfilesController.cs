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
    public ProfilesController(ProfilesService profilesService)
    {
      _profilesService = profilesService;
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
  }
}