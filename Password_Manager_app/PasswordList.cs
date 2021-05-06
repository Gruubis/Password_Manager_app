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
    public partial class PasswordList : Form
    {
        Encryption encryption = new Encryption();
        CheckBox button = new CheckBox();
        Image img = new Bitmap(@"C:\Users\Domantas\Downloads\eye-with-thick-outline-variant.png");
        public static string Password;
        public static int line;
        public PasswordList()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PasswordList_Load(object sender, EventArgs e)
        {
        }

        
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            button.Appearance = Appearance.Button;
            dataGridView1.Rows.Clear();
            string[] lines = File.ReadAllLines($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt");
            string[] splitedLine;
            for (int i = 0; i < lines.Length; i++)
            {
                splitedLine = lines[i].Split(",");
                if (textBox1.Text.Contains(splitedLine[0]))
                {
                    dataGridView1.Rows[0].Cells[0].Value = splitedLine[0];
                    dataGridView1.Rows[0].Cells[1].Value = splitedLine[1];
                    dataGridView1.Rows[0].Cells[2].Value = splitedLine[2];
                    dataGridView1.Rows[0].Cells[3].Value = splitedLine[3];
                    dataGridView1.Rows[0].Cells[4].Value = button;
                }

            }
            dataGridView1.Height = dgvHeight();

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                DataGridViewButtonCell btn = dataGridView1.Rows[e.RowIndex].Cells[4] as DataGridViewButtonCell;

                CheckBox b = (CheckBox)btn.Value;
                if (!b.Checked)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[2].Value = encryption.decryptText(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    b.Checked = true;
                }
                else
                {
                    b.Checked = false;
                    dataGridView1.Rows[e.RowIndex].Cells[2].Value = encryption.encryptText(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                }
            }
            else if (e.ColumnIndex == 2)
            {
                string[] lines = File.ReadAllLines($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt");
                Clipboard.SetText(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                string[] pass = lines[e.RowIndex].Split(",");
                Password = pass[2];
                line = e.RowIndex;
                OptionWindow ow = new OptionWindow();
                ow.ShowDialog();
            }
            else
            {
                Clipboard.SetText(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                MessageBox.Show("Copied to Clipboard");

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string[] lines = File.ReadAllLines($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt");
            CheckBox[] buttons = new CheckBox[lines.Length];
            string[] splitedLine;
            for (int i = 0; i < lines.Length; i++)
            {
                splitedLine = lines[i].Split(",");
                buttons[i] = new CheckBox();
                buttons[i].Text = "Show password";
                buttons[i].Appearance = Appearance.Button;
                buttons[i].ForeColor = SystemColors.Control;
                dataGridView1.Rows.Add(new object[] { splitedLine[0], splitedLine[1], splitedLine[2], splitedLine[3], buttons[i] });
                };
            dataGridView1.Height = dgvHeight();
            }
        public int dgvHeight()
        {
            int sum = this.dataGridView1.ColumnHeadersHeight;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
                sum += row.Height + 1; // I dont think the height property includes the cell border size, so + 1

            return sum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string newPassword = Prompt.ShowDialog("Enter new Password", "Change Password");
            string text = File.ReadAllText($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt");
            text = text.Replace(dataGridView1.Rows[0].Cells[2].Value.ToString(), encryption.encryptText(newPassword));
            File.WriteAllText($@"C:\Users\Domantas\Desktop\New folder\UserFiles\{Form1.loggedInUser}File.txt", text);
        }
        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 4)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.hide_interface_symbol__1_.Width;
                var h = Properties.Resources.hide_interface_symbol__1_.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }
    }
}
