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
using kpp_rt.Клиенты;
using kpp_rt.Карта;
using kpp_rt.Объект;
using System.IO.Ports;
using System.Threading;
using kpp_rt.Уровень_доступа_Карт;
using kpp_rt.Отчеты;

namespace kpp_rt
{
    public partial class OperatorForm : Form
    {
        public OperatorForm()
        {
            InitializeComponent();
        }

        private void OperatorForm_Load(object sender, EventArgs e)
        {
            //radioButton1.Checked = true;
            toolStripMenuItem1.Visible = false;
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            
            start_comport();
            BeginInvoke(new InvokeDelegate(InvokeMethod));


        }
       private void datagrid_1()
        {
          
        }

        // работа com port
        public System.IO.Ports.SerialPort sport;
        public string idcard;
        public string id_sotrud;
        public string status_sotrud;
        private Thread myThread;
        public delegate void InvokeDelegate();

        public void start_comport()
        {
            try
            {

                Properties.Settings.Default.local_city = "text";

                String port = "COM2";
                int baudrate = 9600;
                Parity parity = (Parity)Enum.Parse(typeof(Parity), "None");
                int databits = 8;
                StopBits stopbits = (StopBits)Enum.Parse(typeof(StopBits), "One");
                serialport_connect(port, baudrate, parity, databits, stopbits);
                //MessageBox.Show("Включено");
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка работы: ");            
            }
        }




        public void serialport_connect(String port, int baudrate, Parity parity, int databits, StopBits stopbits)
        {
            sport = new System.IO.Ports.SerialPort(port, baudrate, parity, databits, stopbits);
            try
            {
                sport.Open();
                sport.DataReceived += new SerialDataReceivedEventHandler(serialport_DataReceived);

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Ошибка"); }
        }


        // получение идентификатора карты с порта
        private void serialport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string idpool = sport.ReadExisting();
            //MessageBox.Show(test, "Полученные данные");
            card_pool(idpool);
        }


        // добавление сотрудника в учет посещений
        private void card_pool(string idpool)
        {
            //MessageBox.Show(idpool,"Сообщение");


            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT ID_Карты, ID_Сотрудника FROM Карта WHERE ID_Карты='" + idpool + "'";
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {

             while (reader.Read()) {
                idcard = reader[0].ToString();
                id_sotrud = reader[1].ToString();}


                
            }
            else
            {
                MessageBox.Show("Ошибка карты!");
            }
            connection.Close();

            search_status();
            sotrud_create_ych(idcard);





        }
        void search_status()
        {

            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT Статус, ID_Карты FROM УчетПосещений WHERE ID_Карты='" + idcard + "'";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                status_sotrud = reader[0].ToString();
                string id_klienta = reader[1].ToString();



            }
            connection.Close();
        }

        private void sotrud_create_ych(string idsot)
        {

            SqlConnection conn = new SqlConnection(Form1.connectString);
            SqlCommand cmd = new SqlCommand();
            string dates = DateTime.Now.ToString("dd-MM-yyyy");
            string times = DateTime.Now.ToString("HH:mm:ss");

            DateTime dt = DateTime.Now;

           

            if (status_sotrud != "true")
            {

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"INSERT INTO[УчетПосещений] (Время, Дата, Статус, ID_Карты) values (@Время, @Дата, @Статус, @ID_Карты)";

                cmd.Parameters.Add("@Время", SqlDbType.NVarChar);
                cmd.Parameters["@Время"].Value = times;

                cmd.Parameters.Add("@Дата", SqlDbType.NVarChar);
                cmd.Parameters["@Дата"].Value = "04.02.2020";
                //cmd.Parameters["@Дата"].Value = dates;

                cmd.Parameters.Add("@Статус", SqlDbType.NVarChar);
                cmd.Parameters["@Статус"].Value = "true";

                cmd.Parameters.Add("@ID_Карты", SqlDbType.NVarChar);
                cmd.Parameters["@ID_Карты"].Value = idsot;

                cmd.ExecuteNonQuery();
                conn.Close();

            }
            else
            {

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"INSERT INTO[УчетПосещений] (Время, Дата, Статус, ID_Карты) values (@Время, @Дата, @Статус, @ID_Карты)";

                cmd.Parameters.Add("@Время", SqlDbType.NVarChar);
                cmd.Parameters["@Время"].Value = times;

                cmd.Parameters.Add("@Дата", SqlDbType.NVarChar);
                //cmd.Parameters["@Дата"].Value = "04.02.2020";
                cmd.Parameters["@Дата"].Value = dates;

                cmd.Parameters.Add("@Статус", SqlDbType.NVarChar);
                cmd.Parameters["@Статус"].Value = "false";

                cmd.Parameters.Add("@ID_Карты", SqlDbType.NVarChar);
                cmd.Parameters["@ID_Карты"].Value = idsot;

                cmd.ExecuteNonQuery();
                conn.Close();
            }


            BeginInvoke(new InvokeDelegate(InvokeMethod));
        }


