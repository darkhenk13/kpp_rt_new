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
    public partial class YrDostupForm : Form
    {
        public YrDostupForm()
        {
            InitializeComponent();
        }

        public string[] arr = new string[8];
        public string id_pers_klienta;
        public string id_klienta;
        public string id_yr_dost;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateYrForm form = new CreateYrForm();
            this.Hide();
            form.Show();
        }

        private void YrDostupForm_Load(object sender, EventArgs e)
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
            command.CommandText = @"SELECT 
            	CASE WHEN УровеньДоступа.Допуск='1' THEN 'Разрешен' ELSE 'Запрещен' END AS [Допуск], 
            	УровеньДоступа.Этаж AS [Этаж],
            	Объект.Город,
            	Объект.Улица,
                Объект.Здание,
            	Объект.Этаж AS [Этажей],
            	ПерссональныеДанныеСотрудника.ФИО AS [ФИО Сотрудника],
            	ПерсональныеДанныеКлиентов.ФИО AS [ФИО Клиента]
            FROM УровеньДоступа
            LEFT JOIN Объект ON УровеньДоступа.ID_Объекта = Объект.ID_Объекта
            LEFT JOIN Карта ON УровеньДоступа.ID_Карты = Карта.ID_Карты
            LEFT JOIN Сотрудники ON Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника
            LEFT JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника
            LEFT JOIN Клиенты ON УровеньДоступа.ID_Клиента = Клиенты.ID_Клиента
            LEFT JOIN ПерсональныеДанныеКлиентов ON Клиенты.ID_ПерснДанныеКлиента = ПерсональныеДанныеКлиентов.ID_ПерснДаннКлиента";
            //command.CommandText = @"SELECT * FROM УровеньДоступа";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
         
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }

                yrdost_delete_klient();
                yrdost_id();
                string id1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();

                DialogResult dialogResult = MessageBox.Show("Удалить запись?", "Удалить", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlConnection connection3 = new SqlConnection(Form1.connectString);
                    SqlCommand command3 = new SqlCommand();
                    command3.Connection = connection3;
                    connection3.Open();
                    command3.CommandText = @"DELETE FROM УровеньДоступа WHERE ID_УрДоступа='" + id_yr_dost + "'";
                    command3.ExecuteNonQuery();
                    connection3.Close();



                    Class1 clas = new Class1();
                    clas.users_ychet("Карта удалена");

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                    MessageBox.Show("Запись удалена!", "Удаление");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
        }
            catch { MessageBox.Show("Ошибка"); }

}


        // ID Персональные данные клиента и ID клиента
        private void yrdost_delete_klient()
        {
            
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_ПерснДаннКлиента, ФИО FROM ПерсональныеДанныеКлиентов WHERE ФИО='" + arr[7] + "'";

            // command.CommandText = "SELECT ID_Пользователь, login, password, role, Фамилия, Имя, Отчество, ДатаРегистрации FROM users WHERE ID_Пользователь=" + id + "";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_pers_klienta = reader[0].ToString();
                string fio = reader[1].ToString();              
            }
            connection.Close();



            command.Connection = connection;
            command.CommandText = "SELECT ID_Клиента, ID_ПерснДанныеКлиента FROM Клиенты WHERE ID_ПерснДанныеКлиента='" + id_pers_klienta + "'";
            connection.Open();

            SqlDataReader reader1 = command.ExecuteReader();

            while (reader1.Read())
            {
                id_klienta = reader1[0].ToString();
                string n = reader1[1].ToString();
            }
            connection.Close();


        }

        // уровень доступа получение ид
        private void yrdost_id()
        {
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_УрДоступа FROM УровеньДоступа WHERE ID_Клиента='" + id_klienta + "'";

             connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_yr_dost = reader[0].ToString();             
            }
            connection.Close();
        }



        private void YrDostupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OperatorForm form = new OperatorForm();
            this.Hide();
            form.Show();
        }
    }
}
