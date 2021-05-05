using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Password_Manager_app
{
    public partial class NewPasswordWindow : Form
    {
        Image img = new Bitmap(@"C:\Users\Domantas\Downloads\eye-with-thick-outline-variant.png");
        Image img2 = new Bitmap(@"C:\Users\Domantas\Downloads\hide-interface-symbol.png");
        Encryption encryption = new Encryption();
        public NewPasswordWindow()
        {
            InitializeComponent();
            Image img = new Bitmap(@"C:\Users\Domantas\Downloads\eye-with-thick-outline-variant.png");
            button2.Image = new Bitmap(img, new Size(32, 23));
        }
      
        private void label4_Click(object sender, EventArgs e)
        {
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
            {
                button2.Image = new Bitmap(img2, new Size(32, 23));
                textBox3.PasswordChar = '\0';
            }
            else
            {
                button2.Image = new Bitmap(img, new Size(32, 23));
                textBox3.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = RandomPassword();
        }
        public string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            StreamWriter sw = File.AppendText($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt");
            sw.WriteLine($"{textBox1.Text},{textBox2.Text},{encryption.encryptText(textBox3.Text)},{textBox4.Text}");
            sw.Close();
            MessageBox.Show("Password Successfully added!");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}
