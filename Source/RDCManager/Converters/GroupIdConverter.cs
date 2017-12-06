using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using RDCManager.Models;

namespace RDCManager.Converters
{
    public class GroupIdConverter : IValueConverter
    {
        private readonly IEnumerable<RDCGroup> _groups;

        public GroupIdConverter(IEnumerable<RDCGroup> groups)
        {
            _groups = groups;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Guid id = Guid.Parse(value.ToString());

            return _groups.First(x => x.Id == id).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}