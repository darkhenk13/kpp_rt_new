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


        public string[] arr = new string[6];

        private void CartForm_Load(object sender, EventArgs e)
        {
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

            command.Connection = connection;
            command.CommandText = @"SELECT
	Карта.ID_Карты AS [Идентификатор карты], 
	ПерссональныеДанныеСотрудника.ФИО AS [ФИО Сотрудника], 
	ПерссональныеДанныеСотрудника.Дата_Рождения, 
	ПерссональныеДанныеСотрудника.Номер_телефона, 
	Должность.Должность,
	Отделы.Отдел
FROM  Карта
JOIN Сотрудники ON Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника
JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника
JOIN Должность ON Сотрудники.ID_Должность = Должность.ID_Должность
JOIN Отделы ON Сотрудники.ID_Отдела = Отделы.ID_Отдела
";
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
            ObjectForm form = new ObjectForm();
            this.Hide();
            form.Show();
        }

        private void CartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OperatorForm form = new OperatorForm();
            this.Hide();
            form.Show();
        }

        

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }

            EditCardForm form = new EditCardForm();
            //form.arr1 = arr;
            this.Hide();
            form.Show();
        }
    }
}
