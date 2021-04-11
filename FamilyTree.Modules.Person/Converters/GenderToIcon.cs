using FamilyTree.Business;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace FamilyTree.Modules.Person.Converters
{
    public class GenderToIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gender = (GenderType)value;

            if (gender == GenderType.Male)
                return "Mars";

            return "Venus";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
