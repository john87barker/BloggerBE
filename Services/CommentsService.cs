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

    internal Comment Edit(Comment updatedComment, string userId)
    {
      Comment original = Get(updatedComment.Id);
      if(updatedComment.CreatorId != userId)
      {
        throw new Exception("Not your comment to edit.");
      }
      updatedComment.Body = updatedComment.Body != null ? updatedComment.Body : original.Body;
      return _repo.Update(updatedComment);
    }

    internal void Delete(int commentId, string userId)
    {
      Comment deadComment = Get(commentId);
      if(deadComment.CreatorId != userId)
      {
        throw new Exception("Not your comment to delete.");
      }
        _repo.Delete(commentId);
    }

      internal Comment GetCommentsByProfile(string id)
    {
      return _repo.GetCommentsByProfile(id);
    }

    internal List<Comment> GetCommentsByBlog(int id)
    {
      return _repo.GetCommentsByBlog(id);
    }

    internal List<Comment> GetCommentsByAccount(string id)
    {
      return _repo.GetCommentsByAccount(id);
    }
  }
}