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
    public partial class CreateObjectForm : Form
    {
        public CreateObjectForm()
        {
            InitializeComponent();
        }

        string connectString = ConfigurationManager.ConnectionStrings["SqlBD"].ConnectionString;


        private void CreateObjectForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO [Объект] (Город, Улица, Здание, Этаж)values(@Город, @Улица, @Здание, @Этаж)";

            cmd.Parameters.Add("@Город", SqlDbType.NVarChar);
            cmd.Parameters["@Город"].Value = textBox1.Text;

            cmd.Parameters.Add("@Улица", SqlDbType.NVarChar);
            cmd.Parameters["@Улица"].Value = textBox2.Text;

            cmd.Parameters.Add("@Здание", SqlDbType.NVarChar);
            cmd.Parameters["@Здание"].Value = textBox3.Text;

            cmd.Parameters.Add("@Этаж", SqlDbType.NVarChar);
            cmd.Parameters["@Этаж"].Value = textBox4.Text;


            cmd.ExecuteNonQuery();
            conn.Close();


            MessageBox.Show("Новая запись добавлена!", "Добавление новой записи", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            Class1 clas = new Class1();
            clas.users_ychet("Добавлене нового объекта");

            ObjectForm form = new ObjectForm();
            this.Hide();
            form.Show();


        }
    }
}
