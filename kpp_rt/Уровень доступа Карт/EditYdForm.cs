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

namespace kpp_rt.Уровень_доступа_Карт
{
    public partial class EditYdForm : Form
    {
        public EditYdForm()
        {
            InitializeComponent();
        }
        public string[] arr1 = new string[8];
        public string[] arr_kliient = new string[8];
        public int k;
        public string klient_old;

        private void button1_Click(object sender, EventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();

        }

        private void EditYdForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();
        }

        private void EditYdForm_Load(object sender, EventArgs e)
        {
            //combobox_();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            if (arr1[6] == "")
            {
                // клиент
                
                if (k == 1)
                {
                    label1.Text = "Клиент";   
                    textBox1.Text = arr_kliient[1];
                }
                else
                {
                    label1.Text = "Клиент";
                    textBox1.Text = arr1[7];

                }
                
            }
            else
            {            
                // сотрудник
                label1.Text = "Сотрудник";
                textBox1.Text = arr1[6];
            }
            textBox2.Text = arr1[2] + " " + arr1[3] + " " + arr1[4] + " " + arr1[5] + " " + arr1[6];


        }


        // 
        //
        private void itazh()
        {
            //string city;
            //string street;
            //string zd;
            //string n;
            //SqlConnection connection1 = new SqlConnection(Form1.connectString);
            //SqlCommand command1 = new SqlCommand();

            //command1.Connection = connection1;
            //connection1.Open();
            //command1.CommandText = "SELECT ID_Объекта, Этаж";
             
            //SqlDataReader reader1 = command1.ExecuteReader();

            //while (reader1.Read())
            //{
            //    id_object = reader1[0].ToString();
            //    city = reader1[1].ToString();
            //    street = reader1[2].ToString();
            //    zd = reader1[3].ToString();

            //}
            //connection1.Close();
            ////MessageBox.Show("Объект", id_object);
            //if (String.IsNullOrEmpty(object_table[3]))
            //{ }
            //else
            //{
            //    for (int i = 1; i <= Convert.ToInt32(object_table[3]); i++)
            //    {
            //        comboBox2.Items.Add(i);
            //    }
            //}
        }


        private void klient(string FIO)
        {
            string id_pers = "";
            // персональные данные клиента
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_ПерснДаннКлиента, ФИО FROM ПерсональныеДанныеКлиентов WHERE ФИО=" + FIO + "";
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id_pers = reader[0].ToString();
                string f = reader[1].ToString();            
            }
            connection.Close();
            // конец поиска персональных данных клиента

            //ид клиента
            command.Connection = connection;
            command.CommandText = "SELECT ID_Клиента, ID_ПерснДанныеКлиента FROM Клиенты WHERE ID_ПерснДанныеКлиента=" + id_pers + "";
            connection.Open();

            SqlDataReader reader1 = command.ExecuteReader();
            while (reader1.Read())
            {
                klient_old = reader1[0].ToString();
                string f = reader1[1].ToString();
            }
            connection.Close();
            //конец ид клиента
        }

        // уровень доступа ID
        private void YrDostupa()
        {
            //SqlConnection connection = new SqlConnection(Form1.connectString);
            //SqlCommand command = new SqlCommand();

            //command.Connection = connection;
            //command.CommandText = "SELECTID_УрДоступа, Допуск, Этаж FROM УровеньДоступа WHERE ФИО=" + FIO + "";
            //connection.Open();

            //SqlDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    id_pers = reader[0].ToString();
            //    string f = reader[1].ToString();
            //}
            //connection.Close();

        }

        void combobox_()
        {
            //SqlConnection connection = new SqlConnection(Form1.connectString);
            //SqlCommand command = new SqlCommand();

            //command.Connection = connection;
            //command.CommandText = @"SELECT Этаж FROM Объект WHERE ID_Объекта='" +  + "'";
            //connection.Open();

            //SqlDataReader reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    comboBox1.Items.Add(reader[0].ToString());
            //}
            //connection.Close();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            EditYdKlientForm form = new EditYdKlientForm();
            form.arr2 = arr1;
            
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditYdForm form = new EditYdForm();
            this.Hide();
            form.Show();
        }
    }
}
