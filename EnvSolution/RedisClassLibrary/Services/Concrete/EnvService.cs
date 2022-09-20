using RedisClassLibrary.Abstract;
using RedisClassLibrary.Models;
using RedisClassLibrary.Services.Connection;
using StackExchange.Redis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace RedisClassLibrary.Services.Concrete
{
    public class EnvService : IEnvService
    {
        private readonly RedisConService _redisCon;
        private readonly Options options;

        public EnvService(RedisConService redisCon, Options options)
        {
            _redisCon = redisCon;
            this.options = options;
        }


        public async Task<T> GetValue<T>(string key)
        {
            var redisDb = _redisCon.GetDb();

            if (!redisDb.KeyExists($"{options.ApplicationName}_{key}"))
                return default(T);


            var value = await redisDb.StringGetAsync($"{options.ApplicationName}_{key}");
            return value.HasValue ? ChangeToType<T>(key, value) : default(T);
        }

        public async Task<bool> SaveOrUpdate(AddEnvModel model)
        {

            model.ApplicationName = options.ApplicationName;
            var status = await _redisCon.GetDb().StringSetAsync($"{options.ApplicationName}_{model.Name}", JsonSerializer.Serialize(model));

            return status;
        }

        private T ChangeToType<T>(string key, RedisValue redisValue)
        {
            if (redisValue.IsNull)
                return default(T);

            try
            {
                var sRedisValue = redisValue;
                if (typeof(T) == typeof(bool))
                {
                    var tempRedisValue = (string)redisValue;
                    if (tempRedisValue.ToLower() == "true")
                        sRedisValue = RedisValue.Unbox(1);
                    else
                        sRedisValue = RedisValue.Unbox(0);
                }

                var obj = Convert.ChangeType(sRedisValue, typeof(T));
                return (T)obj;
            }
            catch
            {
                throw new Exception($"Redis key '{key}' has invalid redis value '{redisValue}'");
            }
        }
    }
}
