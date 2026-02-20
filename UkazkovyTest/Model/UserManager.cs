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
            try
            {
                XDocument doc = XDocument.Load(path);

                var users = doc.Root
                    .Elements("User")
                    .Select(x => new User()
                    {
                        Name = (string)x.Element("name"),
                        Surname = (string)x.Element("surname"),
                        Id = (int?)x.Element("id") ?? 0,
                        Username = (string)x.Element("username"),
                        Email = (string)x.Element("email"),
                        Password = (string)x.Element("password")
                    });

                return new ObservableCollection<User>(users);
            }
            catch (Exception ex)
            {
                return new ObservableCollection<User>();
            }
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
