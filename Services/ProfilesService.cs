using System;
using Bloggr.Models;

namespace Bloggr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _profilesRepository;
    public ProfilesService(ProfilesRepository profilesRepository)
    {
      _profilesRepository = profilesRepository;
    }
    internal Profile Get(string profileId)
    {
      Profile profile = _profilesRepository.Get(profileId);
      if (profile == null)
      {
        throw new Exception("Something went wrong");
      }
      return profile;
    }
  }
}