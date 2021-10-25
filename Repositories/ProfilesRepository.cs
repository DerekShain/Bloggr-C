using System.Data;
using System.Linq;
using Bloggr.Models;
using Dapper;

namespace Bloggr.Services
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _dataBase;
    public ProfilesRepository(IDbConnection dataBase)
    {
      _dataBase = dataBase;
    }

    internal Profile Get(string profileId)
    {
      var sql = @"
      SELECT 
      *
      FROM
      accounts
      ";
      return _dataBase.Query<Profile>(sql, new { profileId }).FirstOrDefault();
    }
  }
}