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
    public partial class RegisterWindow : Form
    {
        private Encryption encryption = new Encryption();
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterWindow_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text == textBox3.Text)
            {
                try
                {
                    register(textBox1.Text, textBox2.Text);
                    MessageBox.Show("Registered successfully!");
                    this.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        public void register(string username, string password)
        {
            StreamReader sr = new StreamReader(@"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt");
            while (sr.Peek() > 0)
            {
                if (sr.ReadLine() == username)
                    throw new Exception($"Username - { username } already exists!");
            }
            sr.Close();
            StreamWriter sw = new StreamWriter(@"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt", true);
            sw.WriteLine(username);
            sw.WriteLine(encryption.HashPassword(password));
            sw.Close();
            File.Create($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{username}File.txt");
        }
        

    }
}
