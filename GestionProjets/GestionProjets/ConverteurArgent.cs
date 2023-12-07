using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.UI.Xaml.Data;
using Windows.UI.Xaml.Data;

namespace GestionProjets {
    public class ConverteurArgent : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value is double amount) {
                return string.Format("{0:F2}$", amount);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}