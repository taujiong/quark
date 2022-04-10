using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Quark.ExceptionHandling;

public class QuarkException : Exception
{
  public const string DefaultExceptionMessage = "Unexpected error occured.";
  public static readonly QuarkException InternalServer500Exception = new(
      DefaultExceptionMessage,
      "This is an internal server error, please try again later.");

  public QuarkException(string message, string? detail = null, Exception? innerException = null)
      : base(message, innerException)
  {
    Detail = detail ?? "Please try again later or correct your operation.";
  }

  public string Detail { get; }

  public virtual bool ShouldLocalizeException => true;

  public virtual object[] MessageLocalizationParameters => Array.Empty<object>();

  public virtual object[] DetailLocalizationParameters => Array.Empty<object>();

  public virtual ProblemDetails ToProblemDetails()
  {
    return new ProblemDetails
    {
      Title = Message,
      Detail = Detail,
      Status = StatusCodes.Status500InternalServerError,
    };
  }
}
