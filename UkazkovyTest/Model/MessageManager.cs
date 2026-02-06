using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace UkazkovyTest.Model
{
    class MessageManager
    {
        //Message manage čte a zapisuje XML soubour
        //následně předává do MainWindow ViewModelu


        private static DateTime? ParseDate(XElement element)
        {
            if (element == null)
                return null;

            if (DateTime.TryParse(element.Value, out var result))
                return result;

            return null;
        }

        //Čte XML dB
        public static ObservableCollection<Message> ReadDB(string path)
        {
            XDocument doc = XDocument.Load(path);

            var messages = doc.Root
                .Elements("Message")
                .Select(x => new Message
                {
                    SendTime = ParseDate(x.Element("SentTime")) ?? DateTime.MinValue,
                    ReceiveTime = ParseDate(x.Element("ReceiveTime")),
                    MessageContent = (string)x.Element("MessageContent"),
                    SenderId = (int?)x.Element("SenderId") ?? 0,
                    ReceiverId = (int?)x.Element("ReceiverId") ?? 1,

                });

            return new ObservableCollection<Message>(messages);
        }



        public static string relativePath = @"Model\MessageDatabase2.xml";
        public static string absolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        public static ObservableCollection<Message> _MessageDatabase = ReadDB(absolutePath);

        public static ObservableCollection<Message> GetMessages()
        {

            return _MessageDatabase;
        }

        //Tvorba nové zprávy
        public static void NewMessage(string Text, int R, int S)
        {
            Message message = new Message();
            message.SendTime = DateTime.Now;
            message.MessageContent = Text;
            message.ReceiverId = R;
            message.SenderId = S;
            _MessageDatabase.Add(message);
            AddMessage(absolutePath, message);
        }

        //Zmena casu doruceni
        public static void SetReceiveTime(int S, int R)
        {
            foreach(Message message in _MessageDatabase)
            {
                if((message.ReceiverId == R && message.SenderId == S)&&(message.ReceiveTime==null))
                {
                    message.ReceiveTime = DateTime.Now;
                    SetXMLReceive(absolutePath, R, S);
                }
            }
        }

        //XML
        public static void AddMessage(string filePath, Message msg)
        {
            XDocument doc;


            if (File.Exists(filePath))
            {
                doc = XDocument.Load(filePath);
            }
            else
            {
                doc = new XDocument(new XElement("messages"));
            }


            XElement messageElement =
                new XElement("Message",
                    new XElement("SenderId", msg.SenderId),
                    new XElement("ReceiverId", msg.ReceiverId),
                    new XElement("MessageContent", msg.MessageContent),
                    new XElement("SentTime", msg.SendTime),
                    new XElement("ReceiveTime",msg.ReceiveTime)
                );

            doc.Root!.Add(messageElement);

            doc.Save(filePath);
        }

        public static void SetXMLReceive(string filePath, int R, int S)
        {
            XDocument doc;
            doc = XDocument.Load(filePath);
            foreach (var message in doc.Descendants("Message"))
            {
                var rElement = message.Element("ReceiverId");
                var sElement = message.Element("SenderId");
                var timeElement = message.Element("ReceiveTime");
                string newTime = DateTime.Now.ToString();

                if (rElement != null && sElement != null && timeElement != null)
                {
                    int r = int.Parse(rElement.Value);
                    int s = int.Parse(sElement.Value);

                    if (r == R && s == S && string.IsNullOrEmpty(timeElement.Value))
                    {
                        timeElement.Value = newTime.ToString();
                    }
                }
            }
            doc.Save(absolutePath);
        }

            
    }
}
