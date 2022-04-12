using Microsoft.AspNetCore.Identity;
using Quark.Identity.ExceptionHandling;

namespace Quark.Identity.Extensions;

public static class SignInResultExtensions
{
  public static void CheckErrors(this SignInResult result)
  {
    if (result.Succeeded)
    {
      return;
    }

    if (result.IsLockedOut)
    {
      throw new AuthException(AuthError.IsLockedOut);
    }

    if (result.IsNotAllowed)
    {
      throw new AuthException(AuthError.IsNotAllowed);
    }

    if (!result.RequiresTwoFactor)
    {
      throw new AuthException(AuthError.WrongCredential);
    }
  }
}
