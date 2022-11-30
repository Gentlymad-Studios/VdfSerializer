using Newtonsoft.Json.Linq;
using System;

namespace Gameloop.Vdf.JsonConverter
{
    public static class JTokenExtensions
    {
        public static VToken ToVdf(this JToken tok)
        {
            Type type = tok.GetType();
            if (type == typeof(JValue)) {
                return ((JValue)tok).ToVdf();
            }else if (type == typeof(JProperty)) {
                return ((JProperty)tok).ToVdf();
            }else if (type == typeof(JObject)) {
                return ((JObject)tok).ToVdf();
            }else if (type == typeof(JArray)) {
                return ((JObject)tok).ToVdf();
            }
            else {
                throw new InvalidOperationException("Unrecognized JToken.");
            }
        }

        public static VValue ToVdf(this JValue val)
        {
            return new VValue(val.Value);
        }

        public static VProperty ToVdf(this JProperty prop)
        {
            return new VProperty(prop.Name, prop.Value.ToVdf());
        }

        public static VObject ToVdf(this JObject obj)
        {
            VObject resultObj = new VObject();

            foreach (JToken tok in obj.Children())
                resultObj.Add((VProperty) tok.ToVdf());

            return resultObj;
        }

        public static VObject ToVdf(this JArray arr)
        {
            VObject resultObj = new VObject();

            for (int index = 0; index < arr.Count; index++)
                resultObj.Add(index.ToString(), arr[index].ToVdf());

            return resultObj;
        }
    }
}
