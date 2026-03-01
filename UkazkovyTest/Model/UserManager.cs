using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using UkazkovyTest.ViewModel;


namespace UkazkovyTest.Model
{
    class UserManager : UserInterface
    {
        //Usermanager pouze cte z databaze a predava data oboum ViewModelum


        private readonly string _filePath;
        private ObservableCollection<User> _database;

        public UserManager(string filePath)
        {
            _filePath = filePath;
            _database = ReadDB(_filePath);
        }

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


        public static string FilePath = @"..\..\..\Model\UserDatabase.xml";
        public static ObservableCollection<User> _UserDatabase = ReadDB(FilePath);
      

        
        public ObservableCollection<User> GetUsers()
        {
            
            return _UserDatabase;
        }
        

    }
}
