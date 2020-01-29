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

namespace kpp_rt.Сотрудники.Должность
{
    public partial class SotrudDolzForm : Form
    {
        public SotrudDolzForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateDolzSotrudForm form = new CreateDolzSotrudForm();
            this.Hide();
            form.Show();
        }

        private void SotrudDolzForm_Load(object sender, EventArgs e)
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


            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();
            command.Connection = connection;

            command.CommandText = @"SELECT Должность FROM Должность";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();




        }
        public string[] arr = new string[1];
        public string[] arr_DEL = new string[1];
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
            //Редактирование строки
            for (int i = 0; i < 1; i++)
            {
                arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }
            EditDolzForm form = new EditDolzForm();
            form.arr1 = arr;
            this.Hide();
            form.Show();

        }

        private void SotrudDolzForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1; i++)
            {
                arr_DEL[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }
            string id_dol = "";


            DialogResult dialogResult = MessageBox.Show("Удалить Должность?", "Удалить", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                /*CREATE TABLE Должность(
	ID_Должность int IDENTITY (1,1),
	Должность nvarchar(100) NULL,
	PRIMARY KEY (ID_Должность)
);*/

                // получить ID Должности
                SqlConnection connection1 = new SqlConnection(Form1.connectString);
                SqlCommand command1 = new SqlCommand();

                command1.Connection = connection1;
                connection1.Open();
                command1.CommandText = "SELECT ID_Должность, Должность FROM Должность WHERE Должность='" + arr_DEL[0] + "'";

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
    }
}
