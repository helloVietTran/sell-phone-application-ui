using System.Globalization;

namespace SellPhoneApplication.Converters
{
    public class MemoryContainsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // cast chung cho IEnumerable<string>
            if (value is IEnumerable<string> list && parameter is string memory)
                return list.Contains(memory);

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
