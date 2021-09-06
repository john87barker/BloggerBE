using System;
using System.Collections.Generic;
using BloggerBE.Models;
using BloggerBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggerBE.Controllers
{
    [ApiController]
    [Route("/api/profile")]
    public class ProfilesController : ControllerBase
    {
    private readonly ProfilesService _profilesService;
    private readonly BlogsService _blogsService;

    public ProfilesController(ProfilesService profilesService, BlogsService blogsService)
    {
      _profilesService = profilesService;
      _blogsService = blogsService;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
        try
        {
        Profile profile = _profilesService.Get(id);
        return Ok(profile);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }



     [HttpGet("{id}/{blogs}")]
    public ActionResult<Blog> GetBlogsByProfile(string id)
    {
        try
        {
        Blog blogs = _blogsService.GetBlogsByProfile(id);
        return Ok(blogs);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
  }
}