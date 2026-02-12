using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using UkazkovyTest.Commands;
using UkazkovyTest.Model;
using UkazkovyTest.View;
using static System.Net.Mime.MediaTypeNames;

namespace UkazkovyTest.ViewModel
{
    internal class MainWindowModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        public ObservableCollection<UserMessage> UserMessages { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<User> BtnUsers { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        //Zpravovy filtr aby ukazoval pouze 2 spolu komunikujici osoby
        public ICollectionView FiltredMessages { get; set; }

        //Posila obsah textboxu pro tvorbu nove zpravy
        public ICommand SendMessageCommand { get; set; }
        //Při stisknusti meni activeReceivera coz meni filtr zobrazenych zprav
        public ICommand ChangeReceiverCommand {  get; set; }


        public User ActiveUser { get;}
        
        //Sbírá obsah textboxu, na kterém záleží Obsah labelu a obsah nově vytvořené zprávy
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
                    OnPropertyChanged(nameof(TextLength));
                }
            }
        }
        public int TextLength => string.IsNullOrEmpty(SendContent) ? 0 : SendContent.Length;
        public bool CanClickButton => !string.IsNullOrEmpty(SendContent) && SendContent.Length < 255;


        //Proměná která určuje s kým momentálně uživatel komunikuje
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
            SendContent = "";
            Users = UserManager.GetUsers();
            BtnUsers = UserManager.GetUsers();
            BtnUsers.Remove(acitiveUser);
            Messages = MessageManager.GetMessages();
            
            ActiveUser = acitiveUser;
            UpdateUserMessages();
            FiltredMessages = CollectionViewSource.GetDefaultView(UserMessages);
            foreach (User user in Users)
            {
                if (user != ActiveUser)
                {
                    ActiveReceiver = user;
                    break;
                }
            }

            FiltredMessages.Filter = FilterMessages;
            FiltredMessages.SortDescriptions.Clear();
            FiltredMessages.SortDescriptions.Add(new SortDescription(nameof(UserMessage.SendTime), ListSortDirection.Ascending));

            OnPropertyChanged(nameof(FiltredMessages));
            OnPropertyChanged(nameof(Messages));

            SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);

            ChangeReceiverCommand = new RelayCommand(ChangeReceiver, CanChangeReceiver);

            MessageManager.SetReceiveTime(ActiveUser.Id, ActiveReceiver.Id);
        }

        public bool CanSendMessage(object obj)
        {
            return true;
        }

        //Tvorba nové zprávy a aktulizace UI
        public void SendMessage(object obj)
        {
            MessageManager.NewMessage(SendContent, ActiveUser.Id, ActiveReceiver.Id);
            UpdateUserMessages();
            FiltredMessages.Refresh();
            SendContent = "";
        }

        public bool CanChangeReceiver(object parametr)
        {
            return true;
        }

       //Změna přijimatele zpráv od uživatele
        public void ChangeReceiver(object parametr)
        {
            if (Users == null)
            {
                return;
            }
            else
            {
                foreach (User user in Users)
                {
                    if (parametr is int value && value == user.Id)
                    {
                        ActiveReceiver = user;
                        MessageManager.SetReceiveTime(ActiveUser.Id, ActiveReceiver.Id);
                        UpdateUserMessages();
                        FiltredMessages.Refresh();

                        OnPropertyChanged(nameof(Messages));
                        break;
                    }
                }
            }
        }

        //Filtr na zobrazování zpráv uživatele a příjemce
        private bool FilterMessages(object obj)
        {
            var msg = obj as UserMessage;
            if (msg == null) return false;

            return ((msg.SenderId == ActiveUser.Id && msg.ReceiverId == ActiveReceiver.Id) ||
                    (msg.SenderId == ActiveReceiver.Id && msg.ReceiverId == ActiveUser.Id));
        }

        //Spojuje Messages a Usery. Je později filtrovaná
        public void UpdateUserMessages()
        {
            if (Messages == null) Messages = new ObservableCollection<Message>();
            if (Users == null) Users = new ObservableCollection<User>();
            if (ActiveUser == null) return;

            var displayList = new ObservableCollection<UserMessage>();

            foreach (Message m in Messages)
            {
                if (m == null) continue;

                string jmeno = "Chyba";

                if (m.ReceiverId == ActiveUser.Id)
                {
                    jmeno = ActiveUser.Username ?? "Chyba";
                }
                else
                {
                    foreach (User user in Users)
                    {
                        if (user != null && m.ReceiverId == user.Id)
                        {
                            jmeno = user.Username ?? "Chyba";
                            break;
                        }
                    }
                }

                displayList.Add(new UserMessage
                {
                    MessageContent = m.MessageContent ?? "",
                    SendTime = m.SendTime,
                    ReceiveTime = m.ReceiveTime,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    Username = jmeno
                });
            }

            if (UserMessages == null)
                UserMessages = new ObservableCollection<UserMessage>();
            else
                UserMessages.Clear();

            foreach (var um in displayList)
                UserMessages.Add(um);

            OnPropertyChanged(nameof(UserMessages));
        }

    }


}
