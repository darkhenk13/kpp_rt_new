using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kpp_rt.Сотрудники
{
    public partial class EditSotrudForm : Form
    {
        public EditSotrudForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();
        }

        private void EditSotrudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();
        }
    }
}
