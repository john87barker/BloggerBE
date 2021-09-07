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
    private readonly CommentsService _commentsService;

    public ProfilesController(ProfilesService profilesService, BlogsService blogsService, CommentsService commentsService)
    {
      _profilesService = profilesService;
      _blogsService = blogsService;
      _commentsService = commentsService;
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

     [HttpGet("{id}/blogs")]
    public ActionResult<List<Blog>> GetBlogsByProfile(string id)
    {
        try
        {
        List<Blog> blogs = _blogsService.GetBlogsByProfile(id);
        return Ok(blogs);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/comments")]
    public ActionResult<Comment> GetCommentsByProfile(string id)
    {
        try
        {
        Comment comments = _commentsService.GetCommentsByProfile(id);
        return Ok(comments);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
  }
}