using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using UkazkovyTest.Model;

namespace UkazkovyTest.ViewModel
{
    public interface MessageInterface
    {

        ObservableCollection<Message> GetMessages();
        void NewMessage(string Content, int SenderId, int ReceiverId);
        void SetReceiveTime(int SenderId, int ReceiverId);


    }
}
