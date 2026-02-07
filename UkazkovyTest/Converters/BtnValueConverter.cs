using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Schema;
using UkazkovyTest.Model;

namespace UkazkovyTest.Converters
{
    public class BtnValueConverter : IMultiValueConverter
    {
        // Zmena tlačítka podle toho zda existuje zprava pro uživatele od jiného uživatele bez času přečtení
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {


            if (values.Length < 2)
                return Brushes.Blue;

            if (values[0] == DependencyProperty.UnsetValue ||
                values[1] == DependencyProperty.UnsetValue ||
                values[2] == DependencyProperty.UnsetValue)
                return Brushes.Blue;

            int id = System.Convert.ToInt32(values[0]);
            var messages = values[1] as IEnumerable<Message>;
            var activeUser = values[2] as User;

            if (activeUser == null)
                return Brushes.Blue;

            if (messages == null)
                return Brushes.Blue;

            bool hasUnread = messages.Any(m => m.ReceiverId == id && m.ReceiveTime == null && m.SenderId == activeUser.id);
            return hasUnread ? Brushes.Red : Brushes.Blue;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

