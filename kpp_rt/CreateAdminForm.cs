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


        }


    }
}
