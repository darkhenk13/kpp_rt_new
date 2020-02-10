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

namespace kpp_rt.Клиенты
{
    public partial class EditKlientForm : Form
    {
        public EditKlientForm()
        {
            InitializeComponent();
        }

        
        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;
        public string[] arr1 = new string[4];
        public string id_pers;


        private void EditKlientForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.MinimumSize = new System.Drawing.Size(240, 270);
            this.MaximumSize = new System.Drawing.Size(240, 270);

            //for (int j = 0; j < 4; j++)
            //{
            //    MessageBox.Show(" ", arr1[j]);

            //}


            //textBox1.ReadOnly = true;

            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_ПерснДаннКлиента, ФИО, Номер_Паспорта, Дата_Рождения FROM ПерсональныеДанныеКлиентов WHERE ФИО='" + arr1[1] + "' AND Номер_Паспорта='" + arr1[2] +"' AND Дата_Рождения='" + arr1[3]  + "'";
            
          // command.CommandText = "SELECT ID_Пользователь, login, password, role, Фамилия, Имя, Отчество, ДатаРегистрации FROM users WHERE ID_Пользователь=" + id + "";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_pers = reader[0].ToString();
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
                textBox3.Text = reader[3].ToString();
            }
            connection.Close();
            textBox4.Text = arr1[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") || textBox4.Text.Equals(""))
                { MessageBox.Show("Заполните все поля ввода", "Ошибка"); }
                else
                {
                    SqlConnection connection1 = new SqlConnection(connectString);
                    SqlCommand command1 = new SqlCommand();
                    command1.Connection = connection1;
                    command1.CommandText = @"UPDATE ПерсональныеДанныеКлиентов SET ФИО='" + textBox1.Text + "', Номер_Паспорта='" + textBox2.Text + "', Дата_Рождения='" + textBox3.Text + "'  WHERE id_ПерснДаннКлиента=" + id_pers + "";
                    connection1.Open();
                    command1.ExecuteNonQuery();
                    connection1.Close();

                    MessageBox.Show("Данные изменены!");

                    Class1 clas = new Class1();
                    clas.users_ychet("Изменение нового сотрудника");

                    KlientForm form = new KlientForm();
                    this.Hide();
                    form.Show();
                }
            }
            catch { MessageBox.Show("Ошибка"); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            KlientForm form = new KlientForm();
            this.Hide();
            form.Show();

        }

        private void EditKlientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            KlientForm form = new KlientForm();
            this.Hide();
            form.Show();
        }
    }
}
