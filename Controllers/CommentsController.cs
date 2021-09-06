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
    [Authorize]
    public class CommentsController : ControllerBase
    {
    private readonly CommentsService _commentsService;

    public CommentsController(CommentsService commentsService)
    {
      _commentsService = commentsService;
    }
// GET only for testing purposes
// [HttpGet]
//     public ActionResult<List<Comment>> Get()
//     {
//       try
//       {
//         List<Comment> comments = _commentsService.Get();
//         return Ok(comments);
//       }
//       catch (Exception err)
//       {
//         return BadRequest(err.Message);
//       }
//     }

    [HttpGet("{id}")]

    public ActionResult<Comment> Get(int id)
    {
      try
      {
        Comment c = _commentsService.Get(id);
        return Ok(c);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]

    public async Task<ActionResult<Comment>> Create([FromBody] Comment newComment)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newComment.CreatorId = userInfo.Id;
        Comment makeComment = _commentsService.Create(newComment);
        return Ok(makeComment);
      }
      catch (Exception err)
      {

        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Comment>> Edit([FromBody] Comment updatedComment, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedComment.Id = id;
        Comment comment = _commentsService.Edit(updatedComment, userInfo.Id);
        return Ok(comment);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _commentsService.Delete(id, userInfo.Id);
        return Ok("delorted comment");
      }
      catch (Exception err)
      {
          return BadRequest(err.Message);
      }
    }



  }
}