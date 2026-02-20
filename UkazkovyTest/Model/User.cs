using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace UkazkovyTest.Model
{

    public class User
    {
        //Inicializace proměných
        /*public User(int id, string name, string surname, string email, string username, string password) 
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Username = username;
            Password = password;
        }*/
        public User() { }

        public int Id {  get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
