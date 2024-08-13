using System.Text;
using System.Xml.Serialization;
using Extension;

namespace System.IO
{
   public static class FileHelper
    {
        public static T ReadConfig<T>(string path) where T : class, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileInfo file = new FileInfo(path);
            FileStream stream = file.OpenRead();
            var res = (T)serializer.Deserialize(stream);
            stream.Close();
            return res;
        }
        public static bool WriteConfigXml<T>(string path, T data) where T : class, new()
        {
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                serializer.Serialize(stream, data);
                stream.Close();
                return true;
            }
            return false;
        }
        public static T ReadConfigJons<T>(string path) where T : class,new()
        {
            if (File.Exists(path))
            {
                using (var stream = File.OpenText(path))
                {
                    var text = stream.ReadToEnd();
                    if (text == "")
                    {
                        return (T)Activator.CreateInstance(typeof(T));
                    }
                    return text.JosnParse<T>();
                }
            }
            return (T)Activator.CreateInstance(typeof(T));
        }
        public static bool ClearFileData(string path)
        {
            if (File.Exists(path))
            {
                FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                stream.SetLength(0);
                stream.Close();
                return true;
            }
            return false;
        }
        public static bool WriteFileData(string path, object obj)
        {
            if (File.Exists(path))
            {
                using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    string josn = obj.ObjectToJosn();
                    byte[] buffer = Encoding.UTF8.GetBytes(josn);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            return false;
        }
        public static byte[]? ReadFileStream(string path)
        {
            byte[] buffer = default(byte[]);
            if (File.Exists(path))
            {
                using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    Stream disStream = new MemoryStream();
                    stream.CopyToAsync(disStream,(int)stream.Length);
                    disStream.Write(buffer, 0, (int)(stream.Length));
                }
            }
            return buffer;
        }
    }
}
