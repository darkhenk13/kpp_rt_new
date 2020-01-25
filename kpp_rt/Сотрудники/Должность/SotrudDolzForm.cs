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

namespace kpp_rt.Сотрудники.Должность
{
    public partial class SotrudDolzForm : Form
    {
        public SotrudDolzForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateDolzSotrudForm form = new CreateDolzSotrudForm();
            this.Hide();
            form.Show();
        }

        private void SotrudDolzForm_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();
            command.Connection = connection;

            command.CommandText = @"SELECT Должность FROM Должность";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();




        }
    }
}
