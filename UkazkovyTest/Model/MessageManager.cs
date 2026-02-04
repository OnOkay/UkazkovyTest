using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;

namespace UkazkovyTest.Model
{
    class MessageManager
    {
        /*
        public static ObservableCollection<User> ReadDB(string path)
        {
            XDocument doc = XDocument.Load(path);

            var messages = doc.Root
                .Elements("Message")
                .Select(x => new 
                {

                    
                });

            return new ObservableCollection<User>(messages);
        }


        //Toto upravit na relativní cestu@".\pigeonImages\pigeon5.png"
        public static ObservableCollection<User> _UserDatabase = ReadDB("C:\\Users\\ondra\\source\\repos\\UkazkovyTest\\UkazkovyTest\\Model\\MessageDatabase.xml");


        public static ObservableCollection<User> GetUsers()
        {

            return _UserDatabase;
        }
        */

        public static ObservableCollection<Message> _MessageDatabase = new ObservableCollection<Message>() {  new Message() { MessageContent = "Ahoj", ReceiverId =2, SenderId =1}, new Message() { MessageContent = "Ahoj", ReceiverId = 2, SenderId = 1 }, new Message() { MessageContent = "Ahoj", ReceiverId = 2, SenderId = 1 } };


        public static ObservableCollection<Message> GetMessages()
        {

            return _MessageDatabase;
        }
    }
}
