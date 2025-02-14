using Cache.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Cache.Models
{
    public abstract class Cacheable<TResult> : ICacheable<TResult>
    {
        public TResult? GetDefaultValue()
        {
            return default;
        }

        /// <summary>
        /// Generates a unique key with the Type.Name and properties name-value
        /// </summary>
        /// <returns></returns>
        public string GetHashKey()
        {
            Dictionary<string, object?> properties = GetType()
                .GetProperties()
                .ToDictionary(
                    p => p.Name,
                    p => p.GetValue(this));

            string json = JsonSerializer.Serialize(properties);

            string hash =
                Convert.ToHexString(
                    MD5.HashData(Encoding.UTF8.GetBytes(json)));

            return $"{GetType().Name}:{hash}";
        }
    }
}
