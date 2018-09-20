using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.Extensions
{
    public static class SessionExtension
    {
        public static void SaveAsJson(this ISession session, string sessionKey, Object saveObject)
        {
            session.SetString(sessionKey, JsonConvert.SerializeObject(saveObject));
        }
        
        public static T GetFromJson<T>(this ISession session, string sessionKey)
        {
            var json = session.GetString(sessionKey);

            if (!string.IsNullOrEmpty(session.GetString(sessionKey)))
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }
    }
}
