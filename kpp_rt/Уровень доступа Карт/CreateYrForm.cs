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

namespace kpp_rt.Уровень_доступа_Карт
{
    public partial class CreateYrForm : Form
    {
        public CreateYrForm()
        {
            InitializeComponent();
        }
        public int status_radio;
        public string id_object;
        public string id_clienta;
        public string id_sotrudnika;
        public string id_card;
        public string[] client = new string[4];
        public string[] sotrudnik = new string[6];
        public string[] object_table = new string[4];


        private void CreateYrForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);


            status_form();
            search_id_object();
            //Город, Улица, Здание, Этаж 


            textBox3.Text = object_table[0] + " " + object_table[1] + " " + object_table[2] + " " + object_table[3];
        }

        // получить ID объекта и этажей
        void search_id_object()
        {
            try
            {
                string city;
                string street;
                string zd;
                string n;
                SqlConnection connection1 = new SqlConnection(Form1.connectString);
                SqlCommand command1 = new SqlCommand();

                command1.Connection = connection1;
                connection1.Open();
                command1.CommandText = "SELECT ID_Объекта, Город, Улица, Здание, Этаж FROM Объект WHERE Город='" + object_table[0] + "' AND Улица='" + object_table[1] + "' AND Здание='" + object_table[2] + "'AND Этаж='" + object_table[3] + "'";

                SqlDataReader reader1 = command1.ExecuteReader();

                while (reader1.Read())
                {
                    id_object = reader1[0].ToString();
                   city = reader1[1].ToString();
                   street = reader1[2].ToString();
                   zd = reader1[3].ToString();

                }
                connection1.Close();
                //MessageBox.Show("Объект", id_object);
                if (String.IsNullOrEmpty(object_table[3]))
                { }
                else
                {
                    for (int i = 1; i <= Convert.ToInt32(object_table[3]); i++)
                    {
                        comboBox2.Items.Add(i);
                    }
                }
            }
            catch { }
        }

        public void status_form()
        {
            if (status_radio != 1)
            {
                //клиенты
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                textBox1.Text = client[1];
            }
            else
            {
                //Сотрудники
                radioButton2.Checked = true;
                radioButton1.Checked = false;
                textBox1.Text = sotrudnik[1];
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked != true)
            {
                status_radio = 1;
                // Сотрудники
                CreateYtSotrudForm form = new CreateYtSotrudForm();
                form.status_radio1 = status_radio;
                form.sotrudnik1 = sotrudnik;
                form.object_table1 = object_table;


                this.Hide();
                form.Show();


            }
            else if (radioButton2.Checked != true)
            {
                status_radio = 2;
                //Клиенты
                CreateYtKlientForm form = new CreateYtKlientForm();
                form.status_radio1 = status_radio;
                form.client1 = client;
                form.object_table1 = object_table;
                this.Hide();
                form.Show();


            }
        }

        private void CreateYrForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Клиент";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Сотрудники";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateYrObjectForm form = new CreateYrObjectForm();
            form.status_radio1 = status_radio;
            form.object_table1 = object_table;
            form.sotrudnik1 = sotrudnik;
            form.client1 = client;
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int dopusk;
            if (comboBox1.Text != "Да")
            {
                dopusk = 0;
            }
            else
            {
                dopusk = 1;
            }


            if (radioButton1.Checked == true)
            {

                id_client_search();
                YrDostupForm form = new YrDostupForm();
                SqlConnection conn = new SqlConnection(Form1.connectString);
                SqlCommand cmd = new SqlCommand();
                conn.Open(); //Устанавливаем соединение с базой данных.
                cmd.Connection = conn;
                cmd.CommandText = @"INSERT INTO [УровеньДоступа] (ID_Объекта, 
                                 ID_Клиента, Допуск, Этаж)
                                        values (@ID_Объекта, 
                                 @ID_Клиента, @Допуск, @Этаж)";


                cmd.Parameters.Add("@ID_Объекта", SqlDbType.Int);
                cmd.Parameters["@ID_Объекта"].Value = id_object;

                cmd.Parameters.Add("@ID_Клиента", SqlDbType.Int);
                cmd.Parameters["@ID_Клиента"].Value = id_clienta;

                cmd.Parameters.Add("@Допуск", SqlDbType.Int);
                cmd.Parameters["@Допуск"].Value = dopusk;

                cmd.Parameters.Add("@Этаж", SqlDbType.NVarChar);
                cmd.Parameters["@Этаж"].Value = comboBox2.Text;




                cmd.ExecuteNonQuery();
                MessageBox.Show("Новая карта создана", "Добавление карты", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                // Close();
                conn.Close();

                Class1 clas = new Class1();
                clas.users_ychet("Добавлене");

                this.Hide();
                form.Show();
            }
            else
            {

                id_sotrudnik_search();


                YrDostupForm form = new YrDostupForm();
                SqlConnection conn = new SqlConnection(Form1.connectString);
                SqlCommand cmd = new SqlCommand();
                conn.Open(); //Устанавливаем соединение с базой данных.
                cmd.Connection = conn;
                cmd.CommandText = @"INSERT INTO [УровеньДоступа] (ID_Объекта, 
                                 ID_Карты, Допуск, Этаж)
                                        values (@ID_Объекта, 
                                 @ID_Карты, @Допуск, @Этаж)";


                cmd.Parameters.Add("@ID_Объекта", SqlDbType.Int);
                cmd.Parameters["@ID_Объекта"].Value = id_object;

                cmd.Parameters.Add("@ID_Карты", SqlDbType.Int);
                cmd.Parameters["@ID_Карты"].Value = id_card;

                cmd.Parameters.Add("@Допуск", SqlDbType.Int);
                cmd.Parameters["@Допуск"].Value = dopusk;

                cmd.Parameters.Add("@Этаж", SqlDbType.NVarChar);
                cmd.Parameters["@Этаж"].Value = comboBox2.Text;




                cmd.ExecuteNonQuery();
                MessageBox.Show("Новая карта создана", "Добавление карты", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                // Close();
                conn.Close();

                Class1 clas = new Class1();
                clas.users_ychet("Добавлене");


                this.Hide();
                form.Show();
            }
            

        }

        //Поиск Клиента
        void id_client_search()
        {
            string id_pers = "";
            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();

            command1.Connection = connection1;
            connection1.Open();
            command1.CommandText = "SELECT ID_ПерснДаннКлиента, ФИО, Номер_Паспорта, Дата_Рождения FROM ПерсональныеДанныеКлиентов WHERE ФИО='" + client[1] + "' AND Номер_Паспорта='" + client[2] + "' AND Дата_Рождения='" + client[3] + "'";

            SqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                id_pers = reader1[0].ToString();
                string n1 = reader1[1].ToString();
                string n2 = reader1[2].ToString();
                string n3 = reader1[3].ToString();

            }
            connection1.Close();

            command1.Connection = connection1;
            connection1.Open();
            command1.CommandText = "SELECT ID_Клиента, ID_ПерснДанныеКлиента FROM Клиенты WHERE ID_ПерснДанныеКлиента='" + id_pers + "'";

            SqlDataReader reader2 = command1.ExecuteReader();

            while (reader2.Read())
            {
                id_clienta = reader2[0].ToString();
                string n1 = reader2[1].ToString();
                

            }
            connection1.Close();

            

        }

        // Поиск Сотрудника
        void id_sotrudnik_search()
        {
            string id_persSot = "";
            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();

            command1.Connection = connection1;
            connection1.Open();
            command1.CommandText = "SELECT ID_ПерснСотрудника, ФИО, Номер_телефона, Дата_Рождения FROM ПерссональныеДанныеСотрудника WHERE ФИО='" + sotrudnik[1] + "' AND Номер_телефона='" + sotrudnik[2] + "' AND Дата_Рождения='" + sotrudnik[3] + "'";

            SqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                id_persSot = reader1[0].ToString();
                string n1 = reader1[1].ToString();
                string n2 = reader1[2].ToString();
                string n3 = reader1[3].ToString();

            }
            connection1.Close();



            command1.Connection = connection1;
            connection1.Open();
            command1.CommandText = "SELECT ID_Сотрудника, ID_ПерснСотрудника FROM Сотрудники WHERE ID_ПерснСотрудника='" + id_persSot + "'";

            SqlDataReader reader2 = command1.ExecuteReader();

            while (reader2.Read())
            {
               id_sotrudnika = reader2[0].ToString();
                string n1 = reader2[1].ToString();


            }
            connection1.Close();


          


            command1.Connection = connection1;
            connection1.Open();
            command1.CommandText = "SELECT ID_Карты, ID_Сотрудника FROM Карта WHERE ID_Сотрудника='" + id_sotrudnika + "'";

            SqlDataReader reader3 = command1.ExecuteReader();

            while (reader3.Read())
            {
                id_card = reader3[0].ToString();
                string n1 = reader3[1].ToString();

            }
            connection1.Close();

        }
           

    }
}
