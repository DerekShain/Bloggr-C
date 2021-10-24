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
      return _dataBase.Query<Profile>(@"
      SELECT
      *
      FROM accounts
      WHERE id = @id;
      ", new { profileId }).FirstOrDefault();
    }
  }
}