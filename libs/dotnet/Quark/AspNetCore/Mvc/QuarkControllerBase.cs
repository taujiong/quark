using Microsoft.AspNetCore.Mvc;

namespace Quark.AspNetCore.Mvc;

[ApiController]
[Route("/api/app/[controller]")]
[ApiConventionType(typeof(QuarkApiConventions))]
public class QuarkControllerBase : ControllerBase
{
}
