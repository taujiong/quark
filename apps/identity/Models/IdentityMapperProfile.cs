using AutoMapper;
using Quark.Identity.Contracts;

namespace Quark.Identity.Models;

public class IdentityMapperProfile : Profile
{
  public IdentityMapperProfile()
  {
    CreateMap<AppUser, IdentityUserOutputDto>();
  }
}
