﻿using System;
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

            this.MinimumSize = new System.Drawing.Size(230, 290);
            this.MaximumSize = new System.Drawing.Size(230, 290);

            this.MinimizeBox = false;
            this.MaximizeBox = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") || textBox4.Text.Equals(""))
                { MessageBox.Show("Не все поля заполенны", "Ошибка"); }
                else
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
            catch { MessageBox.Show("Ошибка"); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ObjectForm form = new ObjectForm();
            this.Hide();
            form.Show();
        }

        private void CreateObjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ObjectForm form = new ObjectForm();
            this.Hide();
            form.Show();
        }
    }
}
