using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace UkazkovyTest.Model
{
    class MessageManager
    {


        private static DateTime? ParseDate(XElement element)
        {
            if (element == null)
                return null;

            if (DateTime.TryParse(element.Value, out var result))
                return result;

            return null;
        }

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
                    ReceiverId = (int?)x.Element("ReceiverId") ?? 0,
                    
                });

            return new ObservableCollection<Message>(messages);
        }


        public static string relativePath = @"Model\MessageDatabase.xml";
        public static string absolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        public static ObservableCollection<Message> _MessageDatabase = ReadDB(absolutePath);

        public static ObservableCollection<Message> GetMessages()
        {

            return _MessageDatabase;
        }

        public static void NewMessage(string Text, int R, int S)
        {
            Message x = new Message();
            x.SendTime=DateTime.Now;
            x.MessageContent = Text;
            x.ReceiverId = R;
            x.SenderId = S;
            _MessageDatabase.Add(x);
            AddMessage(absolutePath, x);
        }
        
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
                    new XElement("SentTime",msg.SendTime)
                );

            doc.Root!.Add(messageElement);

            doc.Save(filePath);
          }         
         
    }
}
