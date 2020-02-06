using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace kpp_rt
{
    public partial class ComPortSettingsForm : Form
    {
        public ComPortSettingsForm()
        {
            InitializeComponent();

            foreach (String s in System.IO.Ports.SerialPort.GetPortNames())
            {
                comPort_com.Items.Add(s);
            }

        }

        private void ComPortSettingsForm_Load(object sender, EventArgs e)
        {
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            status_check();

            if (Properties.Settings.Default.save_com != "0")
            {
                BaudRate_com.Text = Properties.Settings.Default.baudrate;
                Databits_com.Text = Properties.Settings.Default.databits;
                comPort_com.Text = Properties.Settings.Default.port;
                Parity_com.Text = Properties.Settings.Default.parity;
                Stopbits_com.Text = Properties.Settings.Default.stopbits;
            }
            else
            {
                comPort_com.Text = "COM1";
            }

            BaudRate_com.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Databits_com.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comPort_com.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Parity_com.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Stopbits_com.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        }

        public void status_check()
        {
            if (Properties.Settings.Default.status_comport != "0")
            {
                checkBox1.Checked = true;
                checkBox1.Text = "Выключить";
                BaudRate_com.Enabled = true;
                Databits_com.Enabled = true;
                comPort_com.Enabled = true;
                Parity_com.Enabled = true;
                Stopbits_com.Enabled = true;
               
            }
            else
            {

                checkBox1.Checked = false;
                checkBox1.Text = "Включить";
                BaudRate_com.Enabled = false;
                Databits_com.Enabled = false;
                comPort_com.Enabled = false;
                Parity_com.Enabled = false;
                Stopbits_com.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string status = "";
            if (checkBox1.Checked != true)
            {
                Properties.Settings.Default.status_comport = "0";
            }
            else
            {
                Properties.Settings.Default.status_comport = "1";
            }
            
            Properties.Settings.Default.save_com = "1";
            Properties.Settings.Default.baudrate = BaudRate_com.Text;
            Properties.Settings.Default.databits = Databits_com.Text;
            Properties.Settings.Default.port = comPort_com.Text;
            Properties.Settings.Default.parity = Parity_com.Text;
            Properties.Settings.Default.stopbits = Stopbits_com.Text;
           
            Properties.Settings.Default.Save();

            status_check();
            MessageBox.Show("Настройки сохранены!", "Настройки");
            Class1 clas = new Class1();
            clas.users_ychet("Применение настроек Com port");

        }

        private void ComPortSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminForm form = new AdminForm();
            this.Hide();
            form.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
