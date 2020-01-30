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

namespace kpp_rt.Объект
{
    public partial class ObjectForm : Form
    {
        public ObjectForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;
        public string[] arr_object = new string[4];

        private void ObjectForm_Load(object sender, EventArgs e)
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateObjectForm form = new CreateObjectForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
           
            //Редактирование строки
            for (int i = 0; i < 4; i++)
            {
                arr_object[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }

            EditObjectForm form = new EditObjectForm();
            form.arr_object1 = arr_object;
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            // ********************************************************
            string[] arr_del = new string[4];
            string id1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            string id_objecta = "";
            string Gorod;
            string Yl;
            string Zd;
            string Ut;
            

            for (int i = 0; i < 4; i++)
            {
                arr_del[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
            }

            DialogResult dialogResult = MessageBox.Show("Удалить клиента?", "Удалить", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // получить ID Персонаьные данные клиента
                SqlConnection connection = new SqlConnection(connectString);
                SqlCommand command = new SqlCommand();

                command.Connection = connection;
                command.CommandText = "SELECT ID_Объекта, Город, Улица, Здание, Этаж FROM Объект WHERE Город='" + arr_del[0] + "' AND Улица='" + arr_del[1] + "' AND Здание='" + arr_del[2] + "' AND Этаж='" + arr_del[3] + "'";
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id_objecta = reader[0].ToString();
                    Gorod = reader[1].ToString();
                    Yl = reader[2].ToString();
                    Zd = reader[3].ToString();
                    Ut = reader[4].ToString();
                }
                connection.Close();
                // конец ID Персонаьные данные клиента

               

                //конец ID клиента
                SqlConnection connection3 = new SqlConnection(connectString);
                SqlCommand command3 = new SqlCommand();
                command3.Connection = connection3;
                connection3.Open();
                command3.CommandText = @"DELETE FROM Объект WHERE ID_Объекта='" + id_objecta + "'";
                command3.ExecuteNonQuery();
                connection3.Close();

                Class1 clas = new Class1();
                clas.users_ychet("Удаление объекта");

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }

            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


            //_______________________________________________



        }

        private void ObjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OperatorForm form = new OperatorForm();
            this.Hide();
            form.Show();
        }
    }
}
