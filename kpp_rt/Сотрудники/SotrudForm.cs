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
    public partial class SotrudForm : Form
    {
        public SotrudForm()
        {
            InitializeComponent();
        }

        private void SotrudForm_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            command.Connection = connection;
            command.CommandText = @"SELECT Сотрудники.ID_Users, Сотрудники.Дата_Регистрации_Сотрудника, ПерссональныеДанныеСотрудника.ФИО FROM Сотрудники FULL JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
