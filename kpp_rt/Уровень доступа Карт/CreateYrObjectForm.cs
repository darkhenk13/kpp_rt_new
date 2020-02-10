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
    public partial class CreateYrObjectForm : Form
    {
        public CreateYrObjectForm()
        {
            InitializeComponent();
        }
        public string[] object_table1 = new string[4];
        public string[] client1 = new string[4];
        public string[] sotrudnik1 = new string[4];
        public int status_radio1;
        public int k;

        //Edit
        public string[] arr1 = new string[8];
        public string[] arr_klient = new string[8];

        private void CreateYrObjectForm_Load(object sender, EventArgs e)
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
            command.CommandText = @"SELECT Город, Улица, Здание, Этаж FROM Объект";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void CreateYrObjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CreateYrForm form = new CreateYrForm();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (k == 1)
            {
               
            }
            else 
            
            {
                for (int i = 0; i < 4; i++)
                {
                    object_table1[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();

                }


                CreateYrForm form = new CreateYrForm();
                form.object_table = object_table1;
                form.client = client1;
                form.sotrudnik = sotrudnik1;
                form.status_radio = status_radio1;
                this.Hide();
                form.Show();

            }

        }
    }
}
