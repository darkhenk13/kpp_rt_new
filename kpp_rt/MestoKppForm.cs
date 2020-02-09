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
    public partial class MestoKppForm : Form
    {
        public MestoKppForm()
        {
            InitializeComponent();
        }
        public string[] arr = new string[4];
        public int k;
        public string id_object;

        private void MestoKppForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;

            if (k == 1)
            {
                textBox1.Text = arr[0] + ", " + arr[1] + ", " + arr[2] + ", " + arr[3];
            }
            else
            {
                textBox1.Text = Properties.Settings.Default.object_name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();
        }

        private void MestoKppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminMestoKppCreateForm form = new AdminMestoKppCreateForm();
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           searh_object();
            if (k == 1)
            {
                Properties.Settings.Default.object_name = arr[0] + ", " + arr[1] + ", " + arr[2] + ", " + arr[3];
                Properties.Settings.Default.id_object = id_object;
                MessageBox.Show("Настройка сохранена!", "Сохранено");
            }
            else { }
        }

        private void searh_object()
        {

            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT ID_Объекта, Город, Улица, Здание, Этаж FROM Объект WHERE Город='" + arr[0] + "' AND Улица='" + arr[1] + "' AND Здание='" + arr[2] + "'AND Этаж='" + arr[3] + "'";

              connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_object = reader[0].ToString();
                string n = reader[1].ToString();
                string n1 = reader[2].ToString();
                string n2 = reader[3].ToString();
                string n3 = reader[4].ToString();
            }
            connection.Close();

        }


    }
}
