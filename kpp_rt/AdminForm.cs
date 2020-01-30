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
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);



            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;



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


            command1.Connection = connection1;
            command1.CommandText = @"SELECT Log_users.ID_LogUsers AS [ID], Log_users.Время, Log_users.Дата, Log_users.Действие, ПерссональныеДанныеСотрудника.ФИО
FROM Log_users
LEFT JOIN ПользователиПрограммы ON Log_users.ID_Users = ПользователиПрограммы.ID_Users
LEFT JOIN Сотрудники ON ПользователиПрограммы.ID_Users = Сотрудники.ID_Users
LEFT JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника";
            connection1.Open();
            adap1.SelectCommand = command1;
            adap1.Fill(ds1);
            dt1 = ds1.Tables[0];
            dataGridView3.DataSource = dt1;
            connection1.Close();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ComPortSettingsForm form = new ComPortSettingsForm();
            this.Hide();
            form.Show();
            
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class1 clas = new Class1();
            clas.users_ychet("Выход из панели администратора");

            Form1 form = new Form1();
            this.Hide();
            form.Show();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateAdminForm form = new CreateAdminForm();
            this.Hide();
            form.Show();
        }
    }
}
