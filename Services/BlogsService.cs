using System;
using System.Collections.Generic;
using BloggerBE.Models;
using BloggerBE.Repositories;

namespace BloggerBE.Services
{
  public class BlogsService
  {
    private readonly BlogsRepository _repo;

    public BlogsService(BlogsRepository repo)
    {
      _repo = repo;
    }

    internal List<Blog> Get()
    {
      return _repo.Get();
    }

    internal Blog Get(int id)
    {
      Blog blog = _repo.Get(id);
      if(blog == null)
      {
        throw new Exception("Invalid blog ID");
      }
      return blog;
    }

    internal Blog GetBlogsByProfile(string id)
    {
      return _repo.GetBlogsByProfile(id);
    }

    internal Comment GetComments()
    {
      throw new NotImplementedException();
    }

    internal Blog Create(Blog newBlog)
    {
      Blog makeBlog = _repo.Create(newBlog);
      return makeBlog;
    }

    internal Blog Edit(Blog updatedBlog, string userId)
    {
      if(updatedBlog.CreatorId != userId)
      {
        throw new Exception("Not your blog to edit.");
      }
       Blog original = Get(updatedBlog.Id);
      updatedBlog.Title = updatedBlog.Title != null ? updatedBlog.Title : original.Title;
     updatedBlog.Body = updatedBlog.Body != null ? updatedBlog.Body : original.Body;
      updatedBlog.ImgUrl = updatedBlog.ImgUrl != null ? updatedBlog.ImgUrl : original.ImgUrl;
      return _repo.Update(updatedBlog);
    }

    

    internal void Delete(int blogId, string userId)
    {
      Blog deadBlog = Get(blogId);
      if(deadBlog.CreatorId != userId)
      {
        throw new Exception("You don't have permission to delete this.");
      }
      _repo.Delete(blogId);
    }

    
  }
}