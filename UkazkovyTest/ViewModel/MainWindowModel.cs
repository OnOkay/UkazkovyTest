using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        public ICommand SendMessageCommand { get; set; }

        public ICommand SetActiveUserCommand {  get; set; }

        public ICommand ChangeReceiverCommand {  get; set; }


        public User ActiveUser { get;}
        
        private string _SendContent;

        public string SendContent
        {
            get => _SendContent;
            set
            {
                if (_SendContent != value)
                {
                    _SendContent = value;
                    OnPropertyChanged(nameof(SendContent));
                    OnPropertyChanged(nameof(CanClickButton));
                }
            }
        }
        public bool CanClickButton => !string.IsNullOrEmpty(SendContent) || SendContent.Length > 255;


        private User _ActiveReceiver;

        public User ActiveReceiver
        {
            get => _ActiveReceiver;
            set
            {
                _ActiveReceiver = value;
                OnPropertyChanged();
            }
        }

        public MainWindowModel(User acitiveUser)
        {
            SendContent = "h";
            Users = UserManager.GetUsers();
            Messages = MessageManager.GetMessages();
            ActiveUser = acitiveUser;
            foreach (User user in Users)
            {
                if (user != ActiveUser)
                {
                    ActiveReceiver = user;
                    break;
                }
            }

            SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);

            SetActiveUserCommand = new RelayCommand(SetActiveUser, CanSetActiveUser);

            ChangeReceiverCommand = new RelayCommand(ChangeReceiver, CanChangeReceiver);


        }
        public bool CanSetActiveUser(object obj)
        {
            return true;
        }

        public void SetActiveUser(object obj)
        {
            
        }

        public bool CanSendMessage(object obj)
        {
            return true;
        }

        public void SendMessage(object obj)
        {
            
            Messages.Add(new Message() { MessageContent = SendContent, ReceiverId = 1, SenderId = 2 });
        }

        public bool CanChangeReceiver(object parametr)
        {
            if (parametr is int value && value == ActiveReceiver.id)
            {
                return false;
            }
            else
            {
                return true;
            }
                
        }

        public void ChangeReceiver(object parametr)
        {

            foreach (User user in Users)
            {
                if (parametr is int value && value == user.id)
                {
                    ActiveReceiver = user;
                    break;
                }
            }
        }

    }
}
