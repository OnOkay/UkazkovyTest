using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using UkazkovyTest.Model;
using System.Windows.Media;

namespace UkazkovyTest.ViewModel
{
    public class ValueConvertBtnColour: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var user = value as User;
            var messages = parameter as IEnumerable<Message>; // kolekce zpráv z ViewModelu

            if (user != null && messages != null)
            {
                // najdi zprávu pro tohoto uživatele, kde SendTime je null
                var msg = messages.FirstOrDefault(m => m.ReceiverId == user.id && m.SendTime == null);
                if (msg != null)
                    return Brushes.Red;
            }

            return Brushes.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
