using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace System
{
    public static class GetUIElement
    {
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
