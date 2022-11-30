using Newtonsoft.Json.Linq;
using System;

namespace Gameloop.Vdf.JsonConverter
{
    public static class VTokenExtensions
    {
        public static JToken ToJson(this VToken tok)
        {
            Type type = tok.GetType();
            if (type == typeof(VValue)) {
                return ((VValue)tok).ToJson();
            }else if (type == typeof(VProperty)) {
                return ((VProperty)tok).ToJson();
            }
            else if (type == typeof(VObject)) {
                return ((VObject)tok).ToJson();
            }
            else {
                throw new InvalidOperationException("Unrecognized VToken.");
            }
        }

        public static JValue ToJson(this VValue val)
        {
            return new JValue(val.Value);
        }

        public static JProperty ToJson(this VProperty prop)
        {
            return new JProperty(prop.Key, prop.Value.ToJson());
        }

        public static JObject ToJson(this VObject obj)
        {
            JObject resultObj = new JObject();

            foreach (VProperty prop in obj.Children())
                resultObj.Add(prop.ToJson());

            return resultObj;
        }
    }
}
