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
    public class AlignmentYConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            VerticalAlignment temp = (VerticalAlignment)value;
            switch (temp)
            {
                case VerticalAlignment.Top:
                    return AlignmentY.Top;
                case VerticalAlignment.Bottom:
                    return AlignmentY.Bottom;
                case VerticalAlignment.Center:
                default:
                    return AlignmentY.Center;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
