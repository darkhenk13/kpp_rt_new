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

namespace kpp_rt.Сотрудники.Отделы
{
    public partial class CreateOtdelSotrudForm : Form
    {
        public CreateOtdelSotrudForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Заполните поле ввода","Ошибка");
            }
            else
            {
                SqlConnection conn = new SqlConnection(connectString);
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"INSERT INTO [Отделы] (Отдел)values(@Отдел)";

                cmd.Parameters.Add("@Отдел", SqlDbType.NVarChar);
                cmd.Parameters["@Отдел"].Value = textBox1.Text;
                cmd.ExecuteNonQuery();
                conn.Close();


                MessageBox.Show("Новыый отдел в таблицу Отделы добавлена", "Добавление новой записи", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Class1 clas = new Class1();
                clas.users_ychet("Добавлене нового отдела");

                OtdelSotrudForm form = new OtdelSotrudForm();
                this.Hide();
                form.Show();
            }
        }

        private void CreateOtdelSotrudForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void CreateOtdelSotrudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OtdelSotrudForm form = new OtdelSotrudForm();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OtdelSotrudForm form = new OtdelSotrudForm();
            this.Hide();
            form.Show();
        }
    }
}
