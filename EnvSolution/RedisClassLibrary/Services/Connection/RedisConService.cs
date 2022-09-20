using StackExchange.Redis;

namespace RedisClassLibrary.Services.Connection
{
    //Redis bağlantısını üstlenen sınıfımız
    public class RedisConService
    {
        private ConnectionMultiplexer _connectionMultiplexer;

        //Redis bağlantısını sağladık
        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379");

        //Rediste birden çok Database vardır.Hangisine bağlanacağımızı seçiyoruz
        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);
    }
}
