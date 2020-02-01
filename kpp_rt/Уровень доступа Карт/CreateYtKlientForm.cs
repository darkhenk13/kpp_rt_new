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
    public partial class CreateYtKlientForm : Form
    {
        public CreateYtKlientForm()
        {
            InitializeComponent();
        }
        public string[] client1 = new string[4];
        public int status_radio1;
        public string[] object_table1 = new string[4];



        private void CreateYtKlientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CreateYrForm form = new CreateYrForm();
            this.Hide();
            form.Show();

        }

        private void CreateYtKlientForm_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
          for (int i = 0; i < 4; i++)
            {
                client1[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }
          
            CreateYrForm form = new CreateYrForm();
            form.client = client1;
            form.status_radio = status_radio1;
            form.object_table = object_table1;
            this.Hide();
            form.Show();


        }
    }
}
