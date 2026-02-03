using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using UkazkovyTest.Commands;
using UkazkovyTest.Model;
using UkazkovyTest.View;

namespace UkazkovyTest.ViewModel
{
    internal class MainWindowModel
    {
        public ObservableCollection<User> Users { get; set; }

        public ICommand ShowWindowCommand { get; set; }


        public MainWindowModel() 
        {
            Users = UserManager.GetUsers();

            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
        }

        public bool CanShowWindow(object obj)
        {
            return true;
        }

        public void ShowWindow(object obj)
        {
            Login login = new Login();
            login.Show();
        }
    }
}
