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
    public partial class EditDolzForm : Form
    {
        public EditDolzForm()
        {
            InitializeComponent();
        }

        public string[] arr1 = new string[1];
        public string id_dol;

        private void EditDolzForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.MinimumSize = new System.Drawing.Size(230, 140);
            this.MaximumSize = new System.Drawing.Size(230, 140);
            /*
             CREATE TABLE Должность(
	ID_Должность int IDENTITY (1,1),
	Должность nvarchar(100) NULL,
	PRIMARY KEY (ID_Должность)
);
             */



            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_Должность, Должность FROM Должность WHERE Должность='" + arr1[0] + "'";

            // command.CommandText = "SELECT ID_Пользователь, login, password, role, Фамилия, Имя, Отчество, ДатаРегистрации FROM users WHERE ID_Пользователь=" + id + "";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_dol = reader[0].ToString();
                textBox1.Text = reader[1].ToString();               
            }
            connection.Close();

          


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection1;
            command1.CommandText = @"UPDATE Должность SET Должность='" + textBox1.Text + "'  WHERE ID_Должность=" + id_dol + "";
            connection1.Open();
            command1.ExecuteNonQuery();
            connection1.Close();

            MessageBox.Show("Данные изменены!");

            Class1 clas = new Class1();
            clas.users_ychet("Добавлене изменение должности");

            SotrudDolzForm form = new SotrudDolzForm();
            this.Hide();
            form.Show();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            SotrudDolzForm form = new SotrudDolzForm();
            this.Hide();
            form.Show();
        }

        private void EditDolzForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SotrudDolzForm form = new SotrudDolzForm();
            this.Hide();
            form.Show();
        }
    }
}
