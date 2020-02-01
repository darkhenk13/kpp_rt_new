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
            	УровеньДоступа.Допуск, 
            	УровеньДоступа.Этаж AS [Этаж],
            	Объект.Город,
            	Объект.Здание,
            	Объект.Улица,
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


            /*
*/



        }
    }
}
