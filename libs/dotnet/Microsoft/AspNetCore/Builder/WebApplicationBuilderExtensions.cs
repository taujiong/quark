using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quark.AspNetCore.Mvc.ApiExplorer;
using Quark.AspNetCore.Mvc.Conventions;
using Quark.AspNetCore.Users;
using Quark.ExceptionHandling;
using Quark.Localization;

namespace Microsoft.AspNetCore.Builder;

public static class WebApplicationBuilderExtensions
{
  public static void AddQuarkCore(this WebApplicationBuilder builder)
  {
    if (builder == null) throw new ArgumentNullException(nameof(builder));

    builder.Services.AddHttpContextAccessor();
    builder.Services.TryAddScoped<ICurrentUser, CurrentUser>();
  }

  public static IMvcBuilder AddApiController(this WebApplicationBuilder builder)
  {
    if (builder == null) throw new ArgumentNullException(nameof(builder));

    var mvcBuilder = builder.Services.AddControllers();
    mvcBuilder.AddJsonOptions(o =>
    {
      o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    return mvcBuilder;
  }

  public static void ConfigureApiController(this WebApplicationBuilder builder)
  {
    if (builder == null) throw new ArgumentNullException(nameof(builder));

    var routeArea = builder.Configuration["App:RouteArea"]
                    ?? builder.Configuration["App:AppName"]?.ToKebabCase()
                    ?? "app";

    builder.Services.Configure<MvcOptions>(options =>
    {
      options.Conventions.Add(new QuarkApiControllerConvention(routeArea));
      options.Filters.Add(typeof(QuarkExceptionFilter));
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddTransient<IApiDescriptionProvider, ResponseTypeApiDescriptionProvider>();
    builder.Services.AddSwaggerGen(options =>
    {
      var xmlFileName = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
    });
  }

  public static void AddQuarkLocalization(this WebApplicationBuilder builder, IMvcBuilder mvcBuilder)
  {
    if (builder == null) throw new ArgumentNullException(nameof(builder));

    builder.Services.AddLocalization();
    mvcBuilder.AddDataAnnotationsLocalization(options =>
    {
      options.DataAnnotationLocalizerProvider = (_, factory) =>
              factory.Create(typeof(QuarkSharedResource));
    });

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
      var supportedCultures = new List<CultureInfo>
        {
                new("zh-CN"),
                new("en-US"),
        };

      options.ApplyCurrentCultureToResponseHeaders = true;
      options.DefaultRequestCulture = new RequestCulture("zh-CN", "zh-CN");
      options.SupportedCultures = supportedCultures;
      options.SupportedUICultures = supportedCultures;
    });
  }
}
