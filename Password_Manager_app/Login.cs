using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager_app
{
    class Login
    {
        private Encryption encryption = new Encryption();
        public bool loginUser(string username, string password)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt");

            if (lines.Contains(username)) {
              int hash = Array.IndexOf(lines, username) +1;
                    if (encryption.ValidatePassword(password, lines[hash]))
                        return true;
                    else
                        return false;
               
            }
            else
            { 
                return false;
            }

        }
    }
}
