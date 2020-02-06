using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kpp_rt
{
    public partial class EditAdminForm : Form
    {
        public EditAdminForm()
        {
            InitializeComponent();
        }
        public string[] arr_del1 = new string[4];
        private void EditAdminForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = arr_del1[1];
            textBox2.Text = arr_del1[2];
            textBox3.Text = arr_del1[3];
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();

        }

        private void EditAdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();

        }
    }
}
