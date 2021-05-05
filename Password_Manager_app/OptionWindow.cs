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
    public partial class OptionWindow : Form
    {
        public OptionWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePasswordWindow cpw = new ChangePasswordWindow();
            cpw.ShowDialog();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete password?", "Delete password", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] lines = File.ReadAllLines($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt");
                File.WriteAllLines($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt", lines.Where((line) => line != lines[PasswordList.line]));
                MessageBox.Show("Deleted Successfully");
                Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                Close();
            }
        }
    }
}
