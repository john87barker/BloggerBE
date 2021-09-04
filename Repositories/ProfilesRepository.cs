using System;
using System.Collections.Generic;
using System.Data;
using BloggerBE.Models;
using Dapper;

namespace BloggerBE.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile Get(int id)
    {
     string sql = "SELECT * FROM accounts WHERE id = @id";
     return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

  }
}