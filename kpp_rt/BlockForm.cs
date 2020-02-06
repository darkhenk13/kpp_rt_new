using kpp_rt.Карта;
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
    public partial class BlockForm : Form
    {
        public BlockForm()
        {
            InitializeComponent();
        }

        private void BlockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CartForm form = new CartForm();
            this.Hide();
            form.Show();

        }

        private void BlockForm_Load(object sender, EventArgs e)
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
Карта.ID_Карты as [Идентификатор карты],
ЗаблокированныеКарты.Дата_Блокировки as [Дата Блокировки],
ЗаблокированныеКарты.Блокировка,
ПерссональныеДанныеСотрудника.ФИО as [ФИО Сотрудника],
Должность.Должность,
Отделы.Отдел
FROM ЗаблокированныеКарты, Карта, Сотрудники, ПерссональныеДанныеСотрудника, Должность, Отделы
WHERE ЗаблокированныеКарты.ID_Карты = Карта.ID_Карты
AND Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника
AND Сотрудники.ID_Должность = Должность.ID_Должность
AND Сотрудники.ID_Отдела = Отделы.ID_Отдела
AND Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника
";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateBlockForm form = new CreateBlockForm();
            this.Hide();
            form.Show();
        }
    }
}
