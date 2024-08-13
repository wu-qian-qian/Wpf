using Newtonsoft.Json;

namespace Extension
{
   public static class JosnExtension
    {
        public static T JosnParse<T>(this string josn)
        {
            T obj = JsonConvert.DeserializeObject<T>(josn);
            return obj;
        }

        public static string ObjectToJosn(this object obj)
        {
            string josn = JsonConvert.SerializeObject(obj);
            return josn;
        }
    }
}
