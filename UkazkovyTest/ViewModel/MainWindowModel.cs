using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
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
        public ICollectionView FiltredMessages { get; }
        public ICommand SendMessageCommand { get; set; }
        public ICommand SetActiveUserCommand {  get; set; }
        public ICommand ChangeReceiverCommand {  get; set; }
        public ObservableCollection<MessageUserModel> MessageTest { get; }


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
                FiltredMessages.Refresh();
            }
        }

        public MainWindowModel(User acitiveUser)
        {
            SendContent = "h";
            Users = UserManager.GetUsers();
            Messages = MessageManager.GetMessages();
            FiltredMessages = CollectionViewSource.GetDefaultView(Messages);
            ActiveUser = acitiveUser;
            foreach (User user in Users)
            {
                if (user != ActiveUser)
                {
                    ActiveReceiver = user;
                    break;
                }
            }
            MessageTest = new ObservableCollection<MessageUserModel>(
                Messages.Select(m =>
                {
                    var otherUserId =
                        m.SenderId == ActiveUser.id ? m.ReceiverId : m.SenderId;

                    var otherUser = Users.First(u => u.id == otherUserId);

                    return new MessageUserModel(m, otherUser);
                })
                );



            FiltredMessages.Filter = FilterMessages;
            FiltredMessages.SortDescriptions.Clear();
            FiltredMessages.SortDescriptions.Add(new SortDescription(nameof(MessageUserModel.SendTime), ListSortDirection.Ascending));

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
            MessageManager.NewMessage(SendContent, ActiveUser.id, ActiveReceiver.id);
            SendContent = "";
        }

        public bool CanChangeReceiver(object parametr)
        {
            return true;
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

        private bool FilterMessages(object obj)
        {
            if (ActiveReceiver.id == null || ActiveUser.id == null)
                return true;

            var msg = obj as Message;
            return msg != null && (msg.SenderId == ActiveUser.id && msg.ReceiverId == ActiveReceiver.id) || (msg.SenderId == ActiveReceiver.id && msg.ReceiverId == ActiveUser.id);
        }

    }
}
