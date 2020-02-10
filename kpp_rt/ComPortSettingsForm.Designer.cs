namespace kpp_rt
{
    partial class ComPortSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comPort_com = new System.Windows.Forms.ComboBox();
            this.BaudRate_com = new System.Windows.Forms.ComboBox();
            this.Parity_com = new System.Windows.Forms.ComboBox();
            this.Databits_com = new System.Windows.Forms.ComboBox();
            this.Stopbits_com = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(352, 151);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comPort_com
            // 
            this.comPort_com.FormattingEnabled = true;
            this.comPort_com.Location = new System.Drawing.Point(306, 53);
            this.comPort_com.Name = "comPort_com";
            this.comPort_com.Size = new System.Drawing.Size(121, 21);
            this.comPort_com.TabIndex = 2;
            // 
            // BaudRate_com
            // 
            this.BaudRate_com.FormattingEnabled = true;
            this.BaudRate_com.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200"});
            this.BaudRate_com.Location = new System.Drawing.Point(12, 55);
            this.BaudRate_com.Name = "BaudRate_com";
            this.BaudRate_com.Size = new System.Drawing.Size(121, 21);
            this.BaudRate_com.TabIndex = 3;
            // 
            // Parity_com
            // 
            this.Parity_com.FormattingEnabled = true;
            this.Parity_com.Items.AddRange(new object[] {
            "Even",
            "Odd",
            "None",
            "Mark",
            "Space"});
            this.Parity_com.Location = new System.Drawing.Point(12, 109);
            this.Parity_com.Name = "Parity_com";
            this.Parity_com.Size = new System.Drawing.Size(121, 21);
            this.Parity_com.TabIndex = 4;
            // 
            // Databits_com
            // 
            this.Databits_com.AutoCompleteCustomSource.AddRange(new string[] {
            ""});
            this.Databits_com.FormattingEnabled = true;
            this.Databits_com.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.Databits_com.Location = new System.Drawing.Point(156, 55);
            this.Databits_com.Name = "Databits_com";
            this.Databits_com.Size = new System.Drawing.Size(121, 21);
            this.Databits_com.TabIndex = 5;
            // 
            // Stopbits_com
            // 
            this.Stopbits_com.FormattingEnabled = true;
            this.Stopbits_com.Items.AddRange(new object[] {
            "None",
            "One",
            "OnePoint",
            "Two"});
            this.Stopbits_com.Location = new System.Drawing.Point(156, 109);
            this.Stopbits_com.Name = "Stopbits_com";
            this.Stopbits_com.Size = new System.Drawing.Size(121, 21);
            this.Stopbits_com.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Baud Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Parity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Databits";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Stopbits";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(310, 111);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "Включить";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ComPortSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 181);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Stopbits_com);
            this.Controls.Add(this.Databits_com);
            this.Controls.Add(this.Parity_com);
            this.Controls.Add(this.BaudRate_com);
            this.Controls.Add(this.comPort_com);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ComPortSettingsForm";
            this.Text = "Настройка Com Порта";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComPortSettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.ComPortSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comPort_com;
        private System.Windows.Forms.ComboBox BaudRate_com;
        private System.Windows.Forms.ComboBox Parity_com;
        private System.Windows.Forms.ComboBox Databits_com;
        private System.Windows.Forms.ComboBox Stopbits_com;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}