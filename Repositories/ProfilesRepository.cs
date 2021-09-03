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

    internal List<Profile> Get()
    {
     
    }
  }
}