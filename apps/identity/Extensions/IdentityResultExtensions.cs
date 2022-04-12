using Microsoft.AspNetCore.Identity;
using Quark.ExceptionHandling;

namespace Quark.Identity.Extensions;

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
