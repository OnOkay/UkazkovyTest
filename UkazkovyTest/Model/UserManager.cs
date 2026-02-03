using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UkazkovyTest.Model
{
    class UserManager
    {
        //Manager bude tahat z databaze uzivatele
        //Prozatim takhle
        public static ObservableCollection<User> _UserDatabase = new ObservableCollection<User>() { new User() {id = 1, name="a", surname="a", 
        email="a", username="a1", password="a"}, new User() {id = 2, name="b", surname="b",
        email="b", username="b2", password="b"}};

        public static ObservableCollection<User> GetUsers()
        {
            return _UserDatabase;
        }

    }
}
