using System;
using System.Collections.Generic;
using System.Text;
using UkazkovyTest.Model;

namespace UkazkovyTest.ViewModel
{
    public class MessageUserModel
    {
        public DateTime? SendTime { get; }
        public DateTime? ReceiveTime {  get; }
        public int SenderId { get; }
        public int ReceiverId {  get; }
        public string UserName { get; }
        public string Text { get; }

        public MessageUserModel (Message message, User user)
        {
            UserName = user.username;
            Text = message.MessageContent;
            SendTime = message.SendTime;
            ReceiveTime = message.ReceiveTime;
            SenderId = message.SenderId;
            ReceiverId = message.ReceiverId;
        }
    }
}
