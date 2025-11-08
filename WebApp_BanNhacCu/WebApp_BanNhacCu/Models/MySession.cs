
using Newtonsoft.Json;

namespace web_session.Models
{
    public class MySession
    {
        public static T Get<T>(ISession session, string key)
        {
            if (string.IsNullOrEmpty(session.GetString(key)))
            {
                session.SetString(key, JsonConvert.SerializeObject(null));
            }
            return JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
        public static void Set<T>(ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
