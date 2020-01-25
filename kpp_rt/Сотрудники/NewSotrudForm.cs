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

        private void button2_Click(object sender, EventArgs e)
        {
            int hack = 0;

            SqlConnection conn = new SqlConnection(connectString);
            
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
            cmd.Parameters["@Номер_телефона"].Value = textBox2.Text;

            cmd.Parameters.Add("@Дата_Рождения", SqlDbType.NVarChar);
            cmd.Parameters["@Дата_Рождения"].Value = textBox3.Text;
            
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT @@IDENTITY";
            int lastId = Convert.ToInt32(cmd.ExecuteScalar());


            cmd.CommandText = @"INSERT INTO [Сотрудники] (ID_Должность, ID_Users, ID_ПерснСотрудника, Дата_Регистрации_Сотрудника, ID_ЗаблКарта) values (@ID_Должность, @ID_Users, @ID_ПерснСотрудника, @Дата_Регистрации_Сотрудника, @ID_ЗаблКарта)";
            
            cmd.Parameters.Add("@ID_Должность", SqlDbType.Int);
            cmd.Parameters["@ID_Должность"].Value = "123";

            cmd.Parameters.Add("@ID_Users", SqlDbType.Int);
            cmd.Parameters["@ID_Users"].Value = "21";

            cmd.Parameters.Add("@ID_ПерснСотрудника", SqlDbType.Int);
            cmd.Parameters["@ID_ПерснСотрудника"].Value = lastId;

            cmd.Parameters.Add("@Дата_Регистрации_Сотрудника", SqlDbType.NVarChar);
            cmd.Parameters["@Дата_Регистрации_Сотрудника"].Value = "123";

            //ID_ЗаблКарта
            cmd.Parameters.Add("@ID_ЗаблКарта", SqlDbType.Int);
            cmd.Parameters["@ID_ЗаблКарта"].Value = "123";
            cmd.ExecuteNonQuery();


            tran.Commit(); // Потвержение транзакции

            MessageBox.Show("Результат: ", Convert.ToString(hack));
            //conn.Close();


        }


    }
}
