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

    internal List<Profile> Get()
    {
      return _repo.Get();
    }
  }
}