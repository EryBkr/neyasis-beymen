namespace RedisClassLibrary.Models
{
    class GetEnvModel<T>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public T Value { get; set; }
        public int IsActive { get; set; }
        public string ApplicationName { get; set; }
    }
}
