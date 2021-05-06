using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Manager_app
{
    public partial class Form1 : Form
    {
        RegisterWindow rw = new RegisterWindow();
        Encryption encryption = new Encryption();
        Login login = new Login();
        public static string loggedInUser;
        string aesFile = @"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt.aes";
        string txtFile = @"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt";
        string userfile = $@"C:\Users\Domantas\Desktop\New folder\UserFiles\";
        string password = "123";
        public Form1()
        {
            InitializeComponent();
            encryption.FileDecrypt(aesFile, txtFile, password);
            File.Delete(aesFile);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            encryption.FileEncrypt(txtFile, password);
            File.Delete(txtFile);


        }
       


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (login.loginUser(textBox1.Text, textBox2.Text))
                {
                    loggedInUser = textBox1.Text;
                    encryption.FileDecrypt($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{loggedInUser}File.txt.aes", userfile + $"{loggedInUser}File.txt", password);
                    File.Delete(userfile + $"{loggedInUser}File.txt.aes");
                    UserWindow uw = new UserWindow();
                    uw.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bad Credentials!");
                }
            }
            else
            {
                MessageBox.Show("Username or password field is empty try again");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.ShowDialog();
        }
        public string getLoggedUser()
        {
            return loggedInUser;
        }
    }
}