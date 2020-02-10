using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Threading;
//using Excel = Microsoft.Office.Interop.Excel;

namespace kpp_rt.Отчеты
{
    public partial class OtchetForm : Form
    {
        public OtchetForm()
        {
            InitializeComponent();
        }


        public delegate void InvokeDelegate();

        private void OtchetForm_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            //400; 480
            this.MinimumSize = new System.Drawing.Size(350, 480);
            this.MaximumSize = new System.Drawing.Size(350, 480);
            dataGridView1.Visible = false;
            // форма по центру
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersVisible = false; // поля с левой стороны!
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //datarid_pool();

            BeginInvoke(new InvokeDelegate(file_path));
        }


        public void file_path()
        {

            string filepath = System.Environment.CurrentDirectory + "\\doc\\";
            listBox1.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(@"" + filepath + "");
            FileInfo[] files = dinfo.GetFiles();
            foreach (FileInfo filenames in files)
            {
                listBox1.Items.Add(filenames);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string bectest = System.Environment.CurrentDirectory + "\\doc";
            Process.Start("explorer", bectest);
        }
        public String resultat;
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
           resultat = listBox1.Text;
            string message = "Вы точно хотите удалить?";
            string caption = "Удаление Документа";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                deletebac();
            }
        }

        void deletebac()
        {
            string delete = System.Environment.CurrentDirectory + @"\doc\";
            string lol = listBox1.Text;
            string del = delete + @"\" + resultat;
            File.Delete(del);

            string bectest = System.Environment.CurrentDirectory + "\\doc";
            listBox1.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(@"" + bectest + "");
            FileInfo[] files = dinfo.GetFiles();
            foreach (FileInfo filenames in files)
            {

                //listBox2.Visible = true;
                listBox1.Items.Add(filenames);
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string bectest = System.Environment.CurrentDirectory + "\\doc";
            Process.Start(bectest + "\\" + listBox1.Text);
        }

        private void OtchetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OperatorForm form = new OperatorForm();
            AdminForm form1 = new AdminForm();
            if (Properties.Settings.Default.admin_form == "Admin")
            {
                this.Hide();
                form1.Show();
            }
            else 
            {
                this.Hide();
                form.Show();
            }
        }


        public void datarid_pool()
        {
            DateTime now = DateTime.Now;
            DateTime first = new DateTime(now.Year, now.Month, 1); // первый день месяца
            DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1); // последний день месяца


            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();
            command.Connection = connection;

            //command.CommandText = @"SELECT * FROM Учёт ORDER BY ID_Учёт DESC";

            //command.CommandText = @"SELECT Учёт.Время, Учёт.Дата, CASE WHEN Учёт.Статус=0 THEN 'Вышел' ELSE 'Вошел' END AS Статус FROM Учёт";


            command.CommandText = @"SELECT 
	УчетПосещений.Время, 
	FORMAT(УчетПосещений.Дата, N'dd.MM.yyyy', 'en-Us'), 
	CASE WHEN УчетПосещений.Статус='true' THEN 'Вошел' ELSE 'Вышел' END AS [Статус],
	ПерссональныеДанныеСотрудника.ФИО AS [ФИО Сотрудника],
	ПерсональныеДанныеКлиентов.ФИО AS [ФИО Клиента]
