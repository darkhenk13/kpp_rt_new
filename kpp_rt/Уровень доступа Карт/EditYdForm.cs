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

namespace kpp_rt.Уровень_доступа_Карт
{
    public partial class EditYdForm : Form
    {
        public EditYdForm()
        {
            InitializeComponent();
        }
        public string[] arr1 = new string[8];
        public string[] arr_kliient = new string[8];
        public int k; 

        private void button1_Click(object sender, EventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();

        }

        private void EditYdForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();
        }

        private void EditYdForm_Load(object sender, EventArgs e)
        {
            
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            if (arr1[6] == "")
            {
                // клиент
                
                if (k == 1)
                {
                    
                    textBox1.Text = arr_kliient[1];
                }
                else
                {
                    label1.Text = "Клиент";
                    textBox1.Text = arr1[7];

                }
                
            }
            else
            {
                // сотрудник
                label1.Text = "Сотрудник";
                textBox1.Text = arr1[6];
            }
            textBox2.Text = arr1[2] + " " + arr1[3] + " " + arr1[4] + " " + arr1[5] + " " + arr1[6];


        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditYdKlientForm form = new EditYdKlientForm();
            form.arr2 = arr1;
            
            this.Hide();
            form.Show();
        }









    }
}
