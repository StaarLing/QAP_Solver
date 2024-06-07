namespace Main
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.GenTask = new System.Windows.Forms.ToolStripMenuItem();
            this.UpTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuAboutQAP = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.StartButton = new System.Windows.Forms.Button();
            this.checkedListBoxAlg = new System.Windows.Forms.CheckedListBox();
            this.groupBoxAlg = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textBoxTask = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkedListBoxSolve = new System.Windows.Forms.CheckedListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.groupBoxAlg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1029, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.AutoSize = false;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GenTask,
            this.UpTask});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(70, 28);
            this.toolStripDropDownButton1.Text = "Задача";
            // 
            // GenTask
            // 
            this.GenTask.Name = "GenTask";
            this.GenTask.Size = new System.Drawing.Size(248, 26);
            this.GenTask.Text = "Сгенерировать задачу";
            this.GenTask.Click += new System.EventHandler(this.GenTask_Click);
            // 
            // UpTask
            // 
            this.UpTask.Name = "UpTask";
            this.UpTask.Size = new System.Drawing.Size(248, 26);
            this.UpTask.Text = "Загрузить задачу";
            this.UpTask.Click += new System.EventHandler(this.UpTask_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.AutoSize = false;
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuAboutQAP,
            this.toolStripMenuItemHelp});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(115, 28);
            this.toolStripDropDownButton2.Text = "О программе";
            // 
            // toolStripMenuAboutQAP
            // 
            this.toolStripMenuAboutQAP.Name = "toolStripMenuAboutQAP";
            this.toolStripMenuAboutQAP.Size = new System.Drawing.Size(187, 26);
            this.toolStripMenuAboutQAP.Text = "О задаче QAP";
            this.toolStripMenuAboutQAP.Click += new System.EventHandler(this.toolStripMenuAboutQAP_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(187, 26);
            this.toolStripMenuItemHelp.Text = "Помощь";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(771, 621);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(246, 52);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Решить";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkedListBoxAlg
            // 
            this.checkedListBoxAlg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxAlg.CheckOnClick = true;
            this.checkedListBoxAlg.ColumnWidth = 50;
            this.checkedListBoxAlg.FormattingEnabled = true;
            this.checkedListBoxAlg.Items.AddRange(new object[] {
            "GWO",
            "ABC",
            "CS",
            "ALO",
            "SSO",
            "IWO",
            "ACO",
            "BFO",
            "FA",
            "CSO"});
            this.checkedListBoxAlg.Location = new System.Drawing.Point(7, 28);
            this.checkedListBoxAlg.Margin = new System.Windows.Forms.Padding(10);
            this.checkedListBoxAlg.Name = "checkedListBoxAlg";
            this.checkedListBoxAlg.Size = new System.Drawing.Size(154, 191);
            this.checkedListBoxAlg.TabIndex = 2;
            this.checkedListBoxAlg.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // groupBoxAlg
            // 
            this.groupBoxAlg.AutoSize = true;
            this.groupBoxAlg.Controls.Add(this.checkedListBoxAlg);
            this.groupBoxAlg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxAlg.Location = new System.Drawing.Point(12, 49);
            this.groupBoxAlg.Name = "groupBoxAlg";
            this.groupBoxAlg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxAlg.Size = new System.Drawing.Size(168, 235);
            this.groupBoxAlg.TabIndex = 3;
            this.groupBoxAlg.TabStop = false;
            this.groupBoxAlg.Text = "Выбор алгоритмов";
            this.groupBoxAlg.Enter += new System.EventHandler(this.groupBoxAlg_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(14, 376);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(359, 297);
            this.tabControl1.TabIndex = 5;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 290);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(185, 20);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "Выбрать все алгоритмы";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 354);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Изменить параметры алгоритмов:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox1.Location = new System.Drawing.Point(379, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(638, 510);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(186, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 75);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Режим отображения";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 48);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(66, 20);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Текст";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(90, 20);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Картинка";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // textBoxTask
            // 
            this.textBoxTask.Location = new System.Drawing.Point(431, 9);
            this.textBoxTask.Multiline = true;
            this.textBoxTask.Name = "textBoxTask";
            this.textBoxTask.ReadOnly = true;
            this.textBoxTask.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTask.Size = new System.Drawing.Size(586, 491);
            this.textBoxTask.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkedListBoxSolve);
            this.groupBox2.Location = new System.Drawing.Point(771, 525);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 90);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Вывод решения";
            // 
            // checkedListBoxSolve
            // 
            this.checkedListBoxSolve.CheckOnClick = true;
            this.checkedListBoxSolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxSolve.FormattingEnabled = true;
            this.checkedListBoxSolve.Items.AddRange(new object[] {
            "Файл (txt)",
            "Вывести график сходимости",
            "Решить полным перебором"});
            this.checkedListBoxSolve.Location = new System.Drawing.Point(8, 23);
            this.checkedListBoxSolve.Margin = new System.Windows.Forms.Padding(5);
            this.checkedListBoxSolve.Name = "checkedListBoxSolve";
            this.checkedListBoxSolve.Size = new System.Drawing.Size(230, 55);
            this.checkedListBoxSolve.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 316);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(286, 20);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Автонастройка параметров алгоритма";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Blue;
            this.progressBar1.Location = new System.Drawing.Point(379, 621);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(386, 23);
            this.progressBar1.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 599);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Выполняется алгоритм: ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(382, 525);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 46);
            this.button1.TabIndex = 14;
            this.button1.Text = "Сохранить задачу в файл";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1029, 685);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBoxTask);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBoxAlg);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QAP Solver";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxAlg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem GenTask;
        private System.Windows.Forms.ToolStripMenuItem UpTask;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAboutQAP;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBoxTask;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox checkedListBoxSolve;
        private System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.CheckedListBox checkedListBoxAlg;
        public System.Windows.Forms.GroupBox groupBoxAlg;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

