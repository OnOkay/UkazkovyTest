using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Schema;
using UkazkovyTest.Model;

namespace UkazkovyTest.Converters
{
    public class BtnValueConverter : IMultiValueConverter
    {
        //Obrvuje tlačítko pokud zpráva, kterou přijimá nemá Receive Time na červeno a pokud má tak opět na modrou
        //Momentalně blbne Uživatel A, který je pro ostatní neustále červený
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] is not int id || values[1] is not ObservableCollection<Message> messages)
                return Brushes.Blue;

            bool hasUnread = messages.Any(m => m.ReceiverId == id && m.ReceiveTime == null);
            return hasUnread ? Brushes.Red : Brushes.Blue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

