using Quark.ExceptionHandling;
using Microsoft.AspNetCore.Identity;

namespace Quark.Identity.Api.Extensions;

public static class IdentityResultExtensions
{
  public static void CheckErrors(this IdentityResult result)
  {
    if (result.Succeeded)
    {
      return;
    }

    if (result.Errors == null)
    {
      throw QuarkException.InternalServer500Exception;
    }

    throw new UserFriendlyException(result.Errors.First().Description);
  }
}
