using kpp_rt.Объект;
using kpp_rt.Уровень_доступа_Карт;
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
    public partial class CartForm : Form
    {
        public CartForm()
        {
            InitializeComponent();
        }


        public string[] arr = new string[8];

        private void CartForm_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            /* SELECT ЗаблокированныеКарты.Блокировка, ЗаблокированныеКарты.Дата_Блокировки
 FROM Карта
 JOIN Сотрудники
 ON Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника
 JOIN УровеньДоступа
 ON Карта.ID_УрДоступа = УровеньДоступа.ID_УрДоступа
 JOIN Клиенты
 ON Карта.ID_Клиента = Клиенты.ID_Клиента
 JOIN ЗаблокированныеКарты
 ON Карта.ID_ЗаблКарта = ЗаблокированныеКарты.ID_ЗаблКарта */

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            /*CREATE TABLE Карта(
	ID_Карты int,
	ID_Сотрудника int NULL,	
	PRIMARY KEY (ID_Карты)
);*/

            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();


            //CASE WHEN УчетПосещений.Статус='true' THEN 'Вошел' ELSE 'Вышел' END AS [Статус],
            command.Connection = connection;
            command.CommandText = @"SELECT
	Карта.ID_Карты AS [Идентификатор карты], 
	ПерссональныеДанныеСотрудника.ФИО AS [ФИО Сотрудника], 
	ПерссональныеДанныеСотрудника.Дата_Рождения, 
	ПерссональныеДанныеСотрудника.Номер_телефона, 
	Должность.Должность,
	Отделы.Отдел,
    CASE WHEN ЗаблокированныеКарты.Блокировка='1' THEN 'Заблокирована' ELSE 'Разблокирована' END AS [Блокировка],
    ЗаблокированныеКарты.Дата_Блокировки AS [Дата Блокировки]
FROM  Карта
JOIN Сотрудники ON Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника
JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника
JOIN Должность ON Сотрудники.ID_Должность = Должность.ID_Должность
JOIN Отделы ON Сотрудники.ID_Отдела = Отделы.ID_Отдела
JOIN ЗаблокированныеКарты ON Карта.ID_ЗаблКарта = ЗаблокированныеКарты.ID_ЗаблКарта" + " ORDER BY Карта.ID_Карты DESC";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            TestObjectForm form = new TestObjectForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateCardForm f = new CreateCardForm();
            this.Hide();
            f.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            
        }

        private void CartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OperatorForm form = new OperatorForm();
            this.Hide();
            form.Show();
        }

        

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }

                EditCardForm form = new EditCardForm();
                form.arr1 = arr;
                this.Hide();
                form.Show();
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            string dates = DateTime.Now.ToString("dd-MM-yyyy");
            try
            {
                //string idcard = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                for (int i = 0; i < 8; i++)
                {
                    arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }

                if (arr[6] == "Разблокирована")
                {
                    
                    block_cart_unlock(1, "Блокировка карты", dates);
                    DataGridViewTextBoxCell txtxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6];
                    txtxCell.Value = "Заблокирована";

                    DataGridViewTextBoxCell txtxCell1 = (DataGridViewTextBoxCell)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7];
                    txtxCell1.Value = dates;


                    MessageBox.Show("Карта Заблокирована");
                }
                else
                {
                    MessageBox.Show("Карта уже заблокирована");
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }


        private void block_cart_unlock(int bb, string bl, string dates)
        {
            
            string id_block = "";
            // поиск ID Заблокированной карты
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_Карты, ID_ЗаблКарта FROM Карта WHERE ID_Карты='" + arr[0] + "'";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                 string t = reader[0].ToString();
                id_block = reader[1].ToString();
                //textBox2.Text = reader[2].ToString();
                //textBox3.Text = reader[3].ToString();
            }
            connection.Close();
            // поиск ID Заблокированной карты

            // Блокировка карты
            SqlConnection connection1 = new SqlConnection(Form1.connectString);
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection1;
            command1.CommandText = @"UPDATE ЗаблокированныеКарты SET Блокировка='" + bb + "', Дата_Блокировки='" + dates + "'  WHERE ID_ЗаблКарта=" + id_block + "";
            connection1.Open();
            command1.ExecuteNonQuery();
            connection1.Close();

         

            Class1 clas = new Class1();
            clas.users_ychet(bl);
            // Блокировка карты
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }

                if (arr[6] == "Разблокирована")
                {
                    MessageBox.Show("Карта уже разблокирована");
                }
                else
                {
                    
                    block_cart_unlock(0, "Разблокировка карты", "");
                    DataGridViewTextBoxCell txtxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6];
                    txtxCell.Value = "Разблокирована";
                    DataGridViewTextBoxCell txtxCell1 = (DataGridViewTextBoxCell)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7];
                    txtxCell1.Value = "";
                    MessageBox.Show("Карта разблокирована");
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {

                string id1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();

                DialogResult dialogResult = MessageBox.Show("Удалить Карту?", "Удалить", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlConnection connection3 = new SqlConnection(Form1.connectString);
                    SqlCommand command3 = new SqlCommand();
                    command3.Connection = connection3;
                    connection3.Open();
                    command3.CommandText = @"DELETE FROM Карта WHERE ID_Карты='" + id1 + "'";
                    command3.ExecuteNonQuery();
                    connection3.Close();



                    Class1 clas = new Class1();
                    clas.users_ychet("Карта удалена");

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                    MessageBox.Show("Карта удалена!", "Удаление");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            command.Connection = connection;
            /*
             SELECT Клиенты.Дата_Регистрации_Клиента, ПерсональныеДанныеКлиентов.ФИО, ПерсональныеДанныеКлиентов.Номер_Паспорта, ПерсональныеДанныеКлиентов.Дата_Рождения
FROM Клиенты
JOIN ПерсональныеДанныеКлиентов ON Клиенты.ID_ПерснДанныеКлиента = ПерсональныеДанныеКлиентов.ID_ПерснДаннКлиента
             */

            command.CommandText = @"SELECT
	Карта.ID_Карты AS [Идентификатор карты], 
	ПерссональныеДанныеСотрудника.ФИО AS [ФИО Сотрудника], 
	ПерссональныеДанныеСотрудника.Дата_Рождения, 
	ПерссональныеДанныеСотрудника.Номер_телефона, 
	Должность.Должность,
	Отделы.Отдел,
    CASE WHEN ЗаблокированныеКарты.Блокировка='1' THEN 'Заблокирована' ELSE 'Разблокирована' END AS [Блокировка],
    ЗаблокированныеКарты.Дата_Блокировки AS [Дата Блокировки]
FROM  Карта
JOIN Сотрудники ON Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника
JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника
JOIN Должность ON Сотрудники.ID_Должность = Должность.ID_Должность
JOIN Отделы ON Сотрудники.ID_Отдела = Отделы.ID_Отдела
JOIN ЗаблокированныеКарты ON Карта.ID_ЗаблКарта = ЗаблокированныеКарты.ID_ЗаблКарта
WHERE ПерссональныеДанныеСотрудника.ФИО like '%" + toolStripTextBox1.Text + "%' OR Должность.Должность like '%" + toolStripTextBox1.Text + "%' OR  Отделы.Отдел like '%" + toolStripTextBox1.Text + "%'" + " ORDER BY Карта.ID_Карты DESC";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }
    }
}
