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
        public int k;
        public string klient_old;
        public string id_object;
        public string id_yrdost;
        public string dopusk;
        public string n;
        public string sotrudnik_old;

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
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.MinimumSize = new System.Drawing.Size(280, 310);
            this.MaximumSize = new System.Drawing.Size(280, 310);

            //combobox_();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            if (arr1[6] != "")
            {
                // сотрудник
                label1.Text = "Сотрудник";
                textBox1.Text = arr1[6];
                textBox2.Text = arr1[2] + " " + arr1[3] + " " + arr1[4] + " " + arr1[5];
                sotrudniki();
                search_id_object();
                YrSotrud_sotr();

                comboBox2.Text = n;

                if (dopusk != "0")
                {
                    comboBox1.Text = "Да";
                }
                else { comboBox1.Text = "Нет"; }
            }
            else
            {
                



                // клиент
                label1.Text = "Клиент";
                textBox1.Text = arr1[7];
                klient();
                textBox2.Text = arr1[2] + " " + arr1[3] + " " + arr1[4] + " " + arr1[5];

                search_id_object();
                YrDostupa();

                comboBox2.Text = n;


                if (dopusk != "0")
                {
                    comboBox1.Text = "Да";
                }
                else { comboBox1.Text = "Нет"; }
            }


            


        }


        private void klient()
        {
            string id_pers = "";
            // персональные данные клиента
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_ПерснДаннКлиента, ФИО FROM ПерсональныеДанныеКлиентов WHERE ФИО='" + arr1[7] + "'";
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
            command.CommandText = "SELECT ID_Клиента, ID_ПерснДанныеКлиента FROM Клиенты WHERE ID_ПерснДанныеКлиента='" + id_pers + "'";
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

        // сотрудники
        private void sotrudniki()
        {
            string id_pers = "";
            string sotr = "";
            // персональные данные клиента
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_ПерснСотрудника, ФИО FROM ПерссональныеДанныеСотрудника WHERE ФИО='" + arr1[6] + "'";
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
            command.CommandText = "SELECT ID_Сотрудника, ID_ПерснСотрудника FROM Сотрудники WHERE ID_ПерснСотрудника='" + id_pers + "'";
            connection.Open();

            SqlDataReader reader1 = command.ExecuteReader();
            while (reader1.Read())
            {
                sotr = reader1[0].ToString();
                string f = reader1[1].ToString();
            }
            connection.Close();



            command.Connection = connection;
            command.CommandText = "SELECT ID_Карты FROM Карта WHERE ID_Сотрудника='" + sotr + "'";
            connection.Open();

            SqlDataReader reader3 = command.ExecuteReader();
            while (reader3.Read())
            {
                sotrudnik_old = reader3[0].ToString();
               
            }
            connection.Close();

            //конец ид клиента
        }



        // id объекта и этажей
        void search_id_object()
        {

            string city;
            string street;
            string zd = "";
            string n;
            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();

            command1.Connection = connection1;
            connection1.Open();
            command1.CommandText = "SELECT ID_Объекта, Этаж FROM Объект WHERE Город='" + arr1[2] + "' AND Улица='" + arr1[3] + "' AND Здание='" +arr1[4] + "'AND Этаж='" + arr1[5] + "'";

            SqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                id_object = reader1[0].ToString();
                zd = reader1[1].ToString();

            }
            connection1.Close();
            //MessageBox.Show("Объект", id_object);
            if (String.IsNullOrEmpty(zd))
            { }
            else
            {
                for (int i = 1; i <= Convert.ToInt32(zd); i++)
                {
                    comboBox2.Items.Add(i);

                }
            }

        }



        // уровень доступа ID
        private void YrDostupa()
        {
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_УрДоступа, Допуск, Этаж FROM УровеньДоступа WHERE ID_Объекта='" 
                + id_object + "' AND ID_Клиента='" + klient_old + "'";
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id_yrdost = reader[0].ToString();
                dopusk = reader[1].ToString();
                n = reader[2].ToString();
            }
            connection.Close();
        }


        // уровень доступа сотрудника
        private void YrSotrud_sotr()
        {
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_УрДоступа, Допуск, Этаж FROM УровеньДоступа WHERE ID_Объекта='"
                + id_object + "' AND ID_Карты='" + sotrudnik_old + "'";
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id_yrdost = reader[0].ToString();
                dopusk = reader[1].ToString();
                n = reader[2].ToString();
            }
            connection.Close();
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
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();
            string da = "";
            if (arr1[6] != "")
            {
                if (comboBox1.Text != "Да")
                { da = "0"; }
                else { da = "1"; }
                // сотрудник
                
                command1.Connection = connection1;
                command1.CommandText = @"UPDATE УровеньДоступа SET ID_Объекта='" + 
                    id_object + "', ID_Карты='" + sotrudnik_old + "', Допуск='"
                    + da + "', Этаж='" + comboBox2.Text + "'  WHERE ID_УрДоступа='" + id_yrdost + "'";
                connection1.Open();
                command1.ExecuteNonQuery();
                connection1.Close();

                MessageBox.Show("Данные изменены!");

                Class1 clas = new Class1();
                clas.users_ychet("Изменение уровня доступа");

                YrDostupForm form = new YrDostupForm();
                this.Hide();
                form.Show();
              
            }
            else {

                if (comboBox1.Text != "Да")
                { da = "0"; }
                else { da = "1"; }
                // сотрудник
               
                command1.Connection = connection1;
                command1.CommandText = @"UPDATE УровеньДоступа SET ID_Объекта='" +
                    id_object + "', ID_Клиента='" + klient_old + "', Допуск='"
                    + da + "', Этаж='" + comboBox2.Text + "'  WHERE ID_УрДоступа='" + id_yrdost + "'";
                connection1.Open();
                command1.ExecuteNonQuery();
                connection1.Close();

                MessageBox.Show("Данные изменены!");

                Class1 clas = new Class1();
                clas.users_ychet("Изменение уровня доступа");

                YrDostupForm form = new YrDostupForm();
                this.Hide();
                form.Show();


            }


            }

            private void button4_Click(object sender, EventArgs e)
        {
          

        }
    }
}
