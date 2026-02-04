using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using UkazkovyTest.Commands;
using UkazkovyTest.Model;
using UkazkovyTest.View;

namespace UkazkovyTest.ViewModel
{
    internal class MainWindowModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<User> Users { get; set; }

        public ICommand ShowWindowCommand { get; set; }

        public ICommand SetActiveUserCommand {  get; set; }

        public String ActiveUser { get; set; }

        public MainWindowModel() 
        {
            Users = UserManager.GetUsers();

            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);

            SetActiveUserCommand = new RelayCommand(SetActiveUser, CanSetActiveUser);
            ActiveUser = "Goodbye";

        }
        public bool CanSetActiveUser(object obj)
        {
            return true;
        }

        public void SetActiveUser(object obj)
        {
            ActiveUser = "HELLO";
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
