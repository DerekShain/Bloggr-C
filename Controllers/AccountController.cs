using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bloggr.Models;
using Bloggr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.Controllers
{
  // ❌GET: '/api/profile/:id' returns users profile
  // ❌GET: '/api/profile/:id/blogs' returns users Blogs
  // ❌GET: '/api/profile/:id/comments' returns users Comments
  // ✅GET: '/account' returns logged in users profile*
  // ✅GET: '/account/blogs' returns logged in users Blogs*
  // ✅GET: '/account/comments' returns logged in users Comments*
  // ❌PUT: '/account' Allows user to edit their own profile**
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly BlogsService _blogsService;
    private readonly CommentsService _commentsService;
    // private readonly ProfilesService _profilesService;

    public AccountController(AccountService accountService, BlogsService blogsService, CommentsService commentsService)
    {
      _accountService = accountService;
      _blogsService = blogsService;
      _commentsService = commentsService;
      //   _profilesService = profilesService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("blogs")]
    [Authorize]
    public async Task<ActionResult<List<Blog>>> GetBlogByAccount()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_blogsService.GetBlogByAccount(userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("comments")]
    [Authorize]
    public async Task<ActionResult<List<Comment>>> GetCommentByAccount()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_commentsService.GetCommentByAccount(userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Account>> Edit([FromBody] Account accountUpdated)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        accountUpdated.Id = userInfo.Id;
        Account account = _accountService.Edit(accountUpdated);
        return Ok(account);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}