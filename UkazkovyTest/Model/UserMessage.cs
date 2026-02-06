using System;
using System.Collections.Generic;
using System.Text;

namespace UkazkovyTest.Model
{
    public class UserMessage
    {

        //Kombinuje obe tridy, abych mohl pres listview ukazovat jmeno misto id
        public string MessageContent { get; set; }
        public DateTime? SendTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public string Username { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }


    }
}
