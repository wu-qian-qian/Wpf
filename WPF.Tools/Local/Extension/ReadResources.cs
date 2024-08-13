using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace Extension
{
  public  class ReadResources
    {
        /// <summary>
        /// 获取资源字典的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetResouceDic<T>(string key)
        {
            return (T)System.Windows.Application.Current.Resources[key];
        }

       /// <summary>
       /// 本地文件加载成图片
       /// </summary>
       /// <param name="path"></param>
       /// <param name="type"></param>
       /// <returns></returns>
        public static ImageSource ReadLocalResource(string path, UriKind type=UriKind.RelativeOrAbsolute)
        {
            string uri = path;
            if (File.Exists(uri))
            {
                return new BitmapImage(new Uri(uri, type));
            }
            return null;
        }
        /// <summary>
        /// 通过流转换成资源
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static BitmapImage ReadStreamResource(byte[] buffer)
        {
            using var stream = new MemoryStream(buffer);
            var image = new BitmapImage();
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = stream;
            image.EndInit();
            image.Freeze();

            return image;
        }

        public static ImageSource ReadStreamResource(string path)
        {
            BitmapImage image=default(BitmapImage);
            if (!Path.Exists(path))
                return image;
            image.UriSource=new Uri(path);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            return image;
        }      
      }
}
