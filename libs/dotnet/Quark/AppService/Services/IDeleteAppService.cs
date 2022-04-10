namespace Quark.AppService.Services;

public interface IDeleteAppService<in TKey>
{
  Task DeleteById(TKey id);
}
