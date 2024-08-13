using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        /// <summary>
        /// 将josn序列化成节点型
        /// </summary>
        /// <param name="josn"></param>
        /// <returns></returns>
        public static JObject GetJosnObject(this string josn)
        {
           return JObject.Parse(josn);
        }
        /// <summary>
        /// 获取节型的特定节点
        /// </summary>
        /// <param name="josn"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static JToken GetJosnNode(this string josn,string node) 
        {
            return JObject.Parse(josn)[node];
        }
    }
}
