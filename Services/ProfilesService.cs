using System;
using System.Collections.Generic;
using BloggerBE.Models;
using BloggerBE.Repositories;

namespace BloggerBE.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    internal Profile Get(string id)
    {
      Profile profile = _repo.Get(id);
      if(profile == null)
      {
        throw new Exception("invalid profile id");
      }
      return profile;
    }
  }
}