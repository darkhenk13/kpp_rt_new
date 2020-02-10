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
using kpp_rt.Клиенты;
using kpp_rt.Отчеты;

namespace kpp_rt
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;
        public string[] arr_del = new string[4];



        private void AdminForm_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dataGridView3.ReadOnly = true;
            this.MinimumSize = new System.Drawing.Size(800, 480);

            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            Properties.Settings.Default.admin_form = "Admin";

            Properties.Settings.Default.Save();



            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style1 = dataGridView2.ColumnHeadersDefaultCellStyle;
            style1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style2 = dataGridView3.ColumnHeadersDefaultCellStyle;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;



            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            command.Connection = connection;
            command.CommandText = @"SELECT 
пп.login AS [Логин], 
перс.ФИО AS [ФИО Сотрудника], 
перс.Дата_Рождения AS [Дата рождения],
CASE WHEN прав.Права_Доступа='1' THEN 'Администратор' ELSE 'Оператор' END AS [Права Доступа]
FROM ПользователиПрограммы пп, Сотрудники ст, ПерссональныеДанныеСотрудника перс, ПраваДоступа прав
WHERE пп.ID_Сотруднка = ст.ID_Сотрудника
AND ст.ID_ПерснСотрудника = перс.ID_ПерснСотрудника
AND пп.ID_ПравДоступа = прав.ID_ПравДоступа";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();

            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();
            DataSet ds1 = new DataSet();
            SqlDataAdapter adap1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();

            command1.Connection = connection1;
            command1.CommandText = @"SELECT CASE WHEN Права_Доступа='1' THEN 'Администратор' ELSE 'Оператор' END AS [Права доступа] FROM ПраваДоступа";
            connection1.Open();
            adap1.SelectCommand = command1;
            adap1.Fill(ds1);
            dt1 = ds1.Tables[0];
            dataGridView2.DataSource = dt1;
            connection1.Close();


            table_3();

        }



        private void table_3()
        {
            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();
            DataSet ds1 = new DataSet();
            SqlDataAdapter adap1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();

            command1.Connection = connection1;
            command1.CommandText = @"SELECT 
лю.ID_LogUsers AS [ID], 
лю.Время, 
лю.Дата, 
лю.Действие, 
пдс.ФИО AS [ФИО Сотрудника],
пдс.Дата_Рождения AS [Дата рождения]
FROM Log_users лю, ПользователиПрограммы пп, Сотрудники сот, ПерссональныеДанныеСотрудника пдс
WHERE лю.ID_Users = пп.ID_Users
AND пп.ID_Сотруднка = сот.ID_Сотрудника
AND сот.ID_ПерснСотрудника = пдс.ID_ПерснСотрудника";
            connection1.Open();
            adap1.SelectCommand = command1;
            adap1.Fill(ds1);
            dt1 = ds1.Tables[0];
            dataGridView3.DataSource = dt1;
            connection1.Close();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ComPortSettingsForm form = new ComPortSettingsForm();
            this.Hide();
            form.Show();

        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class1 clas = new Class1();
            clas.users_ychet("Выход из панели администратора");

            Form1 form = new Form1();
            this.Hide();
            form.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn2 = new SqlConnection(Form1.connectString);
            SqlCommand command2 = new SqlCommand();

            command2.Connection = conn2;
            conn2.Open();
            command2.CommandText = @"TRUNCATE TABLE Log_users";

            command2.ExecuteNonQuery();

            conn2.Close();
            MessageBox.Show("Таблицы учета пользовтелей очищена!");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MestoKppForm form = new MestoKppForm();
            this.Hide();
            form.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateAdminForm form = new CreateAdminForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            KlientForm form = new KlientForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            OtchetForm form = new OtchetForm();
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string id_users = "";
            string id_prav_dost = "";
            string group;

            for (int i = 0; i < 4; i++)
            {
                arr_del[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }


           

            DialogResult dialogResult = MessageBox.Show("Удалить Пользователя?", "Удалить", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {

                    SqlConnection connection = new SqlConnection(connectString);
                    SqlCommand command = new SqlCommand();


                    if (arr_del[3] == "Администратор")
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT ID_ПравДоступа, Права_Доступа FROM ПраваДоступа WHERE Права_Доступа='1'";
                        connection.Open();

                        SqlDataReader reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            id_prav_dost = reader1[0].ToString();
                            string pp = reader1[1].ToString();
                        }
                        connection.Close();
                    }
                    else
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT ID_ПравДоступа, Права_Доступа FROM ПраваДоступа WHERE Права_Доступа='2'";
                        connection.Open();

                        SqlDataReader reader2 = command.ExecuteReader();
                        while (reader2.Read())
                        {
                            id_prav_dost = reader2[0].ToString();
                            string pp = reader2[1].ToString();
                        }
                        connection.Close();
                    }


                    command.Connection = connection;
                    command.CommandText = "SELECT ID_Users, login, ID_ПравДоступа FROM ПользователиПрограммы WHERE login='" + arr_del[0] + "' AND ID_ПравДоступа='" + id_prav_dost + "'";
                    connection.Open();


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        id_users = reader[0].ToString();
                        string users = reader[1].ToString();
                        string id_pravdostupa = reader[2].ToString();
                    }
                    connection.Close();

                    command.Connection = connection;
                    command.CommandText = @"DELETE FROM ПользователиПрограммы WHERE ID_Users='" + id_users + "'";
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    Class1 clas = new Class1();
                    clas.users_ychet("Удаление пользователя программы");


                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row);
                    }

                    MessageBox.Show("Пользователь удален", "Удалено");
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить пользователя", "Внимание");
                }

            }
            else
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 4; i++)
            {
                arr_del[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }


            EditAdminForm form = new EditAdminForm();
            form.arr_del1 = arr_del;
            this.Hide();
            form.Show();
        }
    }
}
