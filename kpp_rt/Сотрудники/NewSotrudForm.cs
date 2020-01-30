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
    public partial class NewSotrudForm : Form
    {
        public NewSotrudForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;


       public void dolz_s(out int id_dolz)
        {
             id_dolz = 0;
            SqlConnection conn = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();

            command.CommandText = @"SELECT ID_Должность, Должность FROM Должность WHERE Должность='" + comboBox2.Text + "'";

            command.Connection = conn;
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                id_dolz = Convert.ToInt32(reader[0].ToString());
                string dolz1 = reader[1].ToString();
            }
            conn.Close();
        }

        public void otdel_s(out int id_otdel)
        {
            id_otdel = 0;
            SqlConnection conn = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();

            command.CommandText = @"SELECT ID_Отдела, Отдел FROM Отделы WHERE Отдел='" + comboBox1.Text + "'";

            command.Connection = conn;
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                id_otdel = Convert.ToInt32(reader[0].ToString());
                string dolz1 = reader[1].ToString();
            }
            conn.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int hack = 0;
            int id_otdel = 0;
            int id_dolz = 0;


            // Должность

            dolz_s(out id_dolz);



            // Должность конец
            otdel_s(out id_otdel);
            // Отдел

            SqlConnection conn = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();

            
            conn.Open();
            
            SqlTransaction tran = conn.BeginTransaction();
            
            //conn.Open(); //Устанавливаем соединение с базой данных.
            //cmd.Connection = conn;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Transaction = tran;

            cmd.CommandText = @"INSERT INTO [ПерссональныеДанныеСотрудника] (ФИО, Номер_телефона, Дата_Рождения) values (@ФИО, @Номер_телефона, @Дата_Рождения);SELECT SCOPE_IDENTITY();";
            
            cmd.Parameters.Add("@ФИО", SqlDbType.NVarChar);
            cmd.Parameters["@ФИО"].Value = textBox1.Text;

            cmd.Parameters.Add("@Номер_телефона", SqlDbType.NVarChar);
            cmd.Parameters["@Номер_телефона"].Value = maskedTextBox2.Text;

            cmd.Parameters.Add("@Дата_Рождения", SqlDbType.NVarChar);
            cmd.Parameters["@Дата_Рождения"].Value = maskedTextBox1.Text;
            
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT @@IDENTITY";
            int lastId = Convert.ToInt32(cmd.ExecuteScalar());


            cmd.CommandText = @"INSERT INTO [Сотрудники] (ID_Должность, ID_Отдела, ID_ПерснСотрудника, Дата_Регистрации_Сотрудника) values (@ID_Должность, @ID_Отдела, @ID_ПерснСотрудника, @Дата_Регистрации_Сотрудника)";
            
            cmd.Parameters.Add("@ID_Должность", SqlDbType.Int);
            cmd.Parameters["@ID_Должность"].Value = id_dolz;

            cmd.Parameters.Add("@ID_Отдела", SqlDbType.Int);
            cmd.Parameters["@ID_Отдела"].Value = id_otdel;

            cmd.Parameters.Add("@ID_ПерснСотрудника", SqlDbType.Int);
            cmd.Parameters["@ID_ПерснСотрудника"].Value = lastId;

            cmd.Parameters.Add("@Дата_Регистрации_Сотрудника", SqlDbType.NVarChar);
            cmd.Parameters["@Дата_Регистрации_Сотрудника"].Value = DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss");
            
            cmd.ExecuteNonQuery();
            tran.Commit(); // Потвержение транзакции


            Class1 clas = new Class1();
            clas.users_ychet("Добавлене нового сотрудника");

            MessageBox.Show("Запись добавлена", "Добавление");
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();
            //conn.Close();


        }

        







        private void NewSotrudForm_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 100;
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            // Отдел

            SqlConnection connRC = new SqlConnection(connectString);
            string command = "SELECT ID_Отдела, Отдел FROM Отделы";
            SqlDataAdapter da = new SqlDataAdapter(command, connRC);

            DataSet ds = new DataSet();
            connRC.Open();
            da.Fill(ds);
            connRC.Close();

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Отдел";
            comboBox1.ValueMember = "ID_Отдела";
            // Отдел


            // Должность
            dolz();

            //Должность
        }


        void dolz()
        {
            SqlConnection connRC = new SqlConnection(connectString);
            string command = "SELECT Должность FROM Должность";
            SqlDataAdapter da = new SqlDataAdapter(command, connRC);

            DataSet ds = new DataSet();
            connRC.Open();
            da.Fill(ds);
            connRC.Close();

            comboBox2.DataSource = ds.Tables[0];
            comboBox2.DisplayMember = "Должность";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();
        }

        private void NewSotrudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();

        }
    }
}
