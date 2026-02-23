using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace UkazkovyTest.Model
{

    public class User
    {
        //Stringy jsou inicializované
        public User() { }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
