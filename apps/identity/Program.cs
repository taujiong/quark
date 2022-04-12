using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quark.Identity.Data;
using Quark.Identity.Localization;
using Quark.Identity.Models;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.AddQuarkCore();

builder.Services.AddDbContext<AppDbContext>(options =>
{
  var connectionString = builder.Configuration.GetConnectionString("Identity");
  options.UseNpgsql(connectionString, optionsBuilder =>
  {
    optionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(15), null);
  });
});
builder.Services.AddDataProtection(); // fot token providers in identity system
builder.Services.TryAddSingleton<ISystemClock, SystemClock>(); // for security stamp validators in identity system
builder.Services.AddIdentityCore<AppUser>(options =>
    {
      options.User.RequireUniqueEmail = true;
    })
    .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(IdentityMapperProfile));

var mvcBuilder = builder.AddApiController();
builder.AddQuarkLocalization(mvcBuilder);
builder.ConfigureApiController();

#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.

app.UseQuarkControllerPreset();

app.UseHttpsRedirection();

app.UseQuarkLocalization();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
