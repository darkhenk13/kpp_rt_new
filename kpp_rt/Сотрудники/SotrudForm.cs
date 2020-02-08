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
using kpp_rt.Сотрудники.Должность;
using kpp_rt.Сотрудники.Отделы;
using kpp_rt.Сотрудники;

namespace kpp_rt
{
    public partial class SotrudForm : Form
    {
        public SotrudForm()
        {
            InitializeComponent();
        }

        public string[] arr = new string[6];
        public string[] arr_del = new string[6];
        private void SotrudForm_Load(object sender, EventArgs e)
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
            command.CommandText = @"SELECT Сотрудники.Дата_Регистрации_Сотрудника AS [Дата регистрации], ПерссональныеДанныеСотрудника.ФИО, ПерссональныеДанныеСотрудника.Номер_телефона, ПерссональныеДанныеСотрудника.Дата_Рождения, Отделы.Отдел, Должность.Должность
FROM Сотрудники 
JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника
JOIN Отделы ON Сотрудники.ID_Отдела = Отделы.ID_Отдела
JOIN Должность ON Сотрудники.ID_Должность = Должность.ID_Должность";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NewSotrudForm form = new NewSotrudForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SotrudDolzForm form = new SotrudDolzForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            OtdelSotrudForm form = new OtdelSotrudForm();
            this.Hide();
            form.Show();
        }

        private void SotrudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OperatorForm op = new OperatorForm();
            AdminForm form = new AdminForm();
            if (Properties.Settings.Default.admin_form == "Admin")
            {    
                this.Hide();
                form.Show();
            }
            else
            {
                this.Hide();
                op.Show();
            }        
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                //Редактирование строки
                for (int i = 0; i < 6; i++)
                {
                    arr[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }

                EditSotrudForm form = new EditSotrudForm();
                form.arr1 = arr;
                this.Hide();
                form.Show();
            }
            catch {
                MessageBox.Show("Ошибка!");
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
          
                for (int i = 0; i < 6; i++)
                {
                    arr_del[i] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString();
                }




                string id_pers = "";
                string id_sotrud = "";

                try
                {
                    DialogResult dialogResult = MessageBox.Show("Удалить клиента?", "Удалить", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // получить ID Персонаьные данные Сотрудника
                        SqlConnection connection = new SqlConnection(Form1.connectString);
                        SqlCommand command = new SqlCommand();

                        command.Connection = connection;
                        command.CommandText = "SELECT ID_ПерснСотрудника, ФИО, Номер_телефона, Дата_Рождения FROM ПерссональныеДанныеСотрудника WHERE ФИО='" + arr_del[1] + "' AND Номер_телефона='" + arr_del[2] + "' AND Дата_Рождения='" + arr_del[3] + "'";
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            id_pers = reader[0].ToString();
                            string FIO = reader[1].ToString();
                            string phone = reader[2].ToString();
                            string Date_roz = reader[3].ToString();
                        }
                        connection.Close();
                        // конец ID Персонаьные данные сотрудника

                        // получить ID сотрудника
                        SqlConnection connection1 = new SqlConnection(Form1.connectString);
                        SqlCommand command1 = new SqlCommand();

                        command1.Connection = connection1;
                        connection1.Open();
                        command1.CommandText = "SELECT ID_Сотрудника, ID_ПерснСотрудника, Дата_Регистрации_Сотрудника FROM Сотрудники WHERE  ID_ПерснСотрудника='" + id_pers + "' AND Дата_Регистрации_Сотрудника='" + arr_del[0] + "'";

                        SqlDataReader reader1 = command1.ExecuteReader();

                        while (reader1.Read())
                        {
                            id_sotrud = reader1[0].ToString();
                            string n1 = reader1[1].ToString();
                            string n2 = reader1[2].ToString();

                        }
                        connection1.Close();

                        //конец ID клиента
                        SqlConnection connection3 = new SqlConnection(Form1.connectString);
                        SqlCommand command3 = new SqlCommand();
                        command3.Connection = connection3;
                        connection3.Open();
                        command3.CommandText = @"DELETE FROM Сотрудники WHERE ID_Сотрудника='" + id_sotrud + "'";
                        command3.ExecuteNonQuery();
                        connection3.Close();

                        command3.Connection = connection3;
                        command3.CommandText = @"DELETE FROM ПерссональныеДанныеСотрудника WHERE ID_ПерснСотрудника='" + id_pers + "'";
                        connection3.Open();
                        command3.ExecuteNonQuery();
                        connection3.Close();

                        Class1 clas = new Class1();
                        clas.users_ychet("Удаление нового сотрудника");

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            dataGridView1.Rows.Remove(row);
                        }
                    }
                }
                catch
                {

                    MessageBox.Show("Ошибка", "Невозможно удалить!");
                }

            
        
        }
      

        }
    }

