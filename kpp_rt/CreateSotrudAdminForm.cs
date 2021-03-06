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
    public partial class CreateSotrudAdminForm : Form
    {
        public CreateSotrudAdminForm()
        {
            InitializeComponent();
        }
        
        public string[] arr1 = new string[4];

        private void CreateSotrudAdminForm_Load(object sender, EventArgs e)
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
            command.CommandText = @"SELECT Сотрудники.Дата_Регистрации_Сотрудника, ПерссональныеДанныеСотрудника.ФИО, ПерссональныеДанныеСотрудника.Номер_телефона, ПерссональныеДанныеСотрудника.Дата_Рождения, Отделы.Отдел, Должность.Должность
FROM Сотрудники 
JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника
JOIN Отделы ON Сотрудники.ID_Отдела = Отделы.ID_Отдела
JOIN Должность ON Сотрудники.ID_Должность = Должность.ID_Должность";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();

        }

        private void CreateSotrudAdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            CreateAdminForm form = new CreateAdminForm();
            this.Hide();
            form.arr = arr1;
            form.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Редактирование строки
            for (int i = 0; i < 4; i++)
            {
                arr1[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }

            CreateAdminForm form = new CreateAdminForm();
            this.Hide();
            form.arr = arr1;
            form.Show();

        }
    }
}
