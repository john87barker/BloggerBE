using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BloggerBE.Models;
using Dapper;

namespace BloggerBE.Repositories
{
  public class CommentsRepository
  {
      private readonly IDbConnection _db;

    public CommentsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Comment Get(int id)
    {
      string sql = @"
      SELECT 
        a.*,
        c.*
      FROM comments c
      JOIN accounts a ON c.creatorId = a.id
      WHERE c.id = @id
      ";

      return _db.Query<Profile, Comment, Comment>(sql, (profile, c) =>
      {
        c.Creator = profile;
        return c;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }



    internal Comment Update(Comment updatedComment)
    {
      string sql = @"
        UPDATE comments
        SET
            body = @Body
        WHERE id = @Id;
      ";
      _db.Execute(sql, updatedComment);
      return updatedComment;
    }

    internal void Delete(int commentId)
    {
      string sql = "DELETE FROM comments WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { commentId });
    }

    internal Comment GetCommentsByProfile(string id)
    {
      string sql = @"
      SELECT 
        a.*,
        c.*
      FROM comments c
      JOIN accounts a ON c.creatorId = a.id
      WHERE c.creatorId = @id
      ";
      return _db.Query<Profile, Comment, Comment>(sql, (profile, comments) =>
      {
        comments.Creator = profile;
        return comments;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal List<Comment> GetCommentsByAccount(string id)
    {
      string sql = @"
      SELECT 
        a.*,
        c.*
      FROM comments c
      JOIN accounts a ON c.creatorId = a.id
      WHERE c.creatorId = @id
      ";
      return _db.Query<Account, Comment, Comment>(sql, (ac, comments) =>
      {
        comments.Creator = ac;
        return comments;
      }, new { id }, splitOn: "id").ToList(); 
    }

    internal List<Comment> GetCommentsByBlog(int id)
    {
    string sql = @"
      SELECT 
        b.*,
        c.*
      FROM comments c
      JOIN blogs b ON c.blogId = b.id
      WHERE c.blogId = @id
      ";
      return _db.Query<Blog, Comment, Comment>(sql, (blog, comments) =>
      {
        comments.BlogId = blog.Id;
        return comments;
      }, new { id }, splitOn: "id").ToList();    }



    // internal List<Comment> Get()
    // {
    //   string sql = @"
    //   SELECT 
    //     a.*,
    //     c.*
    //   FROM comments c
    //   JOIN accounts a ON c.creatorId = a.id
    //   ";
    //   // data type 1, data type 2, return type
    //   return _db.Query<Profile, Comment, Comment>(sql, (profile, comments) =>
    //   {
    //     comments.CreatorId = profile.Id;
    //     return comments;
    //   }, splitOn: "id").ToList();
    // }
    internal Comment Create(Comment newComment)
    {
      string sql = @"
        INSERT INTO comments
        (body, blogId, creatorId, id)
        VALUES
        (@Body, @BlogId, @CreatorId, @Id);
        SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(sql, newComment);
      return newComment;
      //   return Get(id);
    }

    
  }
}