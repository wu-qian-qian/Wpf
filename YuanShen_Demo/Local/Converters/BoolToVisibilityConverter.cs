using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.Tools.Local.Converter;

namespace YuanShen_Demo.Local.Converters
{
    public class BoolToVisibilityConverter : BaseValueConverter
    {
        public bool IsReverse { get; set; }

        public bool UseHidden { get; set; }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var @bool = System.Convert.ToBoolean(value);

            if (IsReverse) @bool = !@bool;

            if (@bool) return Visibility.Visible;
            else return UseHidden ? Visibility.Hidden : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vis = (Visibility)value;

            bool res = vis != Visibility.Visible;
            if (IsReverse) res = !res;

            return res;
        }
    }
}