FROM УчетПосещений
LEFT JOIN Карта ON УчетПосещений.ID_Карты = Карта.ID_Карты 
LEFT JOIN Сотрудники ON Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника 
LEFT JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника 
LEFT JOIN Клиенты ON УчетПосещений.ID_Клиента = Клиенты.ID_Клиента 
LEFT JOIN ПерсональныеДанныеКлиентов ON Клиенты.ID_ПерснДанныеКлиента = ПерсональныеДанныеКлиентов.ID_ПерснДаннКлиента
WHERE УчетПосещений.Дата > '" + first + "' and УчетПосещений.Дата < '" + last + "'" + " ORDER BY УчетПосещений.ID_УчетПосещений DESC";
            //ORDER BY УчетПосещений.ID_УчетПосещений DESC
            //FORMAT(столбецДаты, N'dd.MM.yyyy', 'en-Us')
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        public void datarid_pool_sotrudniki()
        {
            DateTime now = DateTime.Now;
            DateTime first = new DateTime(now.Year, now.Month, 1); // первый день месяца
            DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1); // последний день месяца


            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();
            command.Connection = connection;

            //command.CommandText = @"SELECT * FROM Учёт ORDER BY ID_Учёт DESC";

            //command.CommandText = @"SELECT Учёт.Время, Учёт.Дата, CASE WHEN Учёт.Статус=0 THEN 'Вышел' ELSE 'Вошел' END AS Статус FROM Учёт";


            command.CommandText = @"SELECT 
	УчетПосещений.Время, 
	FORMAT(УчетПосещений.Дата, N'dd.MM.yyyy', 'en-Us'), 
	CASE WHEN УчетПосещений.Статус='true' THEN 'Вошел' ELSE 'Вышел' END AS [Статус],
	ПерссональныеДанныеСотрудника.ФИО AS [ФИО Сотрудника]
FROM УчетПосещений
JOIN Карта ON УчетПосещений.ID_Карты = Карта.ID_Карты 
JOIN Сотрудники ON Карта.ID_Сотрудника = Сотрудники.ID_Сотрудника 
JOIN ПерссональныеДанныеСотрудника ON Сотрудники.ID_ПерснСотрудника = ПерссональныеДанныеСотрудника.ID_ПерснСотрудника
WHERE УчетПосещений.Дата > '" + first + "' and УчетПосещений.Дата < '" + last + "'" + " ORDER BY УчетПосещений.ID_УчетПосещений DESC";
            //ORDER BY УчетПосещений.ID_УчетПосещений DESC
            //FORMAT(столбецДаты, N'dd.MM.yyyy', 'en-Us')
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }


        public void datarid_pool_klient()
        {
            DateTime now = DateTime.Now;
            DateTime first = new DateTime(now.Year, now.Month, 1); // первый день месяца
            DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1); // последний день месяца


            SqlConnection connection = new SqlConnection(Form1.connectString);
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();
            command.Connection = connection;
            command.CommandText = @"SELECT 
	УчетПосещений.Время, 
	FORMAT(УчетПосещений.Дата, N'dd.MM.yyyy', 'en-Us'), 
CASE WHEN УчетПосещений.Статус='true' THEN 'Вошел' ELSE 'Вышел' END AS [Статус],
            	ПерсональныеДанныеКлиентов.ФИО AS [ФИО Клиента]
            FROM УчетПосещений
             JOIN Клиенты ON УчетПосещений.ID_Клиента = Клиенты.ID_Клиента 
            JOIN ПерсональныеДанныеКлиентов ON Клиенты.ID_ПерснДанныеКлиента = ПерсональныеДанныеКлиентов.ID_ПерснДаннКлиента          
