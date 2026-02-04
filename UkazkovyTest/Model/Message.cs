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
       // public DateTime SendTime { get; set; } 
       // public DateTime ReceiveTime { get; set; }
        public string MessageContent { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }


    }
}
