using Quark.Identity.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Quark.Identity.Api.Data;

public class AppDbContext : IdentityUserContext<AppUser>
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
}
