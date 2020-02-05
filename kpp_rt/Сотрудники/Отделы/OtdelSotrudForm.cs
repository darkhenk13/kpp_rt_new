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

namespace kpp_rt.Сотрудники.Отделы
{
    public partial class OtdelSotrudForm : Form
    {
        public OtdelSotrudForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateOtdelSotrudForm form = new CreateOtdelSotrudForm();
            this.Hide();
            form.Show();
        }

        private void OtdelSotrudForm_Load(object sender, EventArgs e)
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
            command.CommandText = @"SELECT Отдел FROM Отделы";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 1; i++)
                {
                    arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }
                EditOtdelForm form = new EditOtdelForm();
                form.arr1 = arr;
                this.Hide();
                form.Show();
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        public string[] arr = new string[1];
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 1; i++)
                {
                    arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }
                string id_dol = "";


                DialogResult dialogResult = MessageBox.Show("Удалить Должность?", "Удалить", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    /*
                     CREATE TABLE Отделы(
        ID_Отдела int IDENTITY (1,1),
        Отдел nvarchar(100) NULL,
        PRIMARY KEY (ID_Отдела)
    );*/
                    // получить ID Отдела
                    SqlConnection connection1 = new SqlConnection(Form1.connectString);
                    SqlCommand command1 = new SqlCommand();

                    command1.Connection = connection1;
                    connection1.Open();
                    command1.CommandText = "SELECT ID_Отдела, Отдел FROM Отделы WHERE Отдел='" + arr[0] + "'";

                    SqlDataReader reader1 = command1.ExecuteReader();

                    while (reader1.Read())
                    {
                        id_dol = reader1[0].ToString();
                        string pers = reader1[1].ToString();

                    }
                    connection1.Close();

                    //конец ID клиента
                    SqlConnection connection3 = new SqlConnection(connectString);
                    SqlCommand command3 = new SqlCommand();
                    command3.Connection = connection3;
                    connection3.Open();
                    command3.CommandText = @"DELETE FROM Должность WHERE ID_Должность='" + id_dol + "'";
                    command3.ExecuteNonQuery();
                    connection3.Close();

                    Class1 clas = new Class1();
                    clas.users_ychet("Удаление отдела");

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row);
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            catch { MessageBox.Show("Ошибка"); }


        }

        private void OtdelSotrudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();
        }
    }
}
