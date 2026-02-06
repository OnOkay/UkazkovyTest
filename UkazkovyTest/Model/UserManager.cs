using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;


namespace UkazkovyTest.Model
{
    class UserManager
    {
        //Usermanager pouze cte z databaze a predava data oboum ViewModelum
        public static ObservableCollection<User> ReadDB (string path)
        {
            XDocument doc = XDocument.Load(path);

            var users = doc.Root
                .Elements("User")
                .Select(x => new User
                {
                    name = (string)x.Element("name"),
                    surname = (string)x.Element("surname"),
                    id = (int?)x.Element("id") ?? 0,
                    username = (string)x.Element("username"),
                    email = (string)x.Element("email"),
                    password = (string)x.Element("password")
                });

            return new ObservableCollection<User>(users);
        }

        public static string relativePath = @"Model\UserDatabase.xml";
        public static string absolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        public static ObservableCollection<User> _UserDatabase = ReadDB(absolutePath);
      

        public static ObservableCollection<User> GetUsers()
        {
            
            return _UserDatabase;
        }

    }
}
