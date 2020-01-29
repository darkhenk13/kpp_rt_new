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
using System.Configuration;

namespace kpp_rt.Карта
{
    public partial class CreateCardForm : Form
    {
        public CreateCardForm()
        {
            InitializeComponent();
        }
        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;

        public string[] new_client1 = new string[4];
        public string[] new_sotrud1 = new string[4];
        public string[] new_object1 = new string[4];
        public bool check;
        public string id_sotr;
        public string id_pers;
      
        // public string[] new_client = new string[4];

        private void CreateCardForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            // объект
            //SqlDataAdapter da = new SqlDataAdapter("SELECT  FROM TBItemList", connectString);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //comboBox1.DataSource = dt;
            //comboBox1.DisplayMember = "ItemName";
            //comboBox1.ValueMember = "ItemName";
            // конец объект
            // уровень доступа

            // конец уровень доступа



            //


            textBox2.Text = new_sotrud1[1];
          
                //textBox2.Text = new_client1[1];
          

         
           
            //textBox2.Text = new_sotrud1[1];

            //for (int j = 0; j < 4; j++)
            //{
            //    MessageBox.Show(" ", new_client1[j]);

            //}


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "Выбрать сотрудника";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "Выбрать клиента";
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
                // сотрудники 
                start_sotrud();
           

                //// клиенты
                //start();
                //resa();
            
            
        }
        void start_sotrud()
        {
            check = false;
            SotrudCardForm sotr = new SotrudCardForm();
            sotr.check1 = check;
            this.Hide();
            sotr.Show();
        }


        void start()
        {
            check = true;
            KlientCardForm kl = new KlientCardForm();
            kl.check1 = check;
            this.Hide();
            kl.Show();
        }
        void resa()
            {
            if (String.IsNullOrWhiteSpace(new_client1[1]))
            {
                textBox2.Text = new_client1[1];

            }
            else
            {
                textBox2.Text = new_client1[1];
            }
            
        }

        private void CreateCardForm_Activated(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //for (int j = 0; j < 4; j++)
            //{
            //    MessageBox.Show(" ", new_client1[j]);

            //}
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
                // клиенты
                ObjectCreateCardForm form = new ObjectCreateCardForm();
                form.new_client = new_client1;
                form.check1 = check;
                this.Hide();
                form.Show();
            
        }

        /*CREATE TABLE Карта(
	ID_Карты int,
	ID_Сотрудника int NULL,
	ID_УрДоступа int NULL,
	ID_ЗаблКарта int NULL,
	PRIMARY KEY (ID_Карты)
);*/

        private void button1_Click(object sender, EventArgs e)
        {
            string id;
            int i = 0;
            while (i < 10)
            {


                Random rand = new Random();
                int size = rand.Next(1000000);
                SqlConnection connection = new SqlConnection(connectString);
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                connection.Open();
                command.CommandText = "SELECT ID_Карты FROM Карта WHERE ID_Карты ='" + size + "'";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader[0].ToString();
                    }
                    connection.Close();
                }
                else
                {
                    i++;
                }
                textBox1.Text = Convert.ToString(size);
                i = 10;
            }
           

            
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            serch_pers();
            serch_sotrud();


            SqlConnection conn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            conn.Open(); //Устанавливаем соединение с базой данных.
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO [Карта] (ID_Карты, 
		                                ID_Сотрудника)
                                        values (@ID_Карты, 
		                                @ID_Сотрудника)";


            cmd.Parameters.Add("@ID_Карты", SqlDbType.Int);
            cmd.Parameters["@ID_Карты"].Value = textBox1.Text;

            cmd.Parameters.Add("@ID_Сотрудника", SqlDbType.NVarChar);
            cmd.Parameters["@ID_Сотрудника"].Value = id_sotr;




            cmd.ExecuteNonQuery();
            MessageBox.Show("Новая карта создана", "Добавление карты", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            // Close();
            conn.Close();

            CartForm form = new CartForm();
            this.Hide();
            form.Show();


        }

        void serch_pers()
        {
            string fio;
            string phone;
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT ID_ПерснСотрудника, ФИО, Номер_телефона FROM ПерссональныеДанныеСотрудника WHERE ФИО='" + new_sotrud1[1] + "' AND Номер_телефона='" + new_sotrud1[2] +"'";
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

            SqlConnection connection = new SqlConnection(connectString);
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

        private void CreateCardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CartForm form = new CartForm();
            this.Hide();
            form.Show();
        }
    }
}
