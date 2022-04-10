using AutoMapper;

namespace Quark.Identity.Api.Models;

public class IdentityMapperProfile : Profile
{
  public IdentityMapperProfile()
  {
    CreateMap<AppUser, IdentityUserOutputDto>();
  }
}
