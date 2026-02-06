using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UkazkovyTest.Model
{
    public class Message:INotifyPropertyChanged
    {
        public Message()
        {

        }
        public DateTime? SendTime { get; set; } 
        public string MessageContent { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        private DateTime? receiveTime;

        public DateTime? ReceiveTime
        {
            get => receiveTime;
            set
            {
                if (receiveTime != value)
                {
                    receiveTime = value;
                    OnPropertyChanged(nameof(ReceiveTime));
                }
            }
        }
        //Potrebuji abych upozornil tlacitka, že se objevil ReceiveTime
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
