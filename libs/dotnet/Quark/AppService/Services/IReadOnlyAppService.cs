using AppService.Dtos;
using Quark.AppService.Dtos;

namespace Quark.AppService.Services;

public interface IReadOnlyAppService<in TKey, TEntityDto>
    : IReadOnlyAppService<TKey, TEntityDto, TEntityDto>
{
}

public interface IReadOnlyAppService<in TKey, TGetOutputDto, TGetListOutputDto>
{
  Task<TGetOutputDto> GetById(TKey id);

  Task<PagedResultDto<TGetListOutputDto>> GetListByQuery(QueryRequestDto input);
}
