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
      return _db.Query<Profile, Blog, Blog>(sql, (profile, blog) =>
      {
        blog.Creator = profile;
        return blog;
      }, splitOn: "id").ToList();

    }

    internal Blog GetBlogsByProfile(string id)
    {
      string sql = @"
      SELECT 
        a.*,
        b.*
      FROM blogs b
      JOIN accounts a ON b.creatorId = a.id
      WHERE b.creatorId = @id
      ";
      return _db.Query<Profile, Blog, Blog>(sql, (profile, blog) =>
      {
        blog.Creator = profile;
        return blog;
      }, new { id }, splitOn: "id").FirstOrDefault();
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
      return _db.Query<Profile, Blog, Blog>(sql, (profile, blog) =>
      {
        blog.Creator = profile;
        return blog;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }



    internal Blog Create(Blog newBlog)
    {
      string sql = @"
        INSERT INTO blogs
        (title, body, imgUrl, published, creatorId, id)
        VALUE
        (@Title, @Body, @ImgUrl, @Published, @CreatorId, @Id);
        SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(sql, newBlog);
      return Get(id);
    }

    internal Blog Update(Blog updatedBlog)
    {
      string sql = @"
        UPDATE blogs
        SET
            title = @Title,
            body = @Body,
            imgUrl = @ImgUrl
        WHERE id = @Id;
      ";
      _db.Execute(sql, updatedBlog);
      return updatedBlog;

    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM blogs WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}