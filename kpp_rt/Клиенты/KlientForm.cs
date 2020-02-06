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
    public partial class KlientForm : Form
    {
        public KlientForm()
        {
            InitializeComponent();
        }
        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;

        private void KlientForm_Load(object sender, EventArgs e)
        {

            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;




            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            command.Connection = connection;
            command.CommandText = @"SELECT Клиенты.Дата_Регистрации_Клиента, ПерсональныеДанныеКлиентов.ФИО, ПерсональныеДанныеКлиентов.Номер_Паспорта, ПерсональныеДанныеКлиентов.Дата_Рождения
FROM Клиенты
JOIN ПерсональныеДанныеКлиентов ON Клиенты.ID_ПерснДанныеКлиента = ПерсональныеДанныеКлиентов.ID_ПерснДаннКлиента";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateKlientForm form = new CreateKlientForm();
            this.Hide();
            form.Show();
        }
        public string[] arr = new string[4];

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                //Редактирование строки
                for (int i = 0; i < 4; i++)
                {
                    arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }

                //for (int j = 0; j < 4; j++)
                //{
                //    MessageBox.Show(" ", arr[j]);

                //}

                EditKlientForm form = new EditKlientForm();
                form.arr1 = arr;
                this.Hide();
                form.Show();
            }
            catch { MessageBox.Show("Ошибка"); }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                string[] arr_del = new string[4];
                string id1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                string id_pers = "";
                string FIO = "";
                string Nomer_Passport = "";
                string Date_roz = "";
                string id_clienta = "";
                string id_persdan = "";
                string date_reg = "";

                for (int i = 0; i < 4; i++)
                {
                    arr_del[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }

                DialogResult dialogResult = MessageBox.Show("Удалить клиента?", "Удалить", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // получить ID Персонаьные данные клиента
                    SqlConnection connection = new SqlConnection(connectString);
                    SqlCommand command = new SqlCommand();

                    command.Connection = connection;
                    command.CommandText = "SELECT ID_ПерснДаннКлиента, ФИО, Номер_Паспорта, Дата_Рождения FROM ПерсональныеДанныеКлиентов WHERE ФИО='" + arr_del[1] + "' AND Номер_Паспорта='" + arr_del[2] + "' AND Дата_Рождения='" + arr_del[3] + "'";
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        id_pers = reader[0].ToString();
                        FIO = reader[1].ToString();
                        Nomer_Passport = reader[2].ToString();
                        Date_roz = reader[3].ToString();
                    }
                    connection.Close();
                    // конец ID Персонаьные данные клиента

                    // получить ID клиента
                    SqlConnection connection1 = new SqlConnection(connectString);
                    SqlCommand command1 = new SqlCommand();

                    command1.Connection = connection1;
                    connection1.Open();
                    command1.CommandText = "SELECT ID_Клиента, ID_ПерснДанныеКлиента, Дата_Регистрации_Клиента FROM Клиенты WHERE ID_ПерснДанныеКлиента='" + id_pers + "' AND Дата_Регистрации_Клиента='" + arr_del[0] + "'";

                    SqlDataReader reader1 = command1.ExecuteReader();

                    while (reader1.Read())
                    {
                        id_clienta = reader1[0].ToString();
                        id_persdan = reader1[1].ToString();
                        date_reg = reader1[2].ToString();

                    }
                    connection1.Close();

                    //конец ID клиента
                    SqlConnection connection3 = new SqlConnection(connectString);
                    SqlCommand command3 = new SqlCommand();
                    command3.Connection = connection3;
                    connection3.Open();
                    command3.CommandText = @"DELETE FROM Клиенты WHERE ID_Клиента='" + id_clienta + "'";
                    command3.ExecuteNonQuery();
                    connection3.Close();

                    command3.Connection = connection3;
                    command3.CommandText = @"DELETE FROM ПерсональныеДанныеКлиентов WHERE ID_ПерснДаннКлиента='" + id_pers + "'";
                    connection3.Open();
                    command3.ExecuteNonQuery();
                    connection3.Close();

                    Class1 clas = new Class1();
                    clas.users_ychet("Удаление нового сотрудника");

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row);
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

      


        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    klient[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }

                search_persid();
                search_klientid();
                search_status();
                SqlConnection conn = new SqlConnection(connectString);
                SqlCommand cmd = new SqlCommand();
                string dates = DateTime.Now.ToString("dd-MM-yyyy");

                string times = DateTime.Now.ToString("HH:mm:ss");

                if (status_klienta != "true")
                {

                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO[УчетПосещений] (Время, Дата, Статус, ID_Клиента) values (@Время, @Дата, @Статус, @ID_Клиента)";

                    cmd.Parameters.Add("@Время", SqlDbType.NVarChar);
                    cmd.Parameters["@Время"].Value = times;

                    cmd.Parameters.Add("@Дата", SqlDbType.NVarChar);
                    cmd.Parameters["@Дата"].Value = dates;

                    cmd.Parameters.Add("@Статус", SqlDbType.NVarChar);
                    cmd.Parameters["@Статус"].Value = "true";

                    cmd.Parameters.Add("@ID_Клиента", SqlDbType.NVarChar);
                    cmd.Parameters["@ID_Клиента"].Value = id_klient;

                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                else
                {

                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO[УчетПосещений] (Время, Дата, Статус, ID_Клиента) values (@Время, @Дата, @Статус, @ID_Клиента)";

                    cmd.Parameters.Add("@Время", SqlDbType.NVarChar);
                    cmd.Parameters["@Время"].Value = times;

                    cmd.Parameters.Add("@Дата", SqlDbType.NVarChar);
                    cmd.Parameters["@Дата"].Value = dates;

                    cmd.Parameters.Add("@Статус", SqlDbType.NVarChar);
                    cmd.Parameters["@Статус"].Value = "false";

                    cmd.Parameters.Add("@ID_Клиента", SqlDbType.NVarChar);
                    cmd.Parameters["@ID_Клиента"].Value = id_klient;

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }


                MessageBox.Show("Клиент добавлен в учет посещений", "Добавление новой записи", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch { MessageBox.Show("Ошибка"); }

          




        }

        public string[] klient = new string[4];
        public string id_persd;
        public string id_klient;
        public string status_klienta;
        void search_persid()
        {
            
            string fio;
            string passport;
            string dateroz;
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT ID_ПерснДаннКлиента, ФИО, Номер_Паспорта, Дата_Рождения FROM ПерсональныеДанныеКлиентов WHERE ФИО='" + klient[1] + "' AND Номер_Паспорта='" + klient[2] + "'AND Дата_Рождения='" + klient[3] + "'";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_persd = reader[0].ToString();
                fio = reader[1].ToString();
                passport = reader[2].ToString();
                dateroz = reader[3].ToString();

            }
            connection.Close();
        }

        void search_klientid()
        {
            
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT ID_Клиента, ID_ПерснДанныеКлиента, Дата_Регистрации_Клиента  FROM Клиенты WHERE ID_ПерснДанныеКлиента='" + id_persd + "' AND Дата_Регистрации_Клиента='" + klient[0] + "'";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_klient = reader[0].ToString();
                string id_pers_kl = reader[1].ToString();
                string datereg = reader[2].ToString();
                

            }
            connection.Close();
        }

        void search_status()
        {
            
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT Статус, ID_Клиента FROM УчетПосещений WHERE ID_Клиента='" + id_klient + "'";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
               status_klienta = reader[0].ToString();
               string id_klienta = reader[1].ToString();
                


            }
            connection.Close();
        }

        private void KlientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OperatorForm form = new OperatorForm();
            this.Hide();
            form.Show();
        }
    }
}
