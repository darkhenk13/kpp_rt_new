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

namespace kpp_rt.Карта
{
    public partial class EditCardForm : Form
    {
        public EditCardForm()
        {
            InitializeComponent();
        }

        public string[] arr1 = new string[8];
        public string[] sotrud_arr = new string[4];
        public int k;
        
      


        private void button1_Click(object sender, EventArgs e)
        {
            CartForm form = new CartForm();
            this.Hide();
            form.Show();
        }

        private void EditCardForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;

            if (k == 1)
            {
               
                textBox1.Text = sotrud_arr[1];
                
            }
            else
            {
                textBox1.Text = arr1[1];
            }
            
            textBox2.Text = arr1[0];

           
        }

       


        private void EditCardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CartForm form = new CartForm();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int f = 1;
            SotrudCardForm form = new SotrudCardForm();
            form.fm = f;
            form.new_sotrud = sotrud_arr;
            form.arr = arr1;
            form.k1 = k;
            this.Hide();
            form.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
                { MessageBox.Show("Заполните все поля ввода", "Ошибка"); }
                else
                {
                    //
                    sotrud_id_();
                   

                   
                  


                    //

                    MessageBox.Show("Данные изменены!");

                    Class1 clas = new Class1();
                    clas.users_ychet("Изменение Карты");

                    CartForm form = new CartForm();
                    this.Hide();
                    form.Show();
                }
            //}
            //catch { MessageBox.Show("Ошибка"); }
        }


        private void sotrud_id_()
        {
            string id_ = "";
            string id_ps = "";
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_ПерснСотрудника FROM ПерссональныеДанныеСотрудника WHERE ФИО='" + sotrud_arr[1] + "' AND Номер_телефона='" + sotrud_arr[2] + "' AND Дата_Рождения='" + sotrud_arr[3] + "'";
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_ps = reader[0].ToString();
                
            }
            connection.Close();
            // поииск ID СОТРУДНИКА

            command.Connection = connection;
            command.CommandText = "SELECT ID_Сотрудника FROM Сотрудники WHERE ID_ПерснСотрудника='" + id_ps + "'";
            connection.Open();

            SqlDataReader reader1 = command.ExecuteReader();

            while (reader1.Read())
            {
                id_= reader1[0].ToString();
                
            }
            connection.Close();
            cart_(id_);
        }


        private void cart_(string id)
        {
            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection1;
            command1.CommandText = @"UPDATE Карта SET ID_Сотрудника='" + id + "'  WHERE ID_Карты=" + arr1[0] + "";
            connection1.Open();
            command1.ExecuteNonQuery();
            connection1.Close();
        }

    }
}
