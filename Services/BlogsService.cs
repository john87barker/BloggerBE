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
  }
}