
using System.Globalization;

namespace SellPhoneApplication.Converters
{
    public class StarVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int rating = (int)value;
            int position = int.Parse(parameter.ToString());
            return rating >= position;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }

}
