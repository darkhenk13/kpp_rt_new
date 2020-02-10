using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kpp_rt
{
    public partial class EditAdminForm : Form
    {
        public EditAdminForm()
        {
            InitializeComponent();
        }
        public string[] arr_del1 = new string[4];
        public string id_users;
        public string password_;
        public string id_sotrud;
        public string id_prav;
        private void EditAdminForm_Load(object sender, EventArgs e)
        {

            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.MinimumSize = new System.Drawing.Size(230, 320);
            this.MaximumSize = new System.Drawing.Size(230, 320);

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            sotrudnik_poisk(arr_del1[1], arr_del1[2]); // получение ID сотрудника


            users();
            textBox1.Enabled = false;
            textBox1.Text = arr_del1[1];
            textBox2.Text = arr_del1[0];
            textBox3.Text = password_;
            combobox_();

            if (2 == Convert.ToInt32(id_prav))
            {
                comboBox1.SelectedIndex = 1;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();

        }

        private void EditAdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();

        }

        // получение ID СОТРУДНИКА
        private void sotrudnik_poisk(string FIO, string date)
        {
            string id_pers = "";
            // персональные данные клиента
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_ПерснСотрудника, ФИО, Дата_Рождения FROM ПерссональныеДанныеСотрудника WHERE ФИО='" + FIO + "' AND Дата_Рождения='" + date + "'";
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id_pers = reader[0].ToString();
                string f = reader[1].ToString();
            }
            connection.Close();


            command.Connection = connection;
            command.CommandText = "SELECT ID_Сотрудника FROM Сотрудники WHERE ID_ПерснСотрудника=" + id_pers + "";
            connection.Open();

            SqlDataReader reader1 = command.ExecuteReader();
            while (reader1.Read())
            {
                id_sotrud = reader1[0].ToString();             
            }
            connection.Close();
        }

        private void users()
        {

            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();


            connection.Close();

            command.Connection = connection;
            command.CommandText = "SELECT ID_Users, password, ID_ПравДоступа FROM ПользователиПрограммы WHERE ID_Сотруднка='" + id_sotrud + "' AND login='" + arr_del1[0] + "'";
            connection.Open();

            SqlDataReader reader1 = command.ExecuteReader();
            while (reader1.Read())
            {
                id_users = reader1[0].ToString();
                password_ = reader1[1].ToString();
                id_prav = reader1[2].ToString();
            }
            connection.Close();         
        }

        void combobox_()
        {
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            //command.CommandText = "SELECT Права_Доступа FROM ПраваДоступа";
            command.CommandText = @"SELECT CASE WHEN Права_Доступа='1' THEN 'Администратор' ELSE 'Оператор' END AS [Права доступа] FROM ПраваДоступа";
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text.Equals("") || textBox3.Text.Equals(""))
                {
                    MessageBox.Show("Не все поля заполнены", "Ошибка");  
                }
                else {
                    string id_prav_dost = "";
                    int p = 0;
                    SqlConnection connection1 = new SqlConnection(Form1.connectString);
                    SqlCommand command1 = new SqlCommand();

                    if (comboBox1.Text != "Администратор")
                    { p = 2; }
                    else
                    { p = 1; }

                    //Тип доступа
                    command1.Connection = connection1;
                    command1.CommandText = "SELECT ID_ПравДоступа, Права_Доступа FROM ПраваДоступа WHERE Права_Доступа='" + p + "'";
                    connection1.Open();
                    SqlDataReader reader1 = command1.ExecuteReader();
                    while (reader1.Read())
                    {
                        id_prav_dost = reader1[0].ToString();
                        string name = reader1[1].ToString();
                    }
                    connection1.Close();
                    // конец Типа доступа

                    // изменение пользователя
                    command1.Connection = connection1;
                    connection1.Open();
                    command1.CommandText = @"UPDATE ПользователиПрограммы SET login='" + textBox2.Text + "', password='" + textBox3.Text + "', ID_ПравДоступа='" + id_prav_dost + "'  WHERE ID_Users=" + id_users + "";

                    command1.ExecuteNonQuery();
                    connection1.Close();
                    // конец изменения пользователя

                    MessageBox.Show("Изменение пользователя программы", "Запись Изменена");
                    AdminForm form = new AdminForm();
                    this.Hide();
                    form.Show();

                }
            }
            catch { MessageBox.Show("Ошибка", "Ошибка"); }
        }
    }
}

