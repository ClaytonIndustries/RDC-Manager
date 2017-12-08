using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using RDCManager.Models;

namespace RDCManager.Converters
{
    public class GroupIdToNameConverter : IValueConverter
    {
        private readonly IEnumerable<RDCGroup> _groups;

        public GroupIdToNameConverter(IEnumerable<RDCGroup> groups)
        {
            _groups = groups;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Guid id = Guid.Parse(value.ToString());

            RDCGroup group = _groups.FirstOrDefault(x => x.Id == id);

            return group?.Name ?? "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}