        private void Invoke_Click(object sender, EventArgs e)
        {
            BeginInvoke(new InvokeDelegate(InvokeMethod));
        }
        public void InvokeMethod()
        {
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            command.Connection = connection;
            command.CommandText = @"
SELECT 
	УчетПосещений.Время, 
	УчетПосещений.Дата, 
	CASE WHEN УчетПосещений.Статус='true' THEN 'Вошел' ELSE 'Вышел' END AS [Статус],
	ПерссональныеДанныеСотрудника.ФИО AS [ФИО Сотрудника],
	ПерсональныеДанныеКлиентов.ФИО AS [ФИО Клиента]
FROM УчетПосещений
LEFT JOIN Карта ON УчетПосещений.ID_Карты = Карта.ID_Карты 
LEFT JOIN Сотрудники ON Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника 
LEFT JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника 
LEFT JOIN Клиенты ON УчетПосещений.ID_Клиента = Клиенты.ID_Клиента 
LEFT JOIN ПерсональныеДанныеКлиентов ON Клиенты.ID_ПерснДанныеКлиента = ПерсональныеДанныеКлиентов.ID_ПерснДаннКлиента
ORDER BY УчетПосещений.ID_УчетПосещений DESC";
            //command.CommandText = @"SELECT * FROM УчетПосещений";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        //void status_sot()
        // {
        //     SqlConnection connection = new SqlConnection(Form1.connectString);
        //     SqlCommand command = new SqlCommand();
        //     command.Connection = connection;
        //     connection.Open();
        //     command.CommandText = "SELECT Статус, ID_Клиента FROM УчетПосещений WHERE ID_Клиента='" + id_klient + "'";
        //     SqlDataReader reader = command.ExecuteReader();

        //     while (reader.Read())
        //     {
        //         status_klienta = reader[0].ToString();
        //         string id_klienta = reader[1].ToString();



        //     }
        //     connection.Close();
        // }
        // конец работы comport

        public void InvokeSotrud()
        {
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            command.Connection = connection;
            command.CommandText = @"SELECT 
	УчетПосещений.Время, 
	УчетПосещений.Дата, 
	CASE WHEN УчетПосещений.Статус='true' THEN 'Вошел' ELSE 'Вышел' END AS [Статус],
	ПерссональныеДанныеСотрудника.ФИО AS [ФИО Сотрудника]
FROM УчетПосещений
JOIN Карта ON УчетПосещений.ID_Карты = Карта.ID_Карты 
JOIN Сотрудники ON Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника 
JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника
ORDER BY УчетПосещений.ID_УчетПосещений DESC
";
            //command.CommandText = @"SELECT * FROM УчетПосещений";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }
        public void InvokeKlient()
        {


            //WHERE date >01.01.2020 AND date < 01.02.2020
            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            command.Connection = connection;
            command.CommandText = @"SELECT 
            	УчетПосещений.Время, 
            	УчетПосещений.Дата, 
            	CASE WHEN УчетПосещений.Статус='true' THEN 'Вошел' ELSE 'Вышел' END AS [Статус],
            	ПерсональныеДанныеКлиентов.ФИО AS [ФИО Клиента]
            FROM УчетПосещений
             JOIN Клиенты ON УчетПосещений.ID_Клиента = Клиенты.ID_Клиента 
            JOIN ПерсональныеДанныеКлиентов ON Клиенты.ID_ПерснДанныеКлиента = ПерсональныеДанныеКлиентов.ID_ПерснДаннКлиента
            ORDER BY УчетПосещений.ID_УчетПосещений DESC

            ";
            //            command.CommandText = @"TRUNCATE TABLE УчетПосещений
            //";

            /*TRUNCATE TABLE developers_copy*/
            //command.CommandText = @"SELECT * FROM УчетПосещений";
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }









        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            sport.Close();
            SotrudForm sotr = new SotrudForm();
            this.Hide();
            sotr.Show();

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            sport.Close();
            KlientForm form = new KlientForm();
            this.Hide();
            form.Show();


        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            sport.Close();
            CartForm form = new CartForm();
            this.Hide();
            form.Show();

        }

        private void OperatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            sport.Close();

            Class1 clas = new Class1();
            clas.users_ychet("Выход из программы");

            Form1 form = new Form1();
            this.Hide();
            form.Show();
            


        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            sport.Close();
            ObjectForm form = new ObjectForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //serialPort1.PortName = "COM1";
            //serialPort1.BaudRate = 9600;
            //serialPort1.ReadTimeout = 500;
            //serialPort1.WriteTimeout = 500;
            //serialPort1.Open();
            //string message = serialPort1.ReadLine();
            //serialPort1.Close();
            //MessageBox.Show(message);



        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private void OperatorForm_Activated(object sender, EventArgs e)
        {
            datagrid_1();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            YrDostupForm form = new YrDostupForm();
            this.Hide();
            form.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            BeginInvoke(new InvokeDelegate(InvokeSotrud));
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            BeginInvoke(new InvokeDelegate(InvokeKlient));
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            BeginInvoke(new InvokeDelegate(InvokeMethod));
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            sport.Close();
            OtchetForm form = new OtchetForm();
            this.Hide();
            form.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SqlConnection conn2 = new SqlConnection(Form1.connectString);
            SqlCommand command2 = new SqlCommand();

            command2.Connection = conn2;
            conn2.Open();
            command2.CommandText = @"TRUNCATE TABLE УчетПосещений";

            command2.ExecuteNonQuery();

            conn2.Close();
            MessageBox.Show("Таблицы учета пользовтелей очищена!");




            //DateTime now = DateTime.Now;
            //DateTime first = new DateTime(now.Year, now.Month, 1); // первый день месяца
            //DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1); // последний день месяца




        }
    }


   



}
