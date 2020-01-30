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

            Class1 clas = new Class1();

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

                    

                    Properties.Settings.Default.users = _login;
                    Properties.Settings.Default.id = _id_users;
                    Properties.Settings.Default.Save();

                    clas.users_ychet("Вход в программу");


                    AdminForm admin = new AdminForm();                 
                    this.Hide();
                    admin.Show();

                    
                    break;

                case "2": // Оператор



                    Properties.Settings.Default.users = _login;
                    Properties.Settings.Default.id = _id_users;
                    Properties.Settings.Default.Save();


                    clas.users_ychet("Вход в пограмму");

                    OperatorForm oper = new OperatorForm();
                    this.Hide();
                    oper.Show();

                    


                    break;
            }
           
        }



      



        private void Form1_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.users =  "";
            Properties.Settings.Default.id = "";
            Properties.Settings.Default.Save();

            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            this.MaximizeBox = false;
            this.MinimizeBox = false;





            // скрыть пароль
            textBox2.UseSystemPasswordChar = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);

            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
                Environment.Exit(0);
           
           
           
        }


       
       



    }
}
