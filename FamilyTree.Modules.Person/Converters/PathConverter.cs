using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace FamilyTree.Modules.Person.Converters
{
    public class PathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = value.ToString();

            if (string.IsNullOrEmpty(path))
                path = "images/default-avatar.png";

            return Path.Combine(Environment.CurrentDirectory, path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
