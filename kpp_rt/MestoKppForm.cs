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
    public partial class MestoKppForm : Form
    {
        public MestoKppForm()
        {
            InitializeComponent();
        }

        private void MestoKppForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();
        }

        private void MestoKppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();
        }
    }
}
