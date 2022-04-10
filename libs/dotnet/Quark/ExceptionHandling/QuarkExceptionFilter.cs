using System.Diagnostics;
using Quark.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Quark.ExceptionHandling;

public class QuarkExceptionFilter : IExceptionFilter
{
  private readonly ILogger<QuarkExceptionFilter> _logger;

  public QuarkExceptionFilter(ILogger<QuarkExceptionFilter> logger)
  {
    _logger = logger;
  }

  public void OnException(ExceptionContext context)
  {
    _logger.LogError("Error occured: {Message}\nStackTrace:\n{StackTrace}",
        context.Exception.Message,
        context.Exception.StackTrace);
    var exception = context.Exception is QuarkException quarkException
        ? quarkException
        : QuarkException.InternalServer500Exception;

    var problemDetails = exception.ToProblemDetails();
    problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;

    if (exception.ShouldLocalizeException)
    {
      var localizer = context.HttpContext.RequestServices
          .GetRequiredService<IStringLocalizerFactory>()
          .Create(typeof(QuarkSharedResource));
      problemDetails.Title = localizer[problemDetails.Title!, exception.MessageLocalizationParameters];
      problemDetails.Detail = localizer[problemDetails.Detail!, exception.DetailLocalizationParameters];
    }

    context.Result = new ObjectResult(problemDetails);
    context.ExceptionHandled = true;
  }
}
