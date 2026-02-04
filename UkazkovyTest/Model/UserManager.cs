using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;



namespace UkazkovyTest.Model
{
    class UserManager
    {
        //ForEach User v xml filu potrebuji new User(). Ve kterém pak rozeberu každého
        //1. deserializace a následně funkce
        
        
        //Foreach xml User udelej toto:
        //Deserializuj User(id = .., ...)
        //_UserDatabase.Adduser(...)
        public static ObservableCollection<User> _UserDatabase = new ObservableCollection<User>() { new User() {id = 1, name="a", surname="a",
        email="a", username="a1", password="a"}, new User() {id = 2, name="b", surname="b",
        email="b", username="b2", password="b"}, new User() {id = 3, name="c", surname="c",
        email="c", username="c3", password="c"}};
        

        public static ObservableCollection<User> GetUsers()
        {
            
            return _UserDatabase;
        }

    }
}
