namespace Company.MicroModules.Redis;

public class RedisClientFactoryException : Exception
{
    public RedisClientFactoryException()
    { }

    public RedisClientFactoryException(string? message)
        : base(message)
    { }

    public RedisClientFactoryException(string? message, Exception? innerException)
        : base(message, innerException)
    { }
}