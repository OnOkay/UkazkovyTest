using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace UkazkovyTest.Model
{
    [XmlRoot("UserDatabase")]
    public class UserDatabase
    {
        [XmlElement("User")]
        public List<User> Users { get; set; }
    }
    public class User
    {
        public User() 
        {
        
        }
        [XmlElement("id")]
        public int id {  get; set; }
        [XmlElement("name")]
        public string name { get; set; }
        [XmlElement("surname")]
        public string surname { get; set; }
        [XmlElement("email")]
        public string email { get; set; }
        [XmlElement("username")]
        public string username { get; set; }
        [XmlElement("password")]
        public string password { get; set; }

    }
}
