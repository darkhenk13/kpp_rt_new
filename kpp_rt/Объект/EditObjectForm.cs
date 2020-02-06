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

namespace kpp_rt.Объект
{
    public partial class EditObjectForm : Form
    {
        public EditObjectForm()
        {
            InitializeComponent();
        }
        public string[] arr_object1 = new string[4];
        
        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;
        public string id_object;

        private void EditObjectForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            //for (int j = 0; j < 4; j++)
            //{
            //    MessageBox.Show(Convert.ToString(j), arr_object1[j]);

            //}

            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_Объекта, Город, Улица, Здание, Этаж FROM Объект WHERE Город='" + arr_object1[0] + "' AND Улица='" + arr_object1[1] + "' AND Здание='" + arr_object1[2] + "'AND Этаж='" + arr_object1[3] + "'";

            // command.CommandText = "SELECT ID_Пользователь, login, password, role, Фамилия, Имя, Отчество, ДатаРегистрации FROM users WHERE ID_Пользователь=" + id + "";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_object = reader[0].ToString();
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
                textBox3.Text = reader[3].ToString();
                textBox4.Text = reader[4].ToString();
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection1 = new SqlConnection(connectString);
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection1;
            command1.CommandText = @"UPDATE Объект SET Город='" + textBox1.Text + "', Улица='" + textBox2.Text + "', Здание='" + textBox3.Text + "', Этаж='" + textBox4.Text + "'  WHERE ID_Объекта='" + id_object + "'";
            connection1.Open();
            command1.ExecuteNonQuery();
            connection1.Close();

            MessageBox.Show("Данные изменены!");

            Class1 clas = new Class1();
            clas.users_ychet("Изменение объекта");

            ObjectForm form = new ObjectForm();
            this.Hide();
            form.Show();



        }

        private void EditObjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ObjectForm form = new ObjectForm();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ObjectForm form = new ObjectForm();
            this.Hide();
            form.Show();
        }
    }
}
