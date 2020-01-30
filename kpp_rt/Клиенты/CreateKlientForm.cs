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

namespace kpp_rt.Клиенты
{
    public partial class CreateKlientForm : Form
    {
        public CreateKlientForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;
        private void button2_Click(object sender, EventArgs e)
        {
            // таблица клиенты
            int id_clienta;
            int id_persndan;
            int datereg;

            //таблица персданныеклиента
            int id_pers;
            string fio;
            string number;
            string date;

            // добавление в таблицуу персн данные клиента
            SqlConnection conn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            conn.Open(); //Устанавливаем соединение с базой данных.
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO [ПерсональныеДанныеКлиентов] (ФИО,
                                Номер_Паспорта, Дата_Рождения)
                                        values (@ФИО,
                                @Номер_Паспорта, @Дата_Рождения)";


            cmd.Parameters.Add("@ФИО", SqlDbType.NVarChar);
            cmd.Parameters["@ФИО"].Value = textBox1.Text;

            cmd.Parameters.Add("@Номер_Паспорта", SqlDbType.NVarChar);
            cmd.Parameters["@Номер_Паспорта"].Value = textBox2.Text;

            cmd.Parameters.Add("@Дата_Рождения", SqlDbType.NVarChar);
            cmd.Parameters["@Дата_Рождения"].Value = textBox3.Text;

            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT @@IDENTITY";
            int lastId = Convert.ToInt32(cmd.ExecuteScalar());
           
            //MessageBox.Show("Новый клиент в таблицу Клиенты добавлен", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            // Close();
            conn.Close();
            // Добавление в таблицу персн данные клиента



            // Добавление в таблицу клиентов

            string _date = DateTime.Now.ToString("dd MMMM yyyy");
            conn.Open(); //Устанавливаем соединение с базой данных.
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO [Клиенты] (ID_ПерснДанныеКлиента,
                                Дата_Регистрации_Клиента)
                                        values (@ID_ПерснДанныеКлиента, @Дата_Регистрации_Клиента)";


            cmd.Parameters.Add("@ID_ПерснДанныеКлиента", SqlDbType.NVarChar);
            cmd.Parameters["@ID_ПерснДанныеКлиента"].Value = lastId;

            cmd.Parameters.Add("@Дата_Регистрации_Клиента", SqlDbType.NVarChar);
            cmd.Parameters["@Дата_Регистрации_Клиента"].Value = _date;

          


            cmd.ExecuteNonQuery();
            MessageBox.Show("Новый клиент добавлен", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            // Close();
            conn.Close();

            Class1 clas = new Class1();
            clas.users_ychet("Добавлене нового клиента");

            KlientForm form = new KlientForm();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KlientForm form = new KlientForm();
            this.Hide();
            form.Show();
        }

        private void CreateKlientForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void CreateKlientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            KlientForm form = new KlientForm();
            this.Hide();
            form.Show();
        }
    }
}
