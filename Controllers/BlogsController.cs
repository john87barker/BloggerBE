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
    [Route("/api/[controller]")]
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
    public ActionResult<List<Blog>> Get()
    {
      try
      {
        List<Blog> blogs = _blogsService.Get();
        return Ok(blogs);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Blog> Get(int id)
    {
      try
      {
        Blog blog = _blogsService.Get(id);
        return Ok(blog);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    // TODO add GET: '/api/blogs/:id/comments' Returns comments for a blog


    [HttpGet("{id}/comments")]
    public ActionResult<List<Comment>> GetCommentsByBlog(int id)
    {
      try
      {
        List<Comment> bComments = _commentsService.GetCommentsByBlog(id);
        return Ok(bComments);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Blog>> Create([FromBody] Blog newBlog)
    {
     
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newBlog.CreatorId = userInfo.Id;
        Blog makeBlog = _blogsService.Create(newBlog);
        return Ok(makeBlog );
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]

    // TODO Make it so only creator can edit
    public async Task<ActionResult<Blog>> Edit([FromBody] Blog updatedBlog, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedBlog.Id = id;
        Blog blog = _blogsService.Edit(updatedBlog, userInfo.Id);
        return Ok(blog);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete (int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _blogsService.Delete(id, userInfo.Id);
        return Ok("delorted");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}