namespace QTRS.ComponentTest
{
    partial class ComponentTestControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentTestControl));
            this.label2 = new System.Windows.Forms.Label();
            this.componentTestDataGridView = new System.Windows.Forms.DataGridView();
            this.deleteComponentTestButton = new System.Windows.Forms.Button();
            this.addComponentTestButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.componentCodeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.productAreaTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.applyPeriodCheckBox = new System.Windows.Forms.CheckBox();
            this.year1RadioButton = new System.Windows.Forms.RadioButton();
            this.month6RadioButton = new System.Windows.Forms.RadioButton();
            this.month3RadioButton = new System.Windows.Forms.RadioButton();
            this.month1RadioButton = new System.Windows.Forms.RadioButton();
            this.day15RadioButton = new System.Windows.Forms.RadioButton();
            this.day7RadioButton = new System.Windows.Forms.RadioButton();
            this.day3RadioButton = new System.Windows.Forms.RadioButton();
            this.day1RadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.endDateTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.quickReportIdx = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.quickReportName = new System.Windows.Forms.Label();
            this.componentDrugTestReportViewButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.componentTestDataGridView)).BeginInit();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("돋움", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(48, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 19);
            this.label2.TabIndex = 14;
            this.label2.Text = "원료약품 품질검사";
            // 
            // componentTestDataGridView
            // 
            this.componentTestDataGridView.AllowUserToAddRows = false;
            this.componentTestDataGridView.AllowUserToDeleteRows = false;
            this.componentTestDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.componentTestDataGridView.Location = new System.Drawing.Point(40, 230);
            this.componentTestDataGridView.Name = "componentTestDataGridView";
            this.componentTestDataGridView.RowTemplate.Height = 23;
            this.componentTestDataGridView.Size = new System.Drawing.Size(1022, 530);
            this.componentTestDataGridView.TabIndex = 21;
            this.componentTestDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.componentTestDataGridView_CellClick);
            this.componentTestDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.componentTestDataGridView_CellDoubleClick);
            // 
            // deleteComponentTestButton
            // 
            this.deleteComponentTestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.deleteComponentTestButton.FlatAppearance.BorderSize = 0;
            this.deleteComponentTestButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.deleteComponentTestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteComponentTestButton.Font = new System.Drawing.Font("돋움", 11.25F);
            this.deleteComponentTestButton.ForeColor = System.Drawing.Color.White;
            this.deleteComponentTestButton.Location = new System.Drawing.Point(976, 191);
            this.deleteComponentTestButton.Name = "deleteComponentTestButton";
            this.deleteComponentTestButton.Size = new System.Drawing.Size(86, 31);
            this.deleteComponentTestButton.TabIndex = 20;
            this.deleteComponentTestButton.Text = "삭제";
            this.deleteComponentTestButton.UseVisualStyleBackColor = false;
            this.deleteComponentTestButton.Click += new System.EventHandler(this.deleteComponentTestButton_Click);
            // 
            // addComponentTestButton
            // 
            this.addComponentTestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.addComponentTestButton.FlatAppearance.BorderSize = 0;
            this.addComponentTestButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.addComponentTestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addComponentTestButton.Font = new System.Drawing.Font("돋움", 11.25F);
            this.addComponentTestButton.ForeColor = System.Drawing.Color.White;
            this.addComponentTestButton.Location = new System.Drawing.Point(884, 191);
            this.addComponentTestButton.Name = "addComponentTestButton";
            this.addComponentTestButton.Size = new System.Drawing.Size(86, 31);
            this.addComponentTestButton.TabIndex = 19;
            this.addComponentTestButton.Text = "추가";
            this.addComponentTestButton.UseVisualStyleBackColor = false;
            this.addComponentTestButton.Click += new System.EventHandler(this.addComponentTestButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("돋움", 14.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label3.Location = new System.Drawing.Point(48, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 19);
            this.label3.TabIndex = 18;
            this.label3.Text = "원료약품 품질검사 리스트";
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.componentCodeTextBox);
            this.searchPanel.Controls.Add(this.label1);
            this.searchPanel.Controls.Add(this.productAreaTypeComboBox);
            this.searchPanel.Controls.Add(this.label7);
            this.searchPanel.Controls.Add(this.applyPeriodCheckBox);
            this.searchPanel.Controls.Add(this.year1RadioButton);
            this.searchPanel.Controls.Add(this.month6RadioButton);
            this.searchPanel.Controls.Add(this.month3RadioButton);
            this.searchPanel.Controls.Add(this.month1RadioButton);
            this.searchPanel.Controls.Add(this.day15RadioButton);
            this.searchPanel.Controls.Add(this.day7RadioButton);
            this.searchPanel.Controls.Add(this.day3RadioButton);
            this.searchPanel.Controls.Add(this.day1RadioButton);
            this.searchPanel.Controls.Add(this.panel1);
            this.searchPanel.Controls.Add(this.label8);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.endDateTimeDateTimePicker);
            this.searchPanel.Controls.Add(this.startDateTimeDateTimePicker);
            this.searchPanel.Controls.Add(this.label10);
            this.searchPanel.Location = new System.Drawing.Point(416, 40);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(646, 125);
            this.searchPanel.TabIndex = 26;
            // 
            // componentCodeTextBox
            // 
            this.componentCodeTextBox.Font = new System.Drawing.Font("돋움", 9.75F);
            this.componentCodeTextBox.Location = new System.Drawing.Point(340, 86);
            this.componentCodeTextBox.Name = "componentCodeTextBox";
            this.componentCodeTextBox.Size = new System.Drawing.Size(159, 22);
            this.componentCodeTextBox.TabIndex = 102;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.label1.Location = new System.Drawing.Point(268, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 101;
            this.label1.Text = "원료 코드";
            // 
            // productAreaTypeComboBox
            // 
            this.productAreaTypeComboBox.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.productAreaTypeComboBox.FormattingEnabled = true;
            this.productAreaTypeComboBox.Location = new System.Drawing.Point(70, 87);
            this.productAreaTypeComboBox.Name = "productAreaTypeComboBox";
            this.productAreaTypeComboBox.Size = new System.Drawing.Size(185, 21);
            this.productAreaTypeComboBox.TabIndex = 100;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.label7.Location = new System.Drawing.Point(17, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 99;
            this.label7.Text = "생산지";
            // 
            // applyPeriodCheckBox
            // 
            this.applyPeriodCheckBox.AutoSize = true;
            this.applyPeriodCheckBox.Checked = true;
            this.applyPeriodCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.applyPeriodCheckBox.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.applyPeriodCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.applyPeriodCheckBox.Location = new System.Drawing.Point(360, 15);
            this.applyPeriodCheckBox.Name = "applyPeriodCheckBox";
            this.applyPeriodCheckBox.Size = new System.Drawing.Size(95, 17);
            this.applyPeriodCheckBox.TabIndex = 98;
            this.applyPeriodCheckBox.Text = "입고일 적용";
            this.applyPeriodCheckBox.UseVisualStyleBackColor = true;
            this.applyPeriodCheckBox.CheckedChanged += new System.EventHandler(this.applyPeriodCheckBox_CheckedChanged);
            // 
            // year1RadioButton
            // 
            this.year1RadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.year1RadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(165)))), ((int)(((byte)(181)))));
            this.year1RadioButton.FlatAppearance.BorderSize = 0;
            this.year1RadioButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.year1RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.year1RadioButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.year1RadioButton.ForeColor = System.Drawing.Color.White;
            this.year1RadioButton.Location = new System.Drawing.Point(448, 48);
            this.year1RadioButton.Name = "year1RadioButton";
            this.year1RadioButton.Size = new System.Drawing.Size(51, 27);
            this.year1RadioButton.TabIndex = 93;
            this.year1RadioButton.Text = "1년";
            this.year1RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.year1RadioButton.UseVisualStyleBackColor = false;
            this.year1RadioButton.CheckedChanged += new System.EventHandler(this.periodRadioButton_CheckedChanged);
            // 
            // month6RadioButton
            // 
            this.month6RadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.month6RadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(165)))), ((int)(((byte)(181)))));
            this.month6RadioButton.FlatAppearance.BorderSize = 0;
            this.month6RadioButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.month6RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.month6RadioButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.month6RadioButton.ForeColor = System.Drawing.Color.White;
            this.month6RadioButton.Location = new System.Drawing.Point(394, 48);
            this.month6RadioButton.Name = "month6RadioButton";
            this.month6RadioButton.Size = new System.Drawing.Size(51, 27);
            this.month6RadioButton.TabIndex = 92;
            this.month6RadioButton.Text = "6개월";
            this.month6RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.month6RadioButton.UseVisualStyleBackColor = false;
            this.month6RadioButton.CheckedChanged += new System.EventHandler(this.periodRadioButton_CheckedChanged);
            // 
            // month3RadioButton
            // 
            this.month3RadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.month3RadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(165)))), ((int)(((byte)(181)))));
            this.month3RadioButton.FlatAppearance.BorderSize = 0;
            this.month3RadioButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.month3RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.month3RadioButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.month3RadioButton.ForeColor = System.Drawing.Color.White;
            this.month3RadioButton.Location = new System.Drawing.Point(340, 48);
            this.month3RadioButton.Name = "month3RadioButton";
            this.month3RadioButton.Size = new System.Drawing.Size(51, 27);
            this.month3RadioButton.TabIndex = 91;
            this.month3RadioButton.Text = "3개월";
            this.month3RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.month3RadioButton.UseVisualStyleBackColor = false;
            this.month3RadioButton.CheckedChanged += new System.EventHandler(this.periodRadioButton_CheckedChanged);
            // 
            // month1RadioButton
            // 
            this.month1RadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.month1RadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(165)))), ((int)(((byte)(181)))));
            this.month1RadioButton.FlatAppearance.BorderSize = 0;
            this.month1RadioButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.month1RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.month1RadioButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.month1RadioButton.ForeColor = System.Drawing.Color.White;
            this.month1RadioButton.Location = new System.Drawing.Point(286, 48);
            this.month1RadioButton.Name = "month1RadioButton";
            this.month1RadioButton.Size = new System.Drawing.Size(51, 27);
            this.month1RadioButton.TabIndex = 90;
            this.month1RadioButton.Text = "1개월";
            this.month1RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.month1RadioButton.UseVisualStyleBackColor = false;
            this.month1RadioButton.CheckedChanged += new System.EventHandler(this.periodRadioButton_CheckedChanged);
            // 
            // day15RadioButton
            // 
            this.day15RadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.day15RadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(165)))), ((int)(((byte)(181)))));
            this.day15RadioButton.FlatAppearance.BorderSize = 0;
            this.day15RadioButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.day15RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.day15RadioButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.day15RadioButton.ForeColor = System.Drawing.Color.White;
            this.day15RadioButton.Location = new System.Drawing.Point(232, 48);
            this.day15RadioButton.Name = "day15RadioButton";
            this.day15RadioButton.Size = new System.Drawing.Size(51, 27);
            this.day15RadioButton.TabIndex = 89;
            this.day15RadioButton.Text = "15일";
            this.day15RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.day15RadioButton.UseVisualStyleBackColor = false;
            this.day15RadioButton.CheckedChanged += new System.EventHandler(this.periodRadioButton_CheckedChanged);
            // 
            // day7RadioButton
            // 
            this.day7RadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.day7RadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(165)))), ((int)(((byte)(181)))));
            this.day7RadioButton.FlatAppearance.BorderSize = 0;
            this.day7RadioButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.day7RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.day7RadioButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.day7RadioButton.ForeColor = System.Drawing.Color.White;
            this.day7RadioButton.Location = new System.Drawing.Point(178, 48);
            this.day7RadioButton.Name = "day7RadioButton";
            this.day7RadioButton.Size = new System.Drawing.Size(51, 27);
            this.day7RadioButton.TabIndex = 88;
            this.day7RadioButton.Text = "1주";
            this.day7RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.day7RadioButton.UseVisualStyleBackColor = false;
            this.day7RadioButton.CheckedChanged += new System.EventHandler(this.periodRadioButton_CheckedChanged);
            // 
            // day3RadioButton
            // 
            this.day3RadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.day3RadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(165)))), ((int)(((byte)(181)))));
            this.day3RadioButton.FlatAppearance.BorderSize = 0;
            this.day3RadioButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.day3RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.day3RadioButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.day3RadioButton.ForeColor = System.Drawing.Color.White;
            this.day3RadioButton.Location = new System.Drawing.Point(124, 48);
            this.day3RadioButton.Name = "day3RadioButton";
            this.day3RadioButton.Size = new System.Drawing.Size(51, 27);
            this.day3RadioButton.TabIndex = 87;
            this.day3RadioButton.Text = "3일";
            this.day3RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.day3RadioButton.UseVisualStyleBackColor = false;
            this.day3RadioButton.CheckedChanged += new System.EventHandler(this.periodRadioButton_CheckedChanged);
            // 
            // day1RadioButton
            // 
            this.day1RadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.day1RadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(165)))), ((int)(((byte)(181)))));
            this.day1RadioButton.FlatAppearance.BorderSize = 0;
            this.day1RadioButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.day1RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.day1RadioButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.day1RadioButton.ForeColor = System.Drawing.Color.White;
            this.day1RadioButton.Location = new System.Drawing.Point(70, 48);
            this.day1RadioButton.Name = "day1RadioButton";
            this.day1RadioButton.Size = new System.Drawing.Size(51, 27);
            this.day1RadioButton.TabIndex = 85;
            this.day1RadioButton.Text = "1일";
            this.day1RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.day1RadioButton.UseVisualStyleBackColor = false;
            this.day1RadioButton.CheckedChanged += new System.EventHandler(this.periodRadioButton_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(126)))), ((int)(((byte)(181)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 2);
            this.panel1.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.label8.Location = new System.Drawing.Point(201, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "~";
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(177)))), ((int)(((byte)(224)))));
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("돋움", 11.25F);
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(514, 16);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(118, 95);
            this.searchButton.TabIndex = 9;
            this.searchButton.Text = "검 색";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // endDateTimeDateTimePicker
            // 
            this.endDateTimeDateTimePicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.endDateTimeDateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.endDateTimeDateTimePicker.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.endDateTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimeDateTimePicker.Location = new System.Drawing.Point(226, 13);
            this.endDateTimeDateTimePicker.Name = "endDateTimeDateTimePicker";
            this.endDateTimeDateTimePicker.Size = new System.Drawing.Size(125, 22);
            this.endDateTimeDateTimePicker.TabIndex = 3;
            this.endDateTimeDateTimePicker.ValueChanged += new System.EventHandler(this.endDateTimeDateTimePicker_ValueChanged);
            // 
            // startDateTimeDateTimePicker
            // 
            this.startDateTimeDateTimePicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.startDateTimeDateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.startDateTimeDateTimePicker.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.startDateTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimeDateTimePicker.Location = new System.Drawing.Point(70, 13);
            this.startDateTimeDateTimePicker.Name = "startDateTimeDateTimePicker";
            this.startDateTimeDateTimePicker.Size = new System.Drawing.Size(125, 22);
            this.startDateTimeDateTimePicker.TabIndex = 2;
            this.startDateTimeDateTimePicker.Value = new System.DateTime(2016, 9, 28, 7, 54, 0, 0);
            this.startDateTimeDateTimePicker.ValueChanged += new System.EventHandler(this.startDateTimeDateTimePicker_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.label10.Location = new System.Drawing.Point(16, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "입고일";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(40, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(2, 12);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(40, 196);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(2, 12);
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // quickReportIdx
            // 
            this.quickReportIdx.AutoSize = true;
            this.quickReportIdx.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.quickReportIdx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.quickReportIdx.Location = new System.Drawing.Point(546, 172);
            this.quickReportIdx.Name = "quickReportIdx";
            this.quickReportIdx.Size = new System.Drawing.Size(41, 13);
            this.quickReportIdx.TabIndex = 29;
            this.quickReportIdx.Text = "(IDX)";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(416, 175);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(2, 8);
            this.pictureBox3.TabIndex = 32;
            this.pictureBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label4.Location = new System.Drawing.Point(421, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "원료약품 선택 정보";
            // 
            // quickReportName
            // 
            this.quickReportName.AutoEllipsis = true;
            this.quickReportName.Font = new System.Drawing.Font("돋움", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.quickReportName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.quickReportName.Location = new System.Drawing.Point(422, 190);
            this.quickReportName.Name = "quickReportName";
            this.quickReportName.Size = new System.Drawing.Size(277, 35);
            this.quickReportName.TabIndex = 33;
            this.quickReportName.Text = "미 선택";
            // 
            // componentDrugTestReportViewButton
            // 
            this.componentDrugTestReportViewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.componentDrugTestReportViewButton.FlatAppearance.BorderSize = 0;
            this.componentDrugTestReportViewButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.componentDrugTestReportViewButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(219)))));
            this.componentDrugTestReportViewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.componentDrugTestReportViewButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.componentDrugTestReportViewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.componentDrugTestReportViewButton.Location = new System.Drawing.Point(715, 191);
            this.componentDrugTestReportViewButton.Name = "componentDrugTestReportViewButton";
            this.componentDrugTestReportViewButton.Size = new System.Drawing.Size(140, 31);
            this.componentDrugTestReportViewButton.TabIndex = 39;
            this.componentDrugTestReportViewButton.Text = "원료 시험 성적서";
            this.componentDrugTestReportViewButton.UseVisualStyleBackColor = false;
            this.componentDrugTestReportViewButton.Click += new System.EventHandler(this.componentDrugTestReportViewButton_Click);
            // 
            // ComponentTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.componentDrugTestReportViewButton);
            this.Controls.Add(this.quickReportName);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.quickReportIdx);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.componentTestDataGridView);
            this.Controls.Add(this.deleteComponentTestButton);
            this.Controls.Add(this.addComponentTestButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "ComponentTestControl";
            this.Size = new System.Drawing.Size(1102, 795);
            this.Load += new System.EventHandler(this.ComponentTestControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.componentTestDataGridView)).EndInit();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button deleteComponentTestButton;
        private System.Windows.Forms.Button addComponentTestButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox applyPeriodCheckBox;
        private System.Windows.Forms.RadioButton year1RadioButton;
        private System.Windows.Forms.RadioButton month6RadioButton;
        private System.Windows.Forms.RadioButton month3RadioButton;
        private System.Windows.Forms.RadioButton month1RadioButton;
        private System.Windows.Forms.RadioButton day15RadioButton;
        private System.Windows.Forms.RadioButton day7RadioButton;
        private System.Windows.Forms.RadioButton day3RadioButton;
        private System.Windows.Forms.RadioButton day1RadioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.DateTimePicker endDateTimeDateTimePicker;
        private System.Windows.Forms.DateTimePicker startDateTimeDateTimePicker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox productAreaTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox componentCodeTextBox;
        public System.Windows.Forms.DataGridView componentTestDataGridView;
        private System.Windows.Forms.Label quickReportIdx;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label quickReportName;
        private System.Windows.Forms.Button componentDrugTestReportViewButton;
    }
}
