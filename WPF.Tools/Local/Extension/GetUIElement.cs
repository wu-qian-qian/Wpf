using System.Windows;
using System.Windows.Media;

namespace System.Windows
{
    public static class GetUIElement
    {
        /// <summary>
        /// 获取夫组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">当前ui</param>
        /// <param name="index">找寻次数</param>
        /// <returns></returns>
        public static T GetParent<T>(this object control,int index=99)
        {
            DependencyObject parent = VisualTreeHelper.GetParent((DependencyObject)control);
            index--;
            if(parent is T ins)
            {
                return ins;
            }
            if (index <= 0)
                return default(T);
            return GetParent<T>(parent,index);
        }
    }
}
