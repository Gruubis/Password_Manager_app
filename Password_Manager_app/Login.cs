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
            IEnumerable<string> usernames = lines.Where(line => line.Contains(username));
            IEnumerable<string> hashes = lines.Where(line => line.Contains(":"));
            if (usernames.Count() != 0) {
                foreach (string hash in hashes)
                {
                    if (encryption.ValidatePassword(password, hash)){
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
           
            return true;
        }
    }
}
