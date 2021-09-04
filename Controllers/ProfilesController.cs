using System;
using System.Collections.Generic;
using BloggerBE.Models;
using BloggerBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggerBE.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProfilesController : ControllerBase
    {
    private readonly ProfilesService _profilesService;

    public ProfilesController(ProfilesService profilesService)
    {
      _profilesService = profilesService;
    }

    [HttpGet("{id}")]
    public ActionResult<List<Profile>> Get(int id)
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
  }
}