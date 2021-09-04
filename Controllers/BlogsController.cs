using System;
using System.Collections.Generic;
using BloggerBE.Models;
using BloggerBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggerBE.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BlogsController : ControllerBase
    {
    private readonly BlogsService _blogsService;
    public BlogsController(BlogsService blogsService)
    {
      _blogsService = blogsService;
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
  }
}