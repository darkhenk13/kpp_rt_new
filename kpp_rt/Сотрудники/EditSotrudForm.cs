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

namespace kpp_rt.Сотрудники
{
    public partial class EditSotrudForm : Form
    {
        public EditSotrudForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;
        public string[] arr1 = new string[6];
        public string id_pers;
        public string id_sotr;
        public string date;
        public string id_otdel;
        public string id_dolz;
        public string comb;
        public string comb1;

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") || textBox4.Text.Equals("") || comboBox1.Text.Equals("") || comboBox2.Text.Equals(""))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка");
                }
                else
                {
                    string new_otdel = "";
                    string new_dolz = "";
                    SqlConnection connection1 = new SqlConnection(connectString);
                    SqlCommand command1 = new SqlCommand();

                    // Отдел
                    command1.Connection = connection1;
                    command1.CommandText = "SELECT ID_Отдела, Отдел FROM Отделы WHERE  Отдел='" + comboBox2.Text + "'";
                    connection1.Open();
                    SqlDataReader reader1 = command1.ExecuteReader();
                    while (reader1.Read())
                    {
                        new_otdel = reader1[0].ToString();
                        string t = reader1[1].ToString();
                    }
                    connection1.Close();
                    // Отдел
                    //Должность
                    command1.Connection = connection1;
                    command1.CommandText = "SELECT ID_Должность, Должность FROM Должность WHERE  Должность='" + comboBox1.Text + "'";
                    connection1.Open();
                    SqlDataReader reader2 = command1.ExecuteReader();
                    while (reader2.Read())
                    {
                        new_dolz = reader2[0].ToString();
                        string t1 = reader2[1].ToString();
                    }
                    connection1.Close();
                    //Должность




                    command1.Connection = connection1;
                    command1.CommandText = @"UPDATE ПерссональныеДанныеСотрудника SET ФИО='" + textBox1.Text + "', Номер_телефона='" + textBox2.Text + "', Дата_Рождения='" + textBox3.Text + "'  WHERE ID_ПерснСотрудника=" + id_pers + "";
                    connection1.Open();
                    command1.ExecuteNonQuery();
                    connection1.Close();




                    command1.Connection = connection1;
                    command1.CommandText = @"UPDATE Сотрудники SET ID_Должность='" + new_dolz + "', ID_Отдела='" + new_otdel + "'  WHERE ID_Сотрудника=" + id_sotr + "";
                    connection1.Open();
                    command1.ExecuteNonQuery();
                    connection1.Close();





                    MessageBox.Show("Данные изменены");

                    Class1 clas = new Class1();
                    clas.users_ychet("Изменение данных");

                    SotrudForm form = new SotrudForm();
                    this.Hide();
                    form.Show();
                }
            }
            catch { MessageBox.Show("", "Ошибка"); }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();
        }

        private void EditSotrudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();
        }

        private void EditSotrudForm_Load(object sender, EventArgs e)
        {
            textBox4.Enabled = false;
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            

            
            // Загрузка данных персональные данные сотрудника
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_ПерснСотрудника, ФИО, Номер_телефона, Дата_Рождения FROM ПерссональныеДанныеСотрудника WHERE ФИО='" + arr1[1] + "' AND Номер_телефона='" + arr1[2] + "' AND Дата_Рождения='" + arr1[3] + "'";

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

            // Конец загрузки данных персональные данные сотрудника

            // сотрудники
            command.Connection = connection;
            command.CommandText = "SELECT ID_Сотрудника, ID_Отдела, ID_Должность, ID_ПерснСотрудника, Дата_Регистрации_Сотрудника FROM Сотрудники WHERE ID_ПерснСотрудника='" + id_pers + "' AND Дата_Регистрации_Сотрудника='" + arr1[0] + "'";

            // command.CommandText = "SELECT ID_Пользователь, login, password, role, Фамилия, Имя, Отчество, ДатаРегистрации FROM users WHERE ID_Пользователь=" + id + "";

            connection.Open();

            SqlDataReader reader1 = command.ExecuteReader();

            while (reader1.Read())
            {
                id_sotr = reader1[0].ToString();
                id_otdel = reader1[1].ToString();
                id_dolz = reader1[2].ToString();
                string id = reader1[3].ToString();
                textBox4.Text = reader1[4].ToString();
               
            }
            connection.Close();
            // сотрудники


           
            // Отдел
            command.Connection = connection;
            command.CommandText = "SELECT ID_Отдела, Отдел FROM Отделы WHERE ID_Отдела='" + id_otdel + "'";
            connection.Open();

            SqlDataReader reader2 = command.ExecuteReader();

            while (reader2.Read())
            {
                string d = reader2[0].ToString();
                //comboBox1.Text = reader2[1].ToString();
                comb = reader2[1].ToString();
            }
            connection.Close();
            // Отдел

            // Должность
            command.Connection = connection;
            command.CommandText = "SELECT ID_Должность, Должность FROM Должность WHERE ID_Должность='" + id_dolz + "'";
            connection.Open();

            SqlDataReader reader3 = command.ExecuteReader();

            while (reader3.Read())
            {
                string d1 = reader3[0].ToString();
                comb1 = reader3[1].ToString();
            }
            connection.Close();
            // Должность
            otdel_items();
            dolzs();

            // Отдел
            for (int i = 0; i < comboBox2.Items.Count; i++)
            {
                if (comboBox2.Items[i].ToString() == comb)
                {
                    comboBox2.SelectedIndex = i;
                }
                else {  } 
            }
            // Отдел
            //Должность
            for (int j = 0; j < comboBox1.Items.Count; j++)
            { 
                if (comboBox1.Items[j].ToString() == comb1)
                {
                    comboBox1.SelectedIndex = j;
                }
                else { }         
            }
            // Должность
        }

        void otdel_items()
        {
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_Отдела, Отдел FROM Отделы";
           
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                id_pers = reader[0].ToString();
                comboBox2.Items.Add(reader[1].ToString());              
            }
            connection.Close();
        }

        void dolzs()
        {
           
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_Должность, Должность FROM Должность";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_pers = reader[0].ToString();
                comboBox1.Items.Add(reader[1].ToString());
            }
            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
