using System;
using System.Data;
using System.Linq;
using BloggerBE.Models;
using Dapper;

namespace BloggerBE.Repositories
{
    public class AccountsRepository
    {
        private readonly IDbConnection _db;

        public AccountsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Account GetByEmail(string userEmail)
        {
            string sql = "SELECT * FROM accounts WHERE email = @userEmail";
            return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
        }

        internal Account GetById(string id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @id";
            return _db.QueryFirstOrDefault<Account>(sql, new { id });
        }

        internal Account Create(Account newAccount)
        {
            string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
            _db.Execute(sql, newAccount);
            return newAccount;
        }

        internal Account Edit(Account update)
        {
            string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }

    internal Account GetBlogsByAccount()
    {
      string sql = @"
      SELECT
        b*,
        a*
      FROM accounts a
      JOIN blogs b ON a.id = b.creatorId
      WHERE a = @Creator
      ";
      return _db.Query<Blog, Account, Account>(sql, (b, ac) =>
      {
        ac.Id = b.CreatorId;
        return ac;
      }, splitOn: "id").FirstOrDefault();
    }
  }
}
