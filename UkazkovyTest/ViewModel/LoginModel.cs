using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Swift;
using System.Text;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UkazkovyTest.Commands;
using UkazkovyTest.Model;
using UkazkovyTest.View;

namespace UkazkovyTest.ViewModel
{
    public class LoginModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public ObservableCollection<User> Users { get; set; }
        public ICommand ShowWindowCommand { get; set; }
        public ICommand QuitAppCommand {  get; set; }
        public string UserNameBox {  get; set; }
        public string PasswordBox { get; set; }

   

        private string _MistakeAnswer;

        public string MistakeAnswer
        {
            get => _MistakeAnswer;
            set
            {
                _MistakeAnswer = value;
                OnPropertyChanged();
            }
        }
        public event Action RequestClose;

        
        public LoginModel()
        {
            Users = UserManager.GetUsers();

            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);

            QuitAppCommand = new RelayCommand(QuitApp, CanQuitApp);


        }
        
        public bool CanQuitApp(object obj)
        {
            return true;
        }

        //Vypíná aplikace
        public void QuitApp(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public bool CanShowWindow(object obj)
        {
            return true;
        }

        //Po kontrole bud otevre nove okno a zavre sebe nebo zobrazi ze se stala chyba
        public void ShowWindow(object obj)
        {
            bool Correct = false;
            foreach (User user in Users)
            {
                if (UserNameBox == user.Username)
                {
                    if (PasswordBox == user.Password)
                    {
                        Correct = true;
                        MainWindow main = new MainWindow(user);
                        Application.Current.MainWindow = main;
                        main.Show();
                        RequestClose?.Invoke();
                        break;
                    }
                }
            }
            if (!Correct)
            {
                MistakeAnswer = "Incorrect password or username";
            }
        }
    }
}
