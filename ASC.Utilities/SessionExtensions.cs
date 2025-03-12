using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace ASC.Utilities
{
    public static class SessionExtensions
    {
        public static void SetSession(this ISession session, string key, object value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }

        public static T GetSession<T>(this ISession session, string key)
        {
            if (session.TryGetValue(key, out byte[] value))
            {
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(value));
            }
            return default(T);
        }
    }
}
