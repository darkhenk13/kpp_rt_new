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

namespace kpp_rt
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

       string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;

        


        private void AdminForm_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            command.Connection = connection;
            command.CommandText = @"SELECT * FROM ПользователиПрограммы";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();

            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();
            DataSet ds1 = new DataSet();
            SqlDataAdapter adap1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();

            command1.Connection = connection1;
            command1.CommandText = @"SELECT * FROM ПраваДоступа";
            connection1.Open();
            adap1.SelectCommand = command1;
            adap1.Fill(ds1);
            dt1 = ds1.Tables[0];
            dataGridView2.DataSource = dt1;
            connection1.Close();
        }
    }
}
