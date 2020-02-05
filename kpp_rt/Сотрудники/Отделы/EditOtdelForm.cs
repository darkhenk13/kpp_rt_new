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

namespace kpp_rt.Сотрудники.Отделы
{
    public partial class EditOtdelForm : Form
    {
        public EditOtdelForm()
        {
            InitializeComponent();
        }

        public string[] arr1 = new string[1];
        public string id_otdel;

        private void button1_Click(object sender, EventArgs e)
        {
            OtdelSotrudForm form = new OtdelSotrudForm();
            this.Hide();
            form.Show();
        }

        private void EditOtdelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OtdelSotrudForm form = new OtdelSotrudForm();
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBox1.Text.Equals(""))
                {
                    MessageBox.Show("Заполните поле","Ошибка");
                }
                else
                {
                    SqlConnection connection1 = new SqlConnection(Form1.connectString);
                    SqlCommand command1 = new SqlCommand();
                    command1.Connection = connection1;
                    command1.CommandText = @"UPDATE Отделы SET Отдел='" + textBox1.Text + "'  WHERE ID_Отдела=" + id_otdel + "";
                    connection1.Open();
                    command1.ExecuteNonQuery();
                    connection1.Close();

                    MessageBox.Show("Данные изменены!");


                    Class1 clas = new Class1();
                    clas.users_ychet("Редактирование отдела");

                    OtdelSotrudForm form = new OtdelSotrudForm();
                    this.Hide();
                    form.Show();

                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void EditOtdelForm_Load(object sender, EventArgs e)
        {
            /*CREATE TABLE Отделы(
	ID_Отдела int IDENTITY (1,1),
	Отдел nvarchar(100) NULL,
	PRIMARY KEY (ID_Отдела)
);*/

            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            this.MaximizeBox = false;
            this.MinimizeBox = false;


            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_Отдела, Отдел FROM Отделы WHERE Отдел='" + arr1[0] + "'";

            // command.CommandText = "SELECT ID_Пользователь, login, password, role, Фамилия, Имя, Отчество, ДатаРегистрации FROM users WHERE ID_Пользователь=" + id + "";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_otdel = reader[0].ToString();
                textBox1.Text = reader[1].ToString();
            }
            connection.Close();
        }


    }
}
