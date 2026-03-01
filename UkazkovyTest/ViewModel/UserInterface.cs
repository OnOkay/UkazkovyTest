using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UkazkovyTest.Model;

namespace UkazkovyTest.ViewModel
{
    public interface UserInterface
    {
        ObservableCollection<User> GetUsers();
    }
}
