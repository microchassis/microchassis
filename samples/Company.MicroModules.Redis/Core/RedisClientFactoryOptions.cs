using System.ComponentModel.DataAnnotations;

namespace Company.MicroModules.Redis;

class RedisClientFactoryOptions
{
    [Required]
    [MinLength(1), MaxLength(20)]
    public required Dictionary<string, RedisClientOptions> Clients { get; set; }
}

class RedisClientOptions
{
    [Required]
    [MinLength(1), MaxLength(1000)]
    public required string Configuration { get; set; }
}