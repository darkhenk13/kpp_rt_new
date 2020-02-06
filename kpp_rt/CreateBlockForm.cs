using kpp_rt.Карта;
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
    public partial class CreateBlockForm : Form
    {
        public CreateBlockForm()
        {
            InitializeComponent();
        }
        public string[] arr = new string[6];

        private void CreateBlockForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            id_card_text();
        }

        private void id_card_text()
        {
            textBox1.Text = arr[0];
            textBox2.Text = arr[1];

        }


        private void CreateBlockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CartForm form = new CartForm();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateBlockSotrudForm form = new CreateBlockSotrudForm();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            editcart();
        }

        private void editcart()
        {
            int block;

            try
            {


                if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || comboBox1.Text.Equals(""))
                { MessageBox.Show("Заполните все поля ввода!", "Ошибка"); }
                else
                {
                    if (comboBox1.Text == "Да")
                    {
                        block = 0;
                    }
                    else
                    {
                        block = 1;
                    }

                    string dates = DateTime.Now.ToString("dd-MM-yyyy");
                    // добавление в таблицуу персн данные клиента
                    SqlConnection conn = new SqlConnection(Form1.connectString);
                    SqlCommand cmd = new SqlCommand();
                    conn.Open(); //Устанавливаем соединение с базой данных.
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO ЗаблокированныеКарты (ID_Карты,
                                Блокировка, Дата_Блокировки)
                                        values (@ID_Карты,
                                @Блокировка, @Дата_Блокировки)";


                    cmd.Parameters.Add("@ID_Карты", SqlDbType.NVarChar);
                    cmd.Parameters["@ID_Карты"].Value = textBox1.Text;

                    cmd.Parameters.Add("@Блокировка", SqlDbType.Int);
                    cmd.Parameters["@Блокировка"].Value = block;

                    cmd.Parameters.Add("@Дата_Блокировки", SqlDbType.NVarChar);
                    cmd.Parameters["@Дата_Блокировки"].Value = dates;

                    cmd.ExecuteNonQuery();       
                    conn.Close();
                   




                    Class1 clas = new Class1();
                    clas.users_ychet("Блокировка карты сотрудника");

                    MessageBox.Show("Запись добавлена", "Выполнено");
                }
            }
            catch { MessageBox.Show("Ошибка"); }

        }



    }
}
