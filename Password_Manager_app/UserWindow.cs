using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Manager_app
{
    public partial class UserWindow : Form
    {

        Encryption encryption = new Encryption();
        public UserWindow()
        {
            InitializeComponent();
            label1.Text = $"Hello, {Form1.loggedInUser}!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void UserWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            NewPasswordWindow npw = new NewPasswordWindow();
            npw.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PasswordList pl = new PasswordList();
            pl.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void UserWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            encryption.FileEncrypt($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt", "123");
            File.Delete($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt");
            Form1.loggedInUser = null;

        }
    }
}
