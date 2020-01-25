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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       public static string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;

        private void button1_Click(object sender, EventArgs e)
        {

            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();

            /*
            //Подключение к бд
            SqlConnection conn = new SqlConnection(connectString);
            SqlCommand comm = new SqlCommand();
            // конец подключения к бд

            // пременные для авторизации
            string _id_users = "";
            string _login = "";
            string _password = "";
            string _role = "";

            // sql запрос для получения данных
            conn.Open();
            comm.Connection = conn;
            comm.CommandText = @"SELECT ПользователиПрограммы.ID_Users, ПользователиПрограммы.login, ПользователиПрограммы.password, ПраваДоступа.Права_Доступа 
FROM ПользователиПрограммы INNER JOIN ПраваДоступа ON ПользователиПрограммы.ID_ПравДоступа = ПраваДоступа.ID_ПравДоступа
WHERE ПользователиПрограммы.login = '" + textBox1.Text + "' AND ПользователиПрограммы.password ='" + textBox2.Text + "'";


            SqlDataReader reader = comm.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    _id_users = reader[0].ToString();
                    _login = reader[1].ToString();
                    _password = reader[2].ToString();
                    _role = reader[3].ToString();
                }
            else {
                MessageBox.Show("Неверныый логин или пароль");
            }

            conn.Close();


            // проверка прав пользователя
            switch (_role)
            {
                case "0":

                    break;

                case "1": // Администратор
                    AdminForm admin = new AdminForm();                 
                    this.Hide();
                    admin.Show();
                    break;

                case "2": // Оператор
                    
                    break;
            }
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewSotrudForm form = new NewSotrudForm();
            this.Hide();
            form.Show();
        }
    }
}
