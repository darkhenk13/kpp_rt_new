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
    public partial class CreateDolzSotrudForm : Form
    {
        public CreateDolzSotrudForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;


        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO[Должность] (Должность)values(@Должность)";

            cmd.Parameters.Add("@Должность", SqlDbType.NVarChar);
            cmd.Parameters["@Должность"].Value = textBox1.Text;
            cmd.ExecuteNonQuery();
            conn.Close();


            MessageBox.Show("Новая должность в таблицу Должности добавлена", "Добавление новой записи", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


            SotrudDolzForm form = new SotrudDolzForm();
            this.Close();
            form.Show();


        }

        private void CreateDolzSotrudForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SotrudDolzForm form = new SotrudDolzForm();
            this.Hide();
            form.Show();
        }

        private void CreateDolzSotrudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SotrudDolzForm form = new SotrudDolzForm();
            this.Hide();
            form.Show();
        }
    }
}
