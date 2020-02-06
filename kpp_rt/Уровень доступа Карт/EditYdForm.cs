using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace kpp_rt.Уровень_доступа_Карт
{
    public partial class EditYdForm : Form
    {
        public EditYdForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateYtKlientForm form = new CreateYtKlientForm();
            this.Hide();
            form.Show();

        }

        private void EditYdForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CreateYtKlientForm form = new CreateYtKlientForm();
            this.Hide();
            form.Show();
        }

        private void EditYdForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        }
    }
}
