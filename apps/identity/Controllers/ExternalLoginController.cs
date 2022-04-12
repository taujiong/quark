using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quark.AspNetCore.Mvc;
using Quark.ExceptionHandling;
using Quark.Identity.Contracts;
using Quark.Identity.Extensions;
using Quark.Identity.Models;

namespace Quark.Identity.Controllers;

public class ExternalLoginController : QuarkControllerBase
{
  private readonly UserManager<AppUser> _userManager;

  public ExternalLoginController(UserManager<AppUser> userManager)
  {
    _userManager = userManager;
  }

  [HttpGet("{id}")]
  public async Task<List<ExternalLoginInfoDto>> GetListByUserId(string id)
  {
    var user = await _userManager.FindByIdAsync(id);
    if (user == null) throw new EntityNotFoundException(typeof(AppUser), id);

    var logins = await _userManager.GetLoginsAsync(user);
    return logins.Select(login => new ExternalLoginInfoDto
    {
      LoginProvider = login.ProviderDisplayName,
      ProviderKey = login.ProviderKey,
    }).ToList();
  }

  [HttpPost]
  [ProducesResponseType(404)]
  public async Task Create(ExternalLoginCreateDto input)
  {
    var user = await _userManager.FindByIdAsync(input.UserId);
    if (user == null) throw new EntityNotFoundException(typeof(AppUser), input.UserId!);

    var loginInfo = new UserLoginInfo(input.LoginProvider, input.ProviderKey,
        input.DisplayName ?? input.LoginProvider);
    var result = await _userManager.AddLoginAsync(user, loginInfo);
    result.CheckErrors();
  }

  [HttpDelete("{id}")]
  public async Task DeleteById(string id, ExternalLoginInfoDto input)
  {
    var user = await _userManager.FindByIdAsync(id);
    if (user == null) throw new EntityNotFoundException(typeof(AppUser), id);

    var result = await _userManager.RemoveLoginAsync(user, input.LoginProvider, input.ProviderKey);
    result.CheckErrors();
  }
}
