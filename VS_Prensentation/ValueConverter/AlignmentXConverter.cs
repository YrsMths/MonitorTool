using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace VS_Presentation.ValueConverter
{
    public class AlignmentXConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HorizontalAlignment temp = (HorizontalAlignment)value;
            switch (temp)
            {
                case HorizontalAlignment.Left:
                    return AlignmentX.Left;
                case HorizontalAlignment.Right:
                    return AlignmentX.Right;
                case HorizontalAlignment.Center:
                default:
                    return AlignmentX.Center;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
