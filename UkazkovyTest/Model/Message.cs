using System;
using System.Collections.Generic;
using System.Text;

namespace UkazkovyTest.Model
{
    class Message
    {
        public Message()
        {

        }
        public DateOnly DateSend {  get; set; }
        public TimeOnly TimeSend { get; set; }
        public string MessageContent { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateOnly DateReceive { get; set; }
        public TimeOnly TimeReceive { get; set; }

    }
}
