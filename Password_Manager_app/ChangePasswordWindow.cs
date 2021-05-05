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
    public partial class ChangePasswordWindow : Form
    {
        Encryption encryption = new Encryption();
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == textBox2.Text)
            {
                string text = File.ReadAllText($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt");
                text = text.Replace(PasswordList.Password, encryption.encryptText(textBox1.Text));
                File.WriteAllText($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt", text);
                MessageBox.Show("Password changed");
                Close();
            }
            else
            {
                MessageBox.Show("Passwords should match");

            }
        }
    }
    }

