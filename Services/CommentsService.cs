using System;
using System.Collections.Generic;
using BloggerBE.Models;
using BloggerBE.Repositories;

namespace BloggerBE.Services
{
  public class CommentsService
  {
    private readonly CommentsRepository _repo;

    public CommentsService(CommentsRepository repo)
    {
      _repo = repo;
    }

    // internal List<Comment> Get()
    // {
    //   return _repo.Get();
    // }

     internal Comment Get(int id)
    {
       Comment c = _repo.Get(id);
      if(c == null)
      {
        throw new Exception("Invalid comment ID");
      }
      return c;
    }


    internal Comment Create(Comment newComment)
    {
      return _repo.Create(newComment);
      
    }

    internal Comment Edit(Comment updatedComment)
    {
      Comment original = Get(updatedComment.Id);
      updatedComment.Body = updatedComment.Body != null ? updatedComment.Body : original.Body;
      return _repo.Update(updatedComment);
    }

    internal void Delete(int commentId, string userId)
    {
      Comment deadComment = Get(commentId);
      if(deadComment.CreatorId != userId)
      {
        throw new Exception("Not your comment to delete.");
        _repo.Delete(commentId);
      }
    }
  }
}