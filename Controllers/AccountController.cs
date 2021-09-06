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
        public async Task<ActionResult<Blog>> GetBlogsByAccount(string id)
        {
            try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Blog blogs = _blogsService.GetBlogsByAccount(id);
        return Ok(blogs);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
        }
    
    
    }
}