WHERE УчетПосещений.Дата > '" + first + "' and УчетПосещений.Дата < '" + last + "'" + " ORDER BY УчетПосещений.ID_УчетПосещений DESC";



            /*
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
             
             
             */
            connection.Open();
            adap.SelectCommand = command;
            adap.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
        }



        public void doc_save_all()
        {
            DateTime now = DateTime.Now;
            DateTime first = new DateTime(now.Year, now.Month, 1); // первый день месяца
            DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1); // последний день месяца
            int row_count = dataGridView1.RowCount;

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document wordDoc;
            Microsoft.Office.Interop.Word.Paragraph wordParag;
            Microsoft.Office.Interop.Word.Table wordTable;
            //создаём новый документ Word и задаём параметры листа
            //wordDoc = wordApp.Documents.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing); //создаём документ Word
            wordDoc = wordApp.Documents.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing); //создаём документ Word
            wordApp.ActiveDocument.PageSetup.BottomMargin = 40f;//задаём верхний отступ
            wordApp.ActiveDocument.PageSetup.TopMargin = 40f;// и нижний
            wordApp.ActiveDocument.PageSetup.LeftMargin = 60f;//задаём левый отступ 
            wordApp.ActiveDocument.PageSetup.RightMargin = 40f;


            
            // первый параграф
            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            //wordParag.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordParag.Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordParag.Range.Text = "                             Отчёт о посещаемости сотрудников и клиентов";
            //wordParag.Range.Text = "С " + first.Day + "." + first.Month + "." + first.Year + " по " + last.Day + "." + last.Month + "." + last.Year;

            wordParag.Range.InsertParagraphAfter();
            //wordParag.Next();


            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.Range.Text = "С " + first.Day + "." + first.Month + "." + first.Year + " по " + last.Day + "." + last.Month + "." + last.Year;
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

            // второй параграф, таблица из 10 строк и 2 колонок
            //wordDoc.Paragraphs.Add(Type.Missing);
            //wordParag.Range.Tables.Add(wordParag.Range, row_count, col_count, Type.Missing, Type.Missing);
            //wordTable = wordDoc.Tables[1];
            //wordTable.Range.Font.Bold = 1;
            //wordTable.Range.Font.Size = 12;
            //wordTable.Range.Text = "1";

            wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Tables.Add(wordParag.Range, row_count, 5, Type.Missing, Type.Missing);
            wordTable = wordDoc.Tables[1];
            wordTable.Range.Font.Bold = 0;
            wordTable.Range.Font.Size = 12;
            //wordTable.Range.Text = "1";


            Word.Border[] borders = new Word.Border[6];//массив бордеров
            borders[0] = wordTable.Borders[Word.WdBorderType.wdBorderLeft];//левая граница 
            borders[1] = wordTable.Borders[Word.WdBorderType.wdBorderRight];//правая граница 
            borders[2] = wordTable.Borders[Word.WdBorderType.wdBorderTop];//нижняя граница 
            borders[3] = wordTable.Borders[Word.WdBorderType.wdBorderBottom];//верхняя граница
            borders[4] = wordTable.Borders[Word.WdBorderType.wdBorderHorizontal];//горизонтальная граница
            borders[5] = wordTable.Borders[Word.WdBorderType.wdBorderVertical];//вертикальная граница
            foreach (Word.Border border in borders)
            {
                border.LineStyle = Word.WdLineStyle.wdLineStyleSingle;//ставим стиль границы 
                border.Color = Word.WdColor.wdColorBlack;//задаем цвет границы
            }


            //задаём ширину колонок и высоту строк
            wordTable.Columns.PreferredWidthType = Microsoft.Office.Interop.Word.WdPreferredWidthType.wdPreferredWidthPoints;
            wordTable.Columns[1].SetWidth(80f, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);

            wordTable.Rows.SetHeight(40f, Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly);
            //wordTable.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowRight;
            wordTable.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;
            wordTable.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //wordTable.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalTop;

            wordTable.Range.Select();



            int rows = dataGridView1.RowCount + 1;
            int columns = 5;

            wordDoc.Tables[1].Cell(1, 1).Range.Text = "Время";
            //wordDoc.Tables[1].Columns.Width = 50;
            wordDoc.Tables[1].Cell(1, 1).Column.Width = 55;
            wordDoc.Tables[1].Cell(1, 2).Range.Text = "Дата";
            wordDoc.Tables[1].Cell(1, 2).Column.Width = 70;
            wordDoc.Tables[1].Cell(1, 3).Range.Text = "Статус";
            wordDoc.Tables[1].Cell(1, 3).Column.Width = 55;         
            wordDoc.Tables[1].Cell(1, 4).Range.Text = "ФИО Сотрудника";
            wordDoc.Tables[1].Cell(1, 4).Column.Width = 150;
            wordDoc.Tables[1].Cell(1, 4).Row.Height = 70;
           //wordDoc.Tables[1].Cell(1, 4).Row.Height = 20;
           wordDoc.Tables[1].Cell(1, 5).Range.Text = "ФИО Клиента";
            wordDoc.Tables[1].Cell(1, 5).Column.Width = 150;
      
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    //Item(i).Rows.Item(j).Height;
                    //wordDoc.Tables[1].Cell(i + 2, j + 1).Height = 20;
                    //wordDoc.Tables[1].Cell(i + 2, j + 1).Row.Height = 20;
                    wordDoc.Tables[1].Cell(i + 2, j + 1).Range.Text = dataGridView1[j, i].Value.ToString();
                            
                }
            }

            string fff = "13 мая 20120 год";
            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceDouble;
            wordParag.Range.Text = "Дата: " + fff + "";
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            string datas12 = DateTime.Now.ToString("dd MMMM yyyy");
            //string fff1 = "13 мая 2016 год";
            //wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            //wordParag.Range.Font.Name = "Times New Roman";
            //wordParag.Range.Font.Size = 16;
            //wordParag.Range.Font.Bold = 0;
            //wordParag.Range.Text = "Дата: " + datas12 + "";
            //wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceDouble;
            wordParag.Range.Text = "   ";
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;


            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceDouble;
            
            wordParag.Range.Text = " Дата: " + datas12 + "                      Подпись____________";
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;


            //сохраняем документ, закрываем документ, выходим из Word

            string bectest = System.Environment.CurrentDirectory + "\\doc";
            string naz = "Отчёт Учёт Сотрудников и Клиентов "; // Название документа
            wordDoc.SaveAs(bectest + "\\" + naz + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".doc"); // путь при создании документа, и установка даты создания
            //+bectest + "\\" + "base" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'"
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
            //System.Diagnostics.Process.Start(@"E:\\5.doc");
            MessageBox.Show("Отчет сохранен");
            //Process.Start("E:\\7.doc");
            BeginInvoke(new InvokeDelegate(file_path));

        }
        
        
        /// <summary>
        /// Выгрузка по сотрудникам
        /// </summary>
        public void doc_save_sotrud()
        {
            DateTime now = DateTime.Now;
            DateTime first = new DateTime(now.Year, now.Month, 1); // первый день месяца
            DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1); // последний день месяца
            int row_count = dataGridView1.RowCount;

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document wordDoc;
            Microsoft.Office.Interop.Word.Paragraph wordParag;
            Microsoft.Office.Interop.Word.Table wordTable;
            //создаём новый документ Word и задаём параметры листа
            //wordDoc = wordApp.Documents.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing); //создаём документ Word
            wordDoc = wordApp.Documents.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing); //создаём документ Word
            wordApp.ActiveDocument.PageSetup.BottomMargin = 40f;//задаём верхний отступ
            wordApp.ActiveDocument.PageSetup.TopMargin = 40f;// и нижний
            wordApp.ActiveDocument.PageSetup.LeftMargin = 60f;//задаём левый отступ 
            wordApp.ActiveDocument.PageSetup.RightMargin = 40f;



            // первый параграф
            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            //wordParag.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordParag.Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordParag.Range.Text = "                                        Отчёт о посещаемости сотрудникам";
            //wordParag.Range.Text = "С " + first.Day + "." + first.Month + "." + first.Year + " по " + last.Day + "." + last.Month + "." + last.Year;

            wordParag.Range.InsertParagraphAfter();
            //wordParag.Next();


            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.Range.Text = "С " + first.Day + "." + first.Month + "." + first.Year + " по " + last.Day + "." + last.Month + "." + last.Year;
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

            // второй параграф, таблица из 10 строк и 2 колонок
            //wordDoc.Paragraphs.Add(Type.Missing);
            //wordParag.Range.Tables.Add(wordParag.Range, row_count, col_count, Type.Missing, Type.Missing);
            //wordTable = wordDoc.Tables[1];
            //wordTable.Range.Font.Bold = 1;
            //wordTable.Range.Font.Size = 12;
            //wordTable.Range.Text = "1";

            wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Tables.Add(wordParag.Range, row_count, 4, Type.Missing, Type.Missing);
            wordTable = wordDoc.Tables[1];
            wordTable.Range.Font.Bold = 0;
            wordTable.Range.Font.Size = 12;
            //wordTable.Range.Text = "1";


            Word.Border[] borders = new Word.Border[6];//массив бордеров
            borders[0] = wordTable.Borders[Word.WdBorderType.wdBorderLeft];//левая граница 
            borders[1] = wordTable.Borders[Word.WdBorderType.wdBorderRight];//правая граница 
            borders[2] = wordTable.Borders[Word.WdBorderType.wdBorderTop];//нижняя граница 
            borders[3] = wordTable.Borders[Word.WdBorderType.wdBorderBottom];//верхняя граница
            borders[4] = wordTable.Borders[Word.WdBorderType.wdBorderHorizontal];//горизонтальная граница
            borders[5] = wordTable.Borders[Word.WdBorderType.wdBorderVertical];//вертикальная граница
            foreach (Word.Border border in borders)
            {
                border.LineStyle = Word.WdLineStyle.wdLineStyleSingle;//ставим стиль границы 
                border.Color = Word.WdColor.wdColorBlack;//задаем цвет границы
            }


            //задаём ширину колонок и высоту строк
            wordTable.Columns.PreferredWidthType = Microsoft.Office.Interop.Word.WdPreferredWidthType.wdPreferredWidthPoints;
            wordTable.Columns[1].SetWidth(80f, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);

            wordTable.Rows.SetHeight(40f, Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly);
            //wordTable.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowRight;
            wordTable.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;
            wordTable.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //wordTable.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalTop;

            wordTable.Range.Select();



            int rows = dataGridView1.RowCount + 1;
            int columns = 4;

            wordDoc.Tables[1].Cell(1, 1).Range.Text = "Время";
            //wordDoc.Tables[1].Columns.Width = 50;
            wordDoc.Tables[1].Cell(1, 1).Column.Width = 55;
            wordDoc.Tables[1].Cell(1, 2).Range.Text = "Дата";
            wordDoc.Tables[1].Cell(1, 2).Column.Width = 70;
            wordDoc.Tables[1].Cell(1, 3).Range.Text = "Статус";
            wordDoc.Tables[1].Cell(1, 3).Column.Width = 55;
            wordDoc.Tables[1].Cell(1, 4).Range.Text = "ФИО Сотрудника";
            wordDoc.Tables[1].Cell(1, 4).Column.Width = 150;
            wordDoc.Tables[1].Cell(1, 4).Row.Height = 70;
            //wordDoc.Tables[1].Cell(1, 4).Row.Height = 20;
            //wordDoc.Tables[1].Cell(1, 5).Range.Text = "ФИО Клиента";
            //wordDoc.Tables[1].Cell(1, 5).Column.Width = 150;

            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    wordDoc.Tables[1].Cell(i + 2, j + 1).Range.Text = dataGridView1[j, i].Value.ToString();

                }
            }

            string fff = "13 мая 20120 год";
            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceDouble;
            wordParag.Range.Text = "Дата: " + fff + "";
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            string datas12 = DateTime.Now.ToString("dd MMMM yyyy");
            //string fff1 = "13 мая 2016 год";
            //wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            //wordParag.Range.Font.Name = "Times New Roman";
            //wordParag.Range.Font.Size = 16;
            //wordParag.Range.Font.Bold = 0;
            //wordParag.Range.Text = "Дата: " + datas12 + "";
            //wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceDouble;
            wordParag.Range.Text = "   ";
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

            wordParag.Range.InsertParagraphAfter();
            wordParag.Next();

            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceDouble;

            wordParag.Range.Text = " Дата: " + datas12 + "                      Подпись____________";
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;


            //сохраняем документ, закрываем документ, выходим из Word

            string bectest = System.Environment.CurrentDirectory + "\\doc";
            string naz = "Отчёт Учёт Сотрудников"; // Название документа
            wordDoc.SaveAs(bectest + "\\" + naz + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".doc"); // путь при создании документа, и установка даты создания
            //+bectest + "\\" + "base" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'"
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
            //System.Diagnostics.Process.Start(@"E:\\5.doc");
            MessageBox.Show("Отчет сохранен");
            //Process.Start("E:\\7.doc");
            BeginInvoke(new InvokeDelegate(file_path));
        }
        /// <summary>
        /// Конец выгрузки по сотрудникам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void doc_save_klient()
        {
            DateTime now = DateTime.Now;
            DateTime first = new DateTime(now.Year, now.Month, 1); // первый день месяца
            DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1); // последний день месяца
            int row_count = dataGridView1.RowCount;

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document wordDoc;
            Microsoft.Office.Interop.Word.Paragraph wordParag;
            Microsoft.Office.Interop.Word.Table wordTable;
            //создаём новый документ Word и задаём параметры листа
            //wordDoc = wordApp.Documents.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing); //создаём документ Word
            wordDoc = wordApp.Documents.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing); //создаём документ Word
            wordApp.ActiveDocument.PageSetup.BottomMargin = 40f;//задаём верхний отступ
            wordApp.ActiveDocument.PageSetup.TopMargin = 40f;// и нижний
            wordApp.ActiveDocument.PageSetup.LeftMargin = 60f;//задаём левый отступ 
            wordApp.ActiveDocument.PageSetup.RightMargin = 40f;



            // первый параграф
            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            //wordParag.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordParag.Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordParag.Range.Text = "                                                                                            Отчёт о посещаемости Клиентов";
            //wordParag.Range.Text = "С " + first.Day + "." + first.Month + "." + first.Year + " по " + last.Day + "." + last.Month + "." + last.Year;

            wordParag.Range.InsertParagraphAfter();
            //wordParag.Next();


            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.Range.Text = "С " + first.Day + "." + first.Month + "." + first.Year + " по " + last.Day + "." + last.Month + "." + last.Year;
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

            // второй параграф, таблица из 10 строк и 2 колонок
            //wordDoc.Paragraphs.Add(Type.Missing);
            //wordParag.Range.Tables.Add(wordParag.Range, row_count, col_count, Type.Missing, Type.Missing);
            //wordTable = wordDoc.Tables[1];
            //wordTable.Range.Font.Bold = 1;
            //wordTable.Range.Font.Size = 12;
            //wordTable.Range.Text = "1";

            wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Tables.Add(wordParag.Range, row_count, 4, Type.Missing, Type.Missing);
            wordTable = wordDoc.Tables[1];
            wordTable.Range.Font.Bold = 0;
            wordTable.Range.Font.Size = 12;
            //wordTable.Range.Text = "1";


            Word.Border[] borders = new Word.Border[6];//массив бордеров
            borders[0] = wordTable.Borders[Word.WdBorderType.wdBorderLeft];//левая граница 
            borders[1] = wordTable.Borders[Word.WdBorderType.wdBorderRight];//правая граница 
            borders[2] = wordTable.Borders[Word.WdBorderType.wdBorderTop];//нижняя граница 
            borders[3] = wordTable.Borders[Word.WdBorderType.wdBorderBottom];//верхняя граница
            borders[4] = wordTable.Borders[Word.WdBorderType.wdBorderHorizontal];//горизонтальная граница
            borders[5] = wordTable.Borders[Word.WdBorderType.wdBorderVertical];//вертикальная граница
            foreach (Word.Border border in borders)
            {
                border.LineStyle = Word.WdLineStyle.wdLineStyleSingle;//ставим стиль границы 
                border.Color = Word.WdColor.wdColorBlack;//задаем цвет границы
            }


            //задаём ширину колонок и высоту строк
            wordTable.Columns.PreferredWidthType = Microsoft.Office.Interop.Word.WdPreferredWidthType.wdPreferredWidthPoints;
            wordTable.Columns[1].SetWidth(80f, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);

            wordTable.Rows.SetHeight(40f, Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly);
            //wordTable.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowRight;
            wordTable.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;
            wordTable.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //wordTable.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalTop;

            wordTable.Range.Select();



            int rows = dataGridView1.RowCount + 1;
            int columns = 4;

            wordDoc.Tables[1].Cell(1, 1).Range.Text = "Время";
            //wordDoc.Tables[1].Columns.Width = 50;
            wordDoc.Tables[1].Cell(1, 1).Column.Width = 55;
            wordDoc.Tables[1].Cell(1, 2).Range.Text = "Дата";
            wordDoc.Tables[1].Cell(1, 2).Column.Width = 70;
            wordDoc.Tables[1].Cell(1, 3).Range.Text = "Статус";
            wordDoc.Tables[1].Cell(1, 3).Column.Width = 55;
            wordDoc.Tables[1].Cell(1, 4).Range.Text = "ФИО Клиента";
            wordDoc.Tables[1].Cell(1, 4).Column.Width = 150;
            wordDoc.Tables[1].Cell(1, 4).Row.Height = 70;
            //wordDoc.Tables[1].Cell(1, 4).Row.Height = 20;
            //wordDoc.Tables[1].Cell(1, 5).Range.Text = "ФИО Клиента";
            //wordDoc.Tables[1].Cell(1, 5).Column.Width = 150;

            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    wordDoc.Tables[1].Cell(i + 2, j + 1).Range.Text = dataGridView1[j, i].Value.ToString();

                }
            }

            string fff = "13 мая 20120 год";
            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceDouble;
            wordParag.Range.Text = "Дата: " + fff + "";
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            string datas12 = DateTime.Now.ToString("dd MMMM yyyy");
            //string fff1 = "13 мая 2016 год";
            //wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            //wordParag.Range.Font.Name = "Times New Roman";
            //wordParag.Range.Font.Size = 16;
            //wordParag.Range.Font.Bold = 0;
            //wordParag.Range.Text = "Дата: " + datas12 + "";
            //wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceDouble;
            wordParag.Range.Text = "   ";
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

            wordParag.Range.InsertParagraphAfter();
            wordParag.Next();

            wordParag = wordDoc.Paragraphs.Add(Type.Missing);
            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Font.Bold = 0;
            wordParag.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceDouble;

            wordParag.Range.Text = " Дата: " + datas12 + "                      Подпись____________";
            wordParag.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;


            //сохраняем документ, закрываем документ, выходим из Word

            string bectest = System.Environment.CurrentDirectory + "\\doc";
            string naz = "Отчёт Учёт Клиентов"; // Название документа
            wordDoc.SaveAs(bectest + "\\" + naz + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".doc"); // путь при создании документа, и установка даты создания
            //+bectest + "\\" + "base" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'"
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
            //System.Diagnostics.Process.Start(@"E:\\5.doc");
            MessageBox.Show("Отчет сохранен");
            //Process.Start("E:\\7.doc");
            BeginInvoke(new InvokeDelegate(file_path));
        }




        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsCount = dataGridView1.Rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
                }
                dataGridView1.DataSource = null;

                datarid_pool();
                Thread myThread = new Thread(new ThreadStart(doc_save_all));
                myThread.Start();
                //doc_save_all();
               
                Class1 clas = new Class1();
                clas.users_ychet("Формирование отчета по сотрудникам и клиентам");
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string bectest = System.Environment.CurrentDirectory + "\\doc";
            Process.Start(bectest + "\\" + listBox1.Text);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsCount = dataGridView1.Rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
                }
                dataGridView1.DataSource = null;

                datarid_pool_sotrudniki();
                Thread myThread = new Thread(new ThreadStart(doc_save_sotrud));
                myThread.Start();
                //doc_save_all();

                Class1 clas = new Class1();
                clas.users_ychet("Формирование отчета по сотрудникам");
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsCount = dataGridView1.Rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
                }
                dataGridView1.DataSource = null;

                datarid_pool_klient();
                Thread myThread = new Thread(new ThreadStart(doc_save_sotrud));
                myThread.Start();
                //doc_save_all();

                Class1 clas = new Class1();
                clas.users_ychet("Формирование отчета по клиентам");
            }
            catch { MessageBox.Show("Ошибка"); }



        }
    }
}
