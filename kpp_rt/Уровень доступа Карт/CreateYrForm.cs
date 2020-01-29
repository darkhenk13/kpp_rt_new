using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kpp_rt.Уровень_доступа_Карт
{
    public partial class CreateYrForm : Form
    {
        public CreateYrForm()
        {
            InitializeComponent();
        }

        private void CreateYrForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
            radioButton1.Checked = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked != true)
            {
                MessageBox.Show("123");
            }
            else if (radioButton2.Checked != true)
            {
                // Сотрудники
                CreateYtSotrudForm form = new CreateYtSotrudForm();
                this.Hide();
                form.Show();

            }
        }

        private void CreateYrForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();
        }
    }
}
