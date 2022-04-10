using System.ComponentModel.DataAnnotations;

namespace Quark.Identity;

public record ExternalLoginInfoDto
{
  [Required]
  public string? LoginProvider { get; set; }

  [Required]
  public string? ProviderKey { get; set; }
}

public record ExternalLoginCreateDto : ExternalLoginInfoDto
{
  [Required]
  public string? UserId { get; set; }

  public string? DisplayName { get; set; }
}
