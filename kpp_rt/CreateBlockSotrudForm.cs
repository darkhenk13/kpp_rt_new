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

namespace kpp_rt
{
    public partial class CreateBlockSotrudForm : Form
    {
        public CreateBlockSotrudForm()
        {
            InitializeComponent();
        }
        public string[] arr1 = new string[6];

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 6; i++)
            {
                arr1[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }

            CreateBlockForm form = new CreateBlockForm();
            form.arr = arr1;
            this.Hide();
            form.Show();
        }

        private void CreateBlockSotrudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CreateBlockForm form = new CreateBlockForm();
            this.Hide();
            form.Show();
        }

        private void CreateBlockSotrudForm_Load(object sender, EventArgs e)
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
    }
}
