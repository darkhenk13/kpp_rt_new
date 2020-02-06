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
    public partial class AdminMestoKppCreateForm : Form
    {
        public AdminMestoKppCreateForm()
        {
            InitializeComponent();
        }
        public string[] arr1 = new string[4];
        public int k1;
        private void AdminMestoKppCreateForm_Load(object sender, EventArgs e)
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

        private void AdminMestoKppCreateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MestoKppForm form = new MestoKppForm();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                k1 = 1;
                for (int i = 0; i < 4; i++)
                {
                    arr1[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }
                MestoKppForm form = new MestoKppForm();
                form.arr = arr1;
                form.k = k1;
                this.Hide();
                form.Show();
            }
            catch
            { 
                MessageBox.Show("Ошибка");
                MestoKppForm form = new MestoKppForm();
                this.Hide();
                form.Show();
            }
        }
    }
}
