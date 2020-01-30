﻿using System;
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

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style1 = dataGridView2.ColumnHeadersDefaultCellStyle;
            style1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style2 = dataGridView3.ColumnHeadersDefaultCellStyle;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;



            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            command.Connection = connection;
            command.CommandText = @"SELECT пп.login AS [Логин], перс.ФИО AS [ФИО Сотрудника], перс.Дата_Рождения AS [Дата рождения], прав.Права_Доступа AS [Права Доступа]
FROM ПользователиПрограммы пп, Сотрудники ст, ПерссональныеДанныеСотрудника перс, ПраваДоступа прав
WHERE пп.ID_Сотруднка = ст.ID_Сотрудника
AND ст.ID_ПерснСотрудника = перс.ID_ПерснСотрудника
AND пп.ID_ПравДоступа = прав.ID_ПравДоступа";
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


            table_3();

        }



       private void table_3()
        {
            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();
            DataSet ds1 = new DataSet();
            SqlDataAdapter adap1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();

            command1.Connection = connection1;
            command1.CommandText = @"SELECT 
лю.ID_LogUsers AS [ID], 
лю.Время, 
лю.Дата, 
лю.Действие, 
пдс.ФИО AS [ФИО Сотрудника],
пдс.Дата_Рождения AS [Дата рождения]
FROM Log_users лю, ПользователиПрограммы пп, Сотрудники сот, ПерссональныеДанныеСотрудника пдс
WHERE лю.ID_Users = пп.ID_Users
AND пп.ID_Сотруднка = сот.ID_Сотрудника
AND сот.ID_ПерснСотрудника = пдс.ID_ПерснСотрудника";
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
