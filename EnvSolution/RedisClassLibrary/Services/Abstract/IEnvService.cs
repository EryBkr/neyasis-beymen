using RedisClassLibrary.Models;

namespace RedisClassLibrary.Abstract
{
    public interface IEnvService
    {
        Task<T> GetValue<T>(string key);
        Task<bool> SaveOrUpdate(AddEnvModel model);
    }
}
