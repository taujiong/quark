namespace Quark.ExceptionHandling;

public class UserFriendlyException : QuarkException
{
  public UserFriendlyException(string message, string? detail = null, Exception? innerException = null)
      : base(message, detail, innerException)
  {
  }

  public override bool ShouldLocalizeException => false;
}
