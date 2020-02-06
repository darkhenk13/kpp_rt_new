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
    public partial class CreateAdminForm : Form
    {
        public CreateAdminForm()
        {
            InitializeComponent();
        }


        public string[] arr = new string[4];
        public string id_pers;
        public string id_sotr;
        public string id_prav;


        private void CreateAdminForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = arr[1];
        }

        private void CreateAdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateSotrudAdminForm form = new CreateSotrudAdminForm();
            this.Hide();
            form.arr1 = arr;
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();

        }


        void serch_pers()
        {
            string fio;
            string phone;
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT ID_ПерснСотрудника, ФИО, Номер_телефона FROM ПерссональныеДанныеСотрудника WHERE ФИО='" + arr[1] + "' AND Номер_телефона='" + arr[2] + "'";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_pers = reader[0].ToString();
                fio = reader[1].ToString();
                phone = reader[2].ToString();

            }
            connection.Close();
        }

        void serch_sotrud()
        {

            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT ID_Сотрудника, ID_ПерснСотрудника FROM Сотрудники WHERE ID_ПерснСотрудника='" + id_pers + "'";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_sotr = reader[0].ToString();
            }
            connection.Close();


            if (comboBox1.Text != "Администратор")
            { id_prav = "2"; }
            else
            { id_prav = "1"; }


        }

        
       

        private void button2_Click(object sender, EventArgs e)
        {
            string users_s = "";


            serch_pers();
            serch_sotrud();






            SqlConnection conn = new SqlConnection(Form1.connectString);
            SqlCommand cmd = new SqlCommand();


            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = "SELECT login FROM ПользователиПрограммы WHERE login='" + textBox2.Text + "'";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users_s = reader[0].ToString();
               

            }
            conn.Close();


            if (textBox2.Text != users_s)
            {



                conn.Open(); //Устанавливаем соединение с базой данных.
                cmd.Connection = conn;
                cmd.CommandText = @"INSERT INTO [ПользователиПрограммы] (login, password, 
		                                ID_Сотруднка, ID_ПравДоступа)
                                        values (@login, @password, 
		                                @ID_Сотруднка, @ID_ПравДоступа)";


                cmd.Parameters.Add("@login", SqlDbType.NVarChar);
                cmd.Parameters["@login"].Value = textBox2.Text;

                cmd.Parameters.Add("@password", SqlDbType.NVarChar);
                cmd.Parameters["@password"].Value = textBox3.Text;

                cmd.Parameters.Add("@ID_Сотруднка", SqlDbType.Int);
                cmd.Parameters["@ID_Сотруднка"].Value = id_sotr;

                cmd.Parameters.Add("@ID_ПравДоступа", SqlDbType.Int);
                cmd.Parameters["@ID_ПравДоступа"].Value = id_prav;





                cmd.ExecuteNonQuery();
                MessageBox.Show("Новый пользователь добавлен", "Добавление пользователя", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                // Close();
                conn.Close();

                Class1 clas = new Class1();
                clas.users_ychet("Добавлене нового пользователя");

                AdminForm form = new AdminForm();
                this.Hide();
                form.Show();
            }
            else
            {
                MessageBox.Show("Такой логин уже существует", "Добавление пользователя", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
