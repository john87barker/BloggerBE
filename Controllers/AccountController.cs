using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggerBE.Models;
using BloggerBE.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggerBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
    private readonly BlogsService _blogsService;
    private readonly CommentsService _commentsService;

    public AccountController(AccountService accountService, CommentsService commentsService, BlogsService blogsService)
        {
            _accountService = accountService;
            _commentsService = commentsService;
            _blogsService = blogsService;
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
        public async Task<ActionResult<List<Blog>>> GetBlogsByAccount()
        {
            try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        
        List<Blog> blogs = _blogsService.GetBlogsByAccount(userInfo.Id);
        return Ok(blogs);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
        }
    
    [HttpGet("comments")]
        [Authorize]
        public async Task<ActionResult<List<Comment>>> GetCommentsByAccount()
        {
            try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        
        List<Comment> comments = _commentsService.GetCommentsByAccount(userInfo.Id);
        return Ok(comments);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
        }


    [HttpPut]
    [Authorize]

    // TODO Make it so only creator can edit
    public async Task<ActionResult<Account>> EditAccount([FromBody] Account updatedA)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // updatedA.Id = id;
        Account ac = _accountService.EditAccount(updatedA, userInfo.Id);
        return Ok(ac);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    }
}