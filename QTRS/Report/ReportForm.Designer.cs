namespace QTRS.Report
{
    partial class ReportForm
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
            this.components = new System.ComponentModel.Container();
            this.ProductQtTestDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ProductQtTestDataSource = new QTRS.Report.ProductQtTestDataSource();
            this.productQtTestHeaderDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ComponentDrugTestHeaderDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ComponentDrugTestDataSource = new QTRS.Report.ComponentDrugTestDataSource();
            this.ComponentDrugTestContentDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ProductMfTestHeaderDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ProductMfTestDataSource = new QTRS.Report.ProductMfTestDataSource();
            this.ProductMfTestContentDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.topPanel = new System.Windows.Forms.Panel();
            this.lotModifySaveButton = new System.Windows.Forms.Button();
            this.testCompanyName = new System.Windows.Forms.ComboBox();
            this.testDescWherecheckbox = new System.Windows.Forms.CheckBox();
            this.testProductTextCheckbox = new System.Windows.Forms.CheckBox();
            this.testProductTextBox = new System.Windows.Forms.TextBox();
            this.testMcComboBox = new System.Windows.Forms.ComboBox();
            this.textMcnumLabel = new System.Windows.Forms.Label();
            this.saveReportButton = new System.Windows.Forms.Button();
            this.disapprovalTestCheckBox = new System.Windows.Forms.CheckBox();
            this.testDescLabel = new System.Windows.Forms.Label();
            this.searchTestButton = new System.Windows.Forms.Button();
            this.applyPeriodCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.endDateTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.createReportButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.testIdComboBox = new System.Windows.Forms.ComboBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.QTRSFlexViewer = new C1.Win.FlexViewer.C1FlexViewer();
            this.FinalProductQtTestDataSource = new QTRS.Report.FinalProductQtTestDataSource();
            ((System.ComponentModel.ISupportInitialize)(this.ProductQtTestDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductQtTestDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productQtTestHeaderDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestHeaderDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestContentDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestHeaderDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestContentDataSetBindingSource)).BeginInit();
            this.topPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QTRSFlexViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinalProductQtTestDataSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductQtTestDataSetBindingSource
            // 
            this.ProductQtTestDataSetBindingSource.DataMember = "ProductQtTestDataSet";
            this.ProductQtTestDataSetBindingSource.DataSource = this.ProductQtTestDataSource;
            // 
            // ProductQtTestDataSource
            // 
            this.ProductQtTestDataSource.DataSetName = "ProductQtTestDataSource";
            this.ProductQtTestDataSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productQtTestHeaderDataSetBindingSource
            // 
            this.productQtTestHeaderDataSetBindingSource.DataMember = "ProductQtTestHeaderDataSet";
            this.productQtTestHeaderDataSetBindingSource.DataSource = this.ProductQtTestDataSource;
            // 
            // ComponentDrugTestHeaderDataSetBindingSource
            // 
            this.ComponentDrugTestHeaderDataSetBindingSource.DataMember = "ComponentDrugTestHeaderDataSet";
            this.ComponentDrugTestHeaderDataSetBindingSource.DataSource = this.ComponentDrugTestDataSource;
            // 
            // ComponentDrugTestDataSource
            // 
            this.ComponentDrugTestDataSource.DataSetName = "ComponentDrugTestDataSource";
            this.ComponentDrugTestDataSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ComponentDrugTestContentDataSetBindingSource
            // 
            this.ComponentDrugTestContentDataSetBindingSource.DataMember = "ComponentDrugTestContentDataSet";
            this.ComponentDrugTestContentDataSetBindingSource.DataSource = this.ComponentDrugTestDataSource;
            // 
            // ProductMfTestHeaderDataSetBindingSource
            // 
            this.ProductMfTestHeaderDataSetBindingSource.DataMember = "ProductMfTestHeaderDataSet";
            this.ProductMfTestHeaderDataSetBindingSource.DataSource = this.ProductMfTestDataSource;
            // 
            // ProductMfTestDataSource
            // 
            this.ProductMfTestDataSource.DataSetName = "ProductMfTestDataSource";
            this.ProductMfTestDataSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ProductMfTestContentDataSetBindingSource
            // 
            this.ProductMfTestContentDataSetBindingSource.DataMember = "ProductMfTestContentDataSet";
            this.ProductMfTestContentDataSetBindingSource.DataSource = this.ProductMfTestDataSource;
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.lotModifySaveButton);
            this.topPanel.Controls.Add(this.testCompanyName);
            this.topPanel.Controls.Add(this.testDescWherecheckbox);
            this.topPanel.Controls.Add(this.testProductTextCheckbox);
            this.topPanel.Controls.Add(this.testProductTextBox);
            this.topPanel.Controls.Add(this.testMcComboBox);
            this.topPanel.Controls.Add(this.textMcnumLabel);
            this.topPanel.Controls.Add(this.saveReportButton);
            this.topPanel.Controls.Add(this.disapprovalTestCheckBox);
            this.topPanel.Controls.Add(this.testDescLabel);
            this.topPanel.Controls.Add(this.searchTestButton);
            this.topPanel.Controls.Add(this.applyPeriodCheckBox);
            this.topPanel.Controls.Add(this.label8);
            this.topPanel.Controls.Add(this.endDateTimeDateTimePicker);
            this.topPanel.Controls.Add(this.startDateTimeDateTimePicker);
            this.topPanel.Controls.Add(this.label2);
            this.topPanel.Controls.Add(this.createReportButton);
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.testIdComboBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1584, 128);
            this.topPanel.TabIndex = 23;
            // 
            // lotModifySaveButton
            // 
            this.lotModifySaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.lotModifySaveButton.FlatAppearance.BorderSize = 0;
            this.lotModifySaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lotModifySaveButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lotModifySaveButton.ForeColor = System.Drawing.Color.White;
            this.lotModifySaveButton.Location = new System.Drawing.Point(1289, 85);
            this.lotModifySaveButton.Name = "lotModifySaveButton";
            this.lotModifySaveButton.Size = new System.Drawing.Size(101, 24);
            this.lotModifySaveButton.TabIndex = 115;
            this.lotModifySaveButton.Text = "Lot 편집";
            this.lotModifySaveButton.UseVisualStyleBackColor = false;
            this.lotModifySaveButton.Visible = false;
            this.lotModifySaveButton.Click += new System.EventHandler(this.MianLotChange_Click);
            // 
            // testCompanyName
            // 
            this.testCompanyName.DropDownWidth = 350;
            this.testCompanyName.Font = new System.Drawing.Font("돋움", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testCompanyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.testCompanyName.FormattingEnabled = true;
            this.testCompanyName.Location = new System.Drawing.Point(143, 86);
            this.testCompanyName.Name = "testCompanyName";
            this.testCompanyName.Size = new System.Drawing.Size(186, 24);
            this.testCompanyName.TabIndex = 114;
            this.testCompanyName.Visible = false;
            // 
            // testDescWherecheckbox
            // 
            this.testDescWherecheckbox.AutoSize = true;
            this.testDescWherecheckbox.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Bold);
            this.testDescWherecheckbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.testDescWherecheckbox.Location = new System.Drawing.Point(39, 90);
            this.testDescWherecheckbox.Name = "testDescWherecheckbox";
            this.testDescWherecheckbox.Size = new System.Drawing.Size(74, 19);
            this.testDescWherecheckbox.TabIndex = 113;
            this.testDescWherecheckbox.Text = "제조사";
            this.testDescWherecheckbox.UseVisualStyleBackColor = true;
            this.testDescWherecheckbox.Visible = false;
            // 
            // testProductTextCheckbox
            // 
            this.testProductTextCheckbox.AutoSize = true;
            this.testProductTextCheckbox.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Bold);
            this.testProductTextCheckbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.testProductTextCheckbox.Location = new System.Drawing.Point(335, 90);
            this.testProductTextCheckbox.Name = "testProductTextCheckbox";
            this.testProductTextCheckbox.Size = new System.Drawing.Size(74, 19);
            this.testProductTextCheckbox.TabIndex = 111;
            this.testProductTextCheckbox.Text = "제품명";
            this.testProductTextCheckbox.UseVisualStyleBackColor = true;
            // 
            // testProductTextBox
            // 
            this.testProductTextBox.Font = new System.Drawing.Font("돋움", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testProductTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.testProductTextBox.Location = new System.Drawing.Point(410, 86);
            this.testProductTextBox.Name = "testProductTextBox";
            this.testProductTextBox.Size = new System.Drawing.Size(308, 26);
            this.testProductTextBox.TabIndex = 110;
            // 
            // testMcComboBox
            // 
            this.testMcComboBox.DropDownWidth = 74;
            this.testMcComboBox.Font = new System.Drawing.Font("돋움", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testMcComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.testMcComboBox.FormattingEnabled = true;
            this.testMcComboBox.Items.AddRange(new object[] {
            "전체",
            "CA#1",
            "CA#3",
            "CA#4",
            "CA#5",
            "CA#6",
            "CA#8",
            "CA#9",
            "CA#10"});
            this.testMcComboBox.Location = new System.Drawing.Point(143, 87);
            this.testMcComboBox.Name = "testMcComboBox";
            this.testMcComboBox.Size = new System.Drawing.Size(74, 24);
            this.testMcComboBox.TabIndex = 108;
            // 
            // textMcnumLabel
            // 
            this.textMcnumLabel.AutoSize = true;
            this.textMcnumLabel.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textMcnumLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.textMcnumLabel.Location = new System.Drawing.Point(36, 91);
            this.textMcnumLabel.Name = "textMcnumLabel";
            this.textMcnumLabel.Size = new System.Drawing.Size(93, 15);
            this.textMcnumLabel.TabIndex = 107;
            this.textMcnumLabel.Text = "테스트 기계";
            // 
            // saveReportButton
            // 
            this.saveReportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.saveReportButton.FlatAppearance.BorderSize = 0;
            this.saveReportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveReportButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.saveReportButton.ForeColor = System.Drawing.Color.White;
            this.saveReportButton.Location = new System.Drawing.Point(1438, 85);
            this.saveReportButton.Name = "saveReportButton";
            this.saveReportButton.Size = new System.Drawing.Size(101, 24);
            this.saveReportButton.TabIndex = 106;
            this.saveReportButton.Text = "리포트 저장";
            this.saveReportButton.UseVisualStyleBackColor = false;
            this.saveReportButton.Visible = false;
            this.saveReportButton.Click += new System.EventHandler(this.saveReportButton_Click);
            // 
            // disapprovalTestCheckBox
            // 
            this.disapprovalTestCheckBox.AutoSize = true;
            this.disapprovalTestCheckBox.Checked = true;
            this.disapprovalTestCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.disapprovalTestCheckBox.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Bold);
            this.disapprovalTestCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.disapprovalTestCheckBox.Location = new System.Drawing.Point(39, 17);
            this.disapprovalTestCheckBox.Name = "disapprovalTestCheckBox";
            this.disapprovalTestCheckBox.Size = new System.Drawing.Size(166, 19);
            this.disapprovalTestCheckBox.TabIndex = 105;
            this.disapprovalTestCheckBox.Text = "미승인 테스트 보기";
            this.disapprovalTestCheckBox.UseVisualStyleBackColor = true;
            // 
            // testDescLabel
            // 
            this.testDescLabel.AutoSize = true;
            this.testDescLabel.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testDescLabel.Location = new System.Drawing.Point(961, 18);
            this.testDescLabel.Name = "testDescLabel";
            this.testDescLabel.Size = new System.Drawing.Size(72, 15);
            this.testDescLabel.TabIndex = 104;
            this.testDescLabel.Text = "Test Desc";
            // 
            // searchTestButton
            // 
            this.searchTestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.searchTestButton.FlatAppearance.BorderSize = 0;
            this.searchTestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchTestButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchTestButton.ForeColor = System.Drawing.Color.White;
            this.searchTestButton.Location = new System.Drawing.Point(617, 45);
            this.searchTestButton.Name = "searchTestButton";
            this.searchTestButton.Size = new System.Drawing.Size(101, 24);
            this.searchTestButton.TabIndex = 4;
            this.searchTestButton.Text = "테스트 검색";
            this.searchTestButton.UseVisualStyleBackColor = false;
            this.searchTestButton.Click += new System.EventHandler(this.searchTestButton_Click);
            // 
            // applyPeriodCheckBox
            // 
            this.applyPeriodCheckBox.AutoSize = true;
            this.applyPeriodCheckBox.Checked = true;
            this.applyPeriodCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.applyPeriodCheckBox.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Bold);
            this.applyPeriodCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.applyPeriodCheckBox.Location = new System.Drawing.Point(515, 51);
            this.applyPeriodCheckBox.Name = "applyPeriodCheckBox";
            this.applyPeriodCheckBox.Size = new System.Drawing.Size(96, 19);
            this.applyPeriodCheckBox.TabIndex = 2;
            this.applyPeriodCheckBox.Text = "기간 적용";
            this.applyPeriodCheckBox.UseVisualStyleBackColor = true;
            this.applyPeriodCheckBox.CheckedChanged += new System.EventHandler(this.applyPeriodCheckBox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.label8.Location = new System.Drawing.Point(312, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 15);
            this.label8.TabIndex = 101;
            this.label8.Text = "~";
            // 
            // endDateTimeDateTimePicker
            // 
            this.endDateTimeDateTimePicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.endDateTimeDateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.endDateTimeDateTimePicker.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.endDateTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimeDateTimePicker.Location = new System.Drawing.Point(335, 46);
            this.endDateTimeDateTimePicker.Name = "endDateTimeDateTimePicker";
            this.endDateTimeDateTimePicker.Size = new System.Drawing.Size(164, 25);
            this.endDateTimeDateTimePicker.TabIndex = 1;
            // 
            // startDateTimeDateTimePicker
            // 
            this.startDateTimeDateTimePicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.startDateTimeDateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.startDateTimeDateTimePicker.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.startDateTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimeDateTimePicker.Location = new System.Drawing.Point(143, 47);
            this.startDateTimeDateTimePicker.Name = "startDateTimeDateTimePicker";
            this.startDateTimeDateTimePicker.Size = new System.Drawing.Size(164, 25);
            this.startDateTimeDateTimePicker.TabIndex = 0;
            this.startDateTimeDateTimePicker.Value = new System.DateTime(2016, 9, 28, 7, 54, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(36, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "테스트 기간";
            // 
            // createReportButton
            // 
            this.createReportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.createReportButton.FlatAppearance.BorderSize = 0;
            this.createReportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createReportButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.createReportButton.ForeColor = System.Drawing.Color.White;
            this.createReportButton.Location = new System.Drawing.Point(1438, 45);
            this.createReportButton.Name = "createReportButton";
            this.createReportButton.Size = new System.Drawing.Size(101, 24);
            this.createReportButton.TabIndex = 5;
            this.createReportButton.Text = "리포트 생성";
            this.createReportButton.UseVisualStyleBackColor = false;
            this.createReportButton.Click += new System.EventHandler(this.createReportButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(857, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "테스트 선택";
            // 
            // testIdComboBox
            // 
            this.testIdComboBox.DropDownWidth = 700;
            this.testIdComboBox.Font = new System.Drawing.Font("돋움", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testIdComboBox.FormattingEnabled = true;
            this.testIdComboBox.Location = new System.Drawing.Point(964, 45);
            this.testIdComboBox.Name = "testIdComboBox";
            this.testIdComboBox.Size = new System.Drawing.Size(468, 24);
            this.testIdComboBox.TabIndex = 3;
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.QTRSFlexViewer);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 128);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1584, 913);
            this.contentPanel.TabIndex = 24;
            this.contentPanel.Resize += new System.EventHandler(this.contentPanel_Resize);
            // 
            // QTRSFlexViewer
            // 
            this.QTRSFlexViewer.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.QTRSFlexViewer.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.QTRSFlexViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QTRSFlexViewer.Location = new System.Drawing.Point(0, 0);
            this.QTRSFlexViewer.Name = "QTRSFlexViewer";
            this.QTRSFlexViewer.Size = new System.Drawing.Size(1584, 913);
            this.QTRSFlexViewer.TabIndex = 1;
            // 
            // FinalProductQtTestDataSource
            // 
            this.FinalProductQtTestDataSource.DataSetName = "FinalProductQtTestDataSource";
            this.FinalProductQtTestDataSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1584, 1041);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "ReportForm";
            this.Text = "ProductQtTestResultReportForm";
            this.Load += new System.EventHandler(this.ProductQtTestResultReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProductQtTestDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductQtTestDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productQtTestHeaderDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestHeaderDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestContentDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestHeaderDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestContentDataSetBindingSource)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.QTRSFlexViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinalProductQtTestDataSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button createReportButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox testIdComboBox;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.BindingSource ProductQtTestDataSetBindingSource;
        private ProductQtTestDataSource ProductQtTestDataSource;
        private System.Windows.Forms.BindingSource productQtTestHeaderDataSetBindingSource;
        private System.Windows.Forms.BindingSource ProductMfTestHeaderDataSetBindingSource;
        private ProductMfTestDataSource ProductMfTestDataSource;
        private System.Windows.Forms.BindingSource ProductMfTestContentDataSetBindingSource;
        private FinalProductQtTestDataSource FinalProductQtTestDataSource;
        private System.Windows.Forms.BindingSource ComponentDrugTestHeaderDataSetBindingSource;
        private ComponentDrugTestDataSource ComponentDrugTestDataSource;
        private System.Windows.Forms.BindingSource ComponentDrugTestContentDataSetBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox applyPeriodCheckBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker endDateTimeDateTimePicker;
        private System.Windows.Forms.DateTimePicker startDateTimeDateTimePicker;
        private System.Windows.Forms.Button searchTestButton;
        private System.Windows.Forms.Label testDescLabel;
        private System.Windows.Forms.CheckBox disapprovalTestCheckBox;
        private System.Windows.Forms.Button saveReportButton;
        private System.Windows.Forms.ComboBox testMcComboBox;
        private System.Windows.Forms.Label textMcnumLabel;
        private System.Windows.Forms.CheckBox testProductTextCheckbox;
        private System.Windows.Forms.TextBox testProductTextBox;
        private System.Windows.Forms.CheckBox testDescWherecheckbox;
        private System.Windows.Forms.ComboBox testCompanyName;
        private C1.Win.FlexViewer.C1FlexViewer QTRSFlexViewer;
        private System.Windows.Forms.Button lotModifySaveButton;
    }
}