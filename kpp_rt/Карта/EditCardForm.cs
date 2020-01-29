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

namespace kpp_rt.Карта
{
    public partial class EditCardForm : Form
    {
        public EditCardForm()
        {
            InitializeComponent();
        }

        public string[] edit_sotrud1 = new string[4];



        private void button1_Click(object sender, EventArgs e)
        {
            CartForm form = new CartForm();
            this.Hide();
            form.Show();
        }

        private void EditCardForm_Load(object sender, EventArgs e)
        {
            
            textBox2.Enabled = false;



           // SqlConnection connection = new SqlConnection(Form1.connectString);
           // SqlCommand command = new SqlCommand();

           // command.Connection = connection;
           // command.CommandText = "SELECT ID_ПерснДаннКлиента, ФИО, Номер_Паспорта, Дата_Рождения FROM ПерсональныеДанныеКлиентов WHERE ФИО='" + arr1[1] + "' AND Номер_Паспорта='" + arr1[2] + "' AND Дата_Рождения='" + arr1[3] + "'";

           // connection.Open();

           // SqlDataReader reader = command.ExecuteReader();

           // while (reader.Read())
           // {
           //    // id_pers = reader[0].ToString();
           //     textBox1.Text = reader[1].ToString();
           //     textBox2.Text = reader[2].ToString();
           //     //textBox3.Text = reader[3].ToString();
           // }
           // connection.Close();
           //// textBox4.Text = arr1[0];



        }

        private void EditCardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CartForm form = new CartForm();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SotrudForm form = new SotrudForm();
            this.Hide();
            form.Show();

        }
    }
}
