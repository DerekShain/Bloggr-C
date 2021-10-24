using System.Data;
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
      return _dataBase.QueryFirstOrDefault<Profile>(@"
      SELECT
      *
      FROM accounts
      WHERE id = @id;
      ", new { profileId });
    }
  }
}