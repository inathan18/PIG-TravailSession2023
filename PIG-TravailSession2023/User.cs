using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIG_TravailSession2023
{
    class User
    {
        string username, password;

        public User() { }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username
        {
            get => username; set => username = value;
        }

        public string Password
        {
            get => password; set => password = value;
        }
    }
}
