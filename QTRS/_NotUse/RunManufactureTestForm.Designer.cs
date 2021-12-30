namespace QTRS
{
    partial class RunManufactureTestForm
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
            this.fileTabControl = new System.Windows.Forms.TabControl();
            this.imageTabPage = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.imageDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.imageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.fileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileTabPage = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.fileDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fileFileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testFormatPanel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.compatibilityOxComboBox = new System.Windows.Forms.ComboBox();
            this.deleteTestButton = new System.Windows.Forms.Button();
            this.addTestButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.testScoreTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.testMethodTextBox = new System.Windows.Forms.TextBox();
            this.testItemComboBox = new System.Windows.Forms.ComboBox();
            this.productMfDataGridView = new System.Windows.Forms.DataGridView();
            this.testFormatPanelTopBorder = new System.Windows.Forms.Panel();
            this.addFileButton = new System.Windows.Forms.Button();
            this.addDataButton = new System.Windows.Forms.Button();
            this.transferProgressBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.captureButton = new System.Windows.Forms.Button();
            this.downloadFileButton = new System.Windows.Forms.Button();
            this.deleteFileButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.exportButton = new System.Windows.Forms.Button();
            this.ohausSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.fileTabControl.SuspendLayout();
            this.imageTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageDataGridView)).BeginInit();
            this.fileTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileDataGridView)).BeginInit();
            this.testFormatPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productMfDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileTabControl
            // 
            this.fileTabControl.Controls.Add(this.imageTabPage);
            this.fileTabControl.Controls.Add(this.fileTabPage);
            this.fileTabControl.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.fileTabControl.Location = new System.Drawing.Point(0, 1);
            this.fileTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.fileTabControl.Name = "fileTabControl";
            this.fileTabControl.SelectedIndex = 0;
            this.fileTabControl.Size = new System.Drawing.Size(860, 278);
            this.fileTabControl.TabIndex = 23;
            // 
            // imageTabPage
            // 
            this.imageTabPage.Controls.Add(this.panel4);
            this.imageTabPage.Controls.Add(this.imageDataGridView);
            this.imageTabPage.Location = new System.Drawing.Point(4, 26);
            this.imageTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.imageTabPage.Name = "imageTabPage";
            this.imageTabPage.Size = new System.Drawing.Size(852, 248);
            this.imageTabPage.TabIndex = 0;
            this.imageTabPage.Text = "이미지 리스트";
            this.imageTabPage.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(852, 2);
            this.panel4.TabIndex = 15;
            // 
            // imageDataGridView
            // 
            this.imageDataGridView.AllowUserToAddRows = false;
            this.imageDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.imageDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.checkBoxColumn,
            this.imageColumn,
            this.fileNameColumn,
            this.dataGridViewTextBoxColumn2,
            this.filePathColumn,
            this.idColumn});
            this.imageDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageDataGridView.Location = new System.Drawing.Point(0, 0);
            this.imageDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.imageDataGridView.Name = "imageDataGridView";
            this.imageDataGridView.RowTemplate.Height = 23;
            this.imageDataGridView.Size = new System.Drawing.Size(852, 248);
            this.imageDataGridView.TabIndex = 14;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // checkBoxColumn
            // 
            this.checkBoxColumn.HeaderText = "";
            this.checkBoxColumn.Name = "checkBoxColumn";
            // 
            // imageColumn
            // 
            this.imageColumn.HeaderText = "파일 이미지";
            this.imageColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.imageColumn.Name = "imageColumn";
            this.imageColumn.ReadOnly = true;
            this.imageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.imageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // fileNameColumn
            // 
            this.fileNameColumn.HeaderText = "파일 이름";
            this.fileNameColumn.Name = "fileNameColumn";
            this.fileNameColumn.ReadOnly = true;
            this.fileNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fileNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "파일 설명";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // filePathColumn
            // 
            this.filePathColumn.HeaderText = "파일 경로";
            this.filePathColumn.Name = "filePathColumn";
            this.filePathColumn.ReadOnly = true;
            this.filePathColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.filePathColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.filePathColumn.Visible = false;
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "ID";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Visible = false;
            // 
            // fileTabPage
            // 
            this.fileTabPage.Controls.Add(this.panel3);
            this.fileTabPage.Controls.Add(this.fileDataGridView);
            this.fileTabPage.Location = new System.Drawing.Point(4, 26);
            this.fileTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.fileTabPage.Name = "fileTabPage";
            this.fileTabPage.Size = new System.Drawing.Size(852, 248);
            this.fileTabPage.TabIndex = 1;
            this.fileTabPage.Text = "파일 리스트";
            this.fileTabPage.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(852, 2);
            this.panel3.TabIndex = 16;
            // 
            // fileDataGridView
            // 
            this.fileDataGridView.AllowUserToAddRows = false;
            this.fileDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fileDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.fileCheckBoxColumn,
            this.fileFileNameColumn,
            this.dataGridViewTextBoxColumn4,
            this.fileFilePath,
            this.fileIdColumn});
            this.fileDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileDataGridView.Location = new System.Drawing.Point(0, 0);
            this.fileDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.fileDataGridView.Name = "fileDataGridView";
            this.fileDataGridView.RowTemplate.Height = 23;
            this.fileDataGridView.Size = new System.Drawing.Size(852, 248);
            this.fileDataGridView.TabIndex = 15;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "No";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // fileCheckBoxColumn
            // 
            this.fileCheckBoxColumn.HeaderText = "";
            this.fileCheckBoxColumn.Name = "fileCheckBoxColumn";
            // 
            // fileFileNameColumn
            // 
            this.fileFileNameColumn.HeaderText = "파일 이름";
            this.fileFileNameColumn.Name = "fileFileNameColumn";
            this.fileFileNameColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "파일 설명";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // fileFilePath
            // 
            this.fileFilePath.HeaderText = "파일 경로";
            this.fileFilePath.Name = "fileFilePath";
            this.fileFilePath.ReadOnly = true;
            this.fileFilePath.Visible = false;
            // 
            // fileIdColumn
            // 
            this.fileIdColumn.HeaderText = "ID";
            this.fileIdColumn.Name = "fileIdColumn";
            this.fileIdColumn.ReadOnly = true;
            this.fileIdColumn.Visible = false;
            // 
            // testFormatPanel
            // 
            this.testFormatPanel.AutoScroll = true;
            this.testFormatPanel.BackColor = System.Drawing.Color.White;
            this.testFormatPanel.Controls.Add(this.label9);
            this.testFormatPanel.Controls.Add(this.compatibilityOxComboBox);
            this.testFormatPanel.Controls.Add(this.deleteTestButton);
            this.testFormatPanel.Controls.Add(this.addTestButton);
            this.testFormatPanel.Controls.Add(this.label4);
            this.testFormatPanel.Controls.Add(this.testScoreTextBox);
            this.testFormatPanel.Controls.Add(this.label7);
            this.testFormatPanel.Controls.Add(this.label8);
            this.testFormatPanel.Controls.Add(this.testMethodTextBox);
            this.testFormatPanel.Controls.Add(this.testItemComboBox);
            this.testFormatPanel.Controls.Add(this.productMfDataGridView);
            this.testFormatPanel.Controls.Add(this.testFormatPanelTopBorder);
            this.testFormatPanel.Location = new System.Drawing.Point(20, 20);
            this.testFormatPanel.Margin = new System.Windows.Forms.Padding(0);
            this.testFormatPanel.Name = "testFormatPanel";
            this.testFormatPanel.Size = new System.Drawing.Size(954, 410);
            this.testFormatPanel.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label9.Location = new System.Drawing.Point(672, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 213;
            this.label9.Text = "적합여부";
            // 
            // compatibilityOxComboBox
            // 
            this.compatibilityOxComboBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.compatibilityOxComboBox.FormattingEnabled = true;
            this.compatibilityOxComboBox.Location = new System.Drawing.Point(745, 72);
            this.compatibilityOxComboBox.Name = "compatibilityOxComboBox";
            this.compatibilityOxComboBox.Size = new System.Drawing.Size(91, 23);
            this.compatibilityOxComboBox.TabIndex = 2;
            // 
            // deleteTestButton
            // 
            this.deleteTestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.deleteTestButton.FlatAppearance.BorderSize = 0;
            this.deleteTestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteTestButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.deleteTestButton.ForeColor = System.Drawing.Color.White;
            this.deleteTestButton.Location = new System.Drawing.Point(868, 62);
            this.deleteTestButton.Name = "deleteTestButton";
            this.deleteTestButton.Size = new System.Drawing.Size(70, 35);
            this.deleteTestButton.TabIndex = 4;
            this.deleteTestButton.Text = "삭 제";
            this.deleteTestButton.UseVisualStyleBackColor = false;
            this.deleteTestButton.Click += new System.EventHandler(this.deleteTestButton_Click);
            // 
            // addTestButton
            // 
            this.addTestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.addTestButton.FlatAppearance.BorderSize = 0;
            this.addTestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addTestButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.addTestButton.ForeColor = System.Drawing.Color.White;
            this.addTestButton.Location = new System.Drawing.Point(868, 12);
            this.addTestButton.Name = "addTestButton";
            this.addTestButton.Size = new System.Drawing.Size(70, 35);
            this.addTestButton.TabIndex = 3;
            this.addTestButton.Text = "추 가";
            this.addTestButton.UseVisualStyleBackColor = false;
            this.addTestButton.Click += new System.EventHandler(this.addTestButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label4.Location = new System.Drawing.Point(15, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 209;
            this.label4.Text = "시험성적";
            // 
            // testScoreTextBox
            // 
            this.testScoreTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testScoreTextBox.Location = new System.Drawing.Point(92, 72);
            this.testScoreTextBox.Name = "testScoreTextBox";
            this.testScoreTextBox.Size = new System.Drawing.Size(554, 25);
            this.testScoreTextBox.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label7.Location = new System.Drawing.Point(15, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 207;
            this.label7.Text = "시험방법";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label8.Location = new System.Drawing.Point(15, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 206;
            this.label8.Text = "시험항목";
            // 
            // testMethodTextBox
            // 
            this.testMethodTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testMethodTextBox.Location = new System.Drawing.Point(92, 41);
            this.testMethodTextBox.Name = "testMethodTextBox";
            this.testMethodTextBox.ReadOnly = true;
            this.testMethodTextBox.Size = new System.Drawing.Size(744, 25);
            this.testMethodTextBox.TabIndex = 205;
            // 
            // testItemComboBox
            // 
            this.testItemComboBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testItemComboBox.FormattingEnabled = true;
            this.testItemComboBox.Location = new System.Drawing.Point(92, 12);
            this.testItemComboBox.Name = "testItemComboBox";
            this.testItemComboBox.Size = new System.Drawing.Size(744, 23);
            this.testItemComboBox.TabIndex = 0;
            this.testItemComboBox.SelectedIndexChanged += new System.EventHandler(this.testItemComboBox_SelectedIndexChanged);
            // 
            // productMfDataGridView
            // 
            this.productMfDataGridView.AllowUserToAddRows = false;
            this.productMfDataGridView.AllowUserToDeleteRows = false;
            this.productMfDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productMfDataGridView.Location = new System.Drawing.Point(0, 114);
            this.productMfDataGridView.Name = "productMfDataGridView";
            this.productMfDataGridView.RowTemplate.Height = 23;
            this.productMfDataGridView.Size = new System.Drawing.Size(954, 296);
            this.productMfDataGridView.TabIndex = 117;
            // 
            // testFormatPanelTopBorder
            // 
            this.testFormatPanelTopBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.testFormatPanelTopBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.testFormatPanelTopBorder.Location = new System.Drawing.Point(0, 0);
            this.testFormatPanelTopBorder.Margin = new System.Windows.Forms.Padding(0);
            this.testFormatPanelTopBorder.Name = "testFormatPanelTopBorder";
            this.testFormatPanelTopBorder.Size = new System.Drawing.Size(954, 2);
            this.testFormatPanelTopBorder.TabIndex = 13;
            // 
            // addFileButton
            // 
            this.addFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.addFileButton.FlatAppearance.BorderSize = 0;
            this.addFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addFileButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.addFileButton.ForeColor = System.Drawing.Color.White;
            this.addFileButton.Location = new System.Drawing.Point(864, 22);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(70, 35);
            this.addFileButton.TabIndex = 0;
            this.addFileButton.Text = "추 가";
            this.addFileButton.UseVisualStyleBackColor = false;
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // addDataButton
            // 
            this.addDataButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.addDataButton.FlatAppearance.BorderSize = 0;
            this.addDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addDataButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.addDataButton.ForeColor = System.Drawing.Color.White;
            this.addDataButton.Location = new System.Drawing.Point(740, 740);
            this.addDataButton.Name = "addDataButton";
            this.addDataButton.Size = new System.Drawing.Size(82, 35);
            this.addDataButton.TabIndex = 0;
            this.addDataButton.Text = "검사완료";
            this.addDataButton.UseVisualStyleBackColor = false;
            this.addDataButton.Click += new System.EventHandler(this.addDataButton_Click);
            // 
            // transferProgressBar
            // 
            this.transferProgressBar.Location = new System.Drawing.Point(20, 740);
            this.transferProgressBar.Name = "transferProgressBar";
            this.transferProgressBar.Size = new System.Drawing.Size(714, 35);
            this.transferProgressBar.TabIndex = 35;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(828, 740);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(70, 35);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "취소";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.captureButton);
            this.panel1.Controls.Add(this.downloadFileButton);
            this.panel1.Controls.Add(this.fileTabControl);
            this.panel1.Controls.Add(this.deleteFileButton);
            this.panel1.Controls.Add(this.addFileButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(20, 445);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(954, 280);
            this.panel1.TabIndex = 32;
            // 
            // captureButton
            // 
            this.captureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.captureButton.FlatAppearance.BorderSize = 0;
            this.captureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.captureButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.captureButton.ForeColor = System.Drawing.Color.White;
            this.captureButton.Location = new System.Drawing.Point(864, 63);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(70, 35);
            this.captureButton.TabIndex = 1;
            this.captureButton.Text = "촬 영";
            this.captureButton.UseVisualStyleBackColor = false;
            this.captureButton.Click += new System.EventHandler(this.captureButton_Click);
            // 
            // downloadFileButton
            // 
            this.downloadFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.downloadFileButton.FlatAppearance.BorderSize = 0;
            this.downloadFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadFileButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.downloadFileButton.ForeColor = System.Drawing.Color.White;
            this.downloadFileButton.Location = new System.Drawing.Point(864, 157);
            this.downloadFileButton.Name = "downloadFileButton";
            this.downloadFileButton.Size = new System.Drawing.Size(70, 35);
            this.downloadFileButton.TabIndex = 3;
            this.downloadFileButton.Text = "다운로드";
            this.downloadFileButton.UseVisualStyleBackColor = false;
            this.downloadFileButton.Click += new System.EventHandler(this.downloadFileButton_Click);
            // 
            // deleteFileButton
            // 
            this.deleteFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.deleteFileButton.FlatAppearance.BorderSize = 0;
            this.deleteFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteFileButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.deleteFileButton.ForeColor = System.Drawing.Color.White;
            this.deleteFileButton.Location = new System.Drawing.Point(864, 104);
            this.deleteFileButton.Name = "deleteFileButton";
            this.deleteFileButton.Size = new System.Drawing.Size(70, 35);
            this.deleteFileButton.TabIndex = 2;
            this.deleteFileButton.Text = "삭 제";
            this.deleteFileButton.UseVisualStyleBackColor = false;
            this.deleteFileButton.Click += new System.EventHandler(this.deleteFileButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(954, 2);
            this.panel2.TabIndex = 13;
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.exportButton.FlatAppearance.BorderSize = 0;
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.exportButton.ForeColor = System.Drawing.Color.White;
            this.exportButton.Location = new System.Drawing.Point(904, 740);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(70, 35);
            this.exportButton.TabIndex = 2;
            this.exportButton.Text = "내보내기";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // ohausSerialPort
            // 
            this.ohausSerialPort.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.ohausSerialPort_ErrorReceived);
            this.ohausSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.ohausSerialPort_DataReceived);
            // 
            // RunManufactureTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(998, 796);
            this.ControlBox = false;
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.testFormatPanel);
            this.Controls.Add(this.addDataButton);
            this.Controls.Add(this.transferProgressBar);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RunManufactureTestForm";
            this.Text = "완제품 제조관리 검사 실행";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RunManufactureTestForm_FormClosed);
            this.Load += new System.EventHandler(this.RunManufactureTestForm_Load);
            this.fileTabControl.ResumeLayout(false);
            this.imageTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageDataGridView)).EndInit();
            this.fileTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileDataGridView)).EndInit();
            this.testFormatPanel.ResumeLayout(false);
            this.testFormatPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productMfDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl fileTabControl;
        private System.Windows.Forms.TabPage imageTabPage;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView imageDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn imageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.TabPage fileTabPage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView fileDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn fileCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileFileNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileIdColumn;
        private System.Windows.Forms.Panel testFormatPanel;
        private System.Windows.Forms.Panel testFormatPanelTopBorder;
        private System.Windows.Forms.Button addFileButton;
        private System.Windows.Forms.Button addDataButton;
        private System.Windows.Forms.ProgressBar transferProgressBar;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.Button downloadFileButton;
        private System.Windows.Forms.Button deleteFileButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView productMfDataGridView;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox testScoreTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox testMethodTextBox;
        private System.Windows.Forms.ComboBox testItemComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox compatibilityOxComboBox;
        private System.Windows.Forms.Button deleteTestButton;
        private System.Windows.Forms.Button addTestButton;
        private System.IO.Ports.SerialPort ohausSerialPort;
    }
}