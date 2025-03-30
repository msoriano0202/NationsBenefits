using Newtonsoft.Json;
using StackExchange.Redis;

namespace NationsBenefits.Application.Cache
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;

        public CacheService()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!value.IsNull)
            { 
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public object RemoveData(string key)
        {
            if (_db.KeyExists(key))
            { 
                return _db.KeyDelete(key);
            }

            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
        }

        //public List<RedisKey> GetKeys(string serverUrl, string keyPrefix)
        //{
        //    var keys = ConnectionHelper.Connection.GetServer(serverUrl).Keys();
        //    return keys.Where(k => k.ToString().StartsWith(keyPrefix)).ToList();
        //}
    }
}
