using Microsoft.AspNetCore.Identity;

namespace Quark.Identity.Api.Models;

public class AppUser : IdentityUser
{
  public AppUser(string userName) : base(userName)
  {
  }
}
