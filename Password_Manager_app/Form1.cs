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
        string loggedInUser;
        public Form1()
        {
            InitializeComponent();
            FileDecrypt(@"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt.aes", @"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt", "123");
            File.Delete(@"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt.aes");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileEncrypt(@"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt", "123");
            File.Delete(@"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt");
            FileEncrypt($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{loggedInUser}File.txt", "123");
            File.Delete($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{loggedInUser}File.txt");

        }

        private void FileEncrypt(string inputFile, string password)
        {
            byte[] salt = GenerateSalt();
            byte[] passwords = Encoding.UTF8.GetBytes(password);
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;//aes 256 bit encryption c#
            AES.BlockSize = 128;//aes 128 bit encryption c#
            AES.Padding = PaddingMode.PKCS7;
            var key = new Rfc2898DeriveBytes(passwords, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Mode = CipherMode.CFB;
            using (FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create))
            {
                fsCrypt.Write(salt, 0, salt.Length);
                using (CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                    {
                        byte[] buffer = new byte[1048576];
                        int read;
                        while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            cs.Write(buffer, 0, read);
                        }
                    }
                }
            }
        }
        public static byte[] GenerateSalt()
        {
            byte[] data = new byte[32];
            using (RNGCryptoServiceProvider rgnCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rgnCryptoServiceProvider.GetBytes(data);
            }
            return data;
        }
        private void FileDecrypt(string inputFileName, string outputFileName, string password)
        {
            byte[] passwords = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];
            using (FileStream fsCrypt = new FileStream(inputFileName, FileMode.Open))
            {
                fsCrypt.Read(salt, 0, salt.Length);
                RijndaelManaged AES = new RijndaelManaged();
                AES.KeySize = 256;//aes 256 bit encryption c#
                AES.BlockSize = 128;//aes 128 bit encryption c#
                var key = new Rfc2898DeriveBytes(passwords, salt, 50000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Padding = PaddingMode.PKCS7;
                AES.Mode = CipherMode.CFB;
                using (CryptoStream cryptoStream = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (FileStream fsOut = new FileStream(outputFileName, FileMode.Create))
                    {
                        int read;
                        byte[] buffer = new byte[1048576];
                        while ((read = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fsOut.Write(buffer, 0, read);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string value;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                StreamReader sr = new StreamReader(@"C:\Users\Domantas\Desktop\New folder\UsersDataBase.txt");
                while (sr.Peek() > -1)
                {
                    value = sr.ReadLine();
                    if (!value.Contains(":"))
                    {
                        continue;
                    }
                    if (rw.ValidatePassword(textBox2.Text, value))
                    {
                        loggedInUser = textBox1.Text;
                        FileDecrypt($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{loggedInUser}File.txt.aes", $@"C:\Users\Domantas\Desktop\New folder\UserFiles\{loggedInUser}File.txt", "123");
                        File.Delete($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{loggedInUser}File.txt.aes");
                        UserWindow uw = new UserWindow();
                        uw.ShowDialog();
                        sr.Close();
                        break;
                    }
                }
                if(loggedInUser == null)
                {
                    MessageBox.Show("bad credentials.");
                }
                sr.Close();

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
    }
}