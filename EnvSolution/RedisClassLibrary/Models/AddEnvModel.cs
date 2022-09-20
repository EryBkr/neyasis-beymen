namespace RedisClassLibrary.Models
{
    public class AddEnvModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public dynamic Value { get; set; }
        public int IsActive { get; set; }
        public string ApplicationName { get; set; }
    }
}
