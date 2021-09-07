using System;
using BloggerBE.Models;
using BloggerBE.Repositories;

namespace BloggerBE.Services
{
    public class AccountService
    {
        private readonly AccountsRepository _repo;
        public AccountService(AccountsRepository repo)
        {
            _repo = repo;
        }

        internal string GetProfileEmailById(string id)
        {
            return _repo.GetById(id).Email;
        }
        internal Account GetProfileByEmail(string email)
        {
            return _repo.GetByEmail(email);
        }
        internal Account GetOrCreateProfile(Account userInfo)
        {
            Account profile = _repo.GetById(userInfo.Id);
            if (profile == null)
            {
                return _repo.Create(userInfo);
            }
            return profile;
        }

        internal Account Edit(Account editData, string userEmail)
        {
            
            Account original = GetProfileByEmail(userEmail);
            original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
            original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
            return _repo.Edit(original);
        }

    internal Account GetBlogsByAccount()
    {
      Account aBlogs = _repo.GetBlogsByAccount();
      return aBlogs;
    }

    internal Account EditAccount(Account updatedA, string userId)
    {
        if(updatedA.Id != userId)
        {
        throw new Exception("NOT ALLOWED");
      }
      Account original = GetById(updatedA.Id);
      updatedA.Name = updatedA.Name != null ? updatedA.Name : original.Name;
      updatedA.Picture = updatedA.Picture != null ? updatedA.Picture : original.Picture;    
      updatedA.Email = updatedA.Email != null ? updatedA.Email : original.Email;
      return _repo.Update(updatedA);

    }

    private Account GetById(string id)
    {
      Account account = _repo.GetById(id);
      if(account == null)
      {
        throw new Exception("Invalid account ID");
      }
      return account;
    }
  }
}