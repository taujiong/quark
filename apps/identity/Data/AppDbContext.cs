using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quark.Identity.Models;

namespace Quark.Identity.Data;

public class AppDbContext : IdentityUserContext<AppUser>
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
}
