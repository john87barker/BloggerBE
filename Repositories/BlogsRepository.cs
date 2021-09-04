using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BloggerBE.Models;
using Dapper;

namespace BloggerBE.Repositories
{
  public class BlogsRepository
  {
    private readonly IDbConnection _db;

    public BlogsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Blog> Get()
    {
    string sql = @"
      SELECT 
        a.*,
        b.*
      FROM blogs b
      JOIN accounts a ON b.creatorId = a.id
      ";
      // data type 1, data type 2, return type
      return _db.Query<Profile, Blog, Blog>(sql, (profile, blog) =>
      {
        blog.Creator = profile;
        return blog;
      }, splitOn: "id").ToList();

    }

    internal Blog Get(int id)
    {
      string sql = @"
      SELECT 
        a.*,
        b.*
      FROM blogs b
      JOIN accounts a ON b.creatorId = a.id
      WHERE b.id = @id
      ";
      // data type 1, data type 2, return type
      return _db.Query<Profile, Blog, Blog>(sql, (profile, blog) =>
      {
        blog.Creator = profile;
        return blog;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }
  }
}