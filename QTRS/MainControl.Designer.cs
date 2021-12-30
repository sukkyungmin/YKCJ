namespace QTRS
{
    partial class MainControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.noticeDataGridView = new System.Windows.Forms.DataGridView();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.viewAllNoticeButton = new System.Windows.Forms.Button();
            this.writeNoticeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.componentTestDataGridView = new System.Windows.Forms.DataGridView();
            this.productTextLabel = new System.Windows.Forms.Label();
            this.reportGroupBox = new System.Windows.Forms.GroupBox();
            this.finalQualityManagementReportViewButton = new System.Windows.Forms.Button();
            this.manufactureManagementReportViewButton = new System.Windows.Forms.Button();
            this.qualityManagementReportViewButton = new System.Windows.Forms.Button();
            this.componentDrugTestReportViewButton = new System.Windows.Forms.Button();
            this.productTextPictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.productTestDataGridView = new System.Windows.Forms.DataGridView();
            this.homeTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.noticeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentTestDataGridView)).BeginInit();
            this.reportGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productTextPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productTestDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(40, 40);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            // 
            // noticeDataGridView
            // 
            this.noticeDataGridView.AllowUserToAddRows = false;
            this.noticeDataGridView.AllowUserToDeleteRows = false;
            this.noticeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.noticeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column21,
            this.Column22,
            this.Column23,
            this.Column24,
            this.Column25});
            this.noticeDataGridView.Location = new System.Drawing.Point(330, 77);
            this.noticeDataGridView.Name = "noticeDataGridView";
            this.noticeDataGridView.ReadOnly = true;
            this.noticeDataGridView.RowTemplate.Height = 23;
            this.noticeDataGridView.Size = new System.Drawing.Size(733, 125);
            this.noticeDataGridView.TabIndex = 1;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "게시기간";
            this.Column21.Name = "Column21";
            this.Column21.ReadOnly = true;
            // 
            // Column22
            // 
            this.Column22.HeaderText = "대상";
            this.Column22.Name = "Column22";
            this.Column22.ReadOnly = true;
            // 
            // Column23
            // 
            this.Column23.HeaderText = "제목";
            this.Column23.Name = "Column23";
            this.Column23.ReadOnly = true;
            // 
            // Column24
            // 
            this.Column24.HeaderText = "작성자";
            this.Column24.Name = "Column24";
            this.Column24.ReadOnly = true;
            // 
            // Column25
            // 
            this.Column25.HeaderText = "idx";
            this.Column25.Name = "Column25";
            this.Column25.ReadOnly = true;
            this.Column25.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(338, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "공지사항";
            // 
            // viewAllNoticeButton
            // 
            this.viewAllNoticeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.viewAllNoticeButton.FlatAppearance.BorderSize = 0;
            this.viewAllNoticeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.viewAllNoticeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewAllNoticeButton.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.viewAllNoticeButton.ForeColor = System.Drawing.Color.White;
            this.viewAllNoticeButton.Location = new System.Drawing.Point(885, 40);
            this.viewAllNoticeButton.Name = "viewAllNoticeButton";
            this.viewAllNoticeButton.Size = new System.Drawing.Size(86, 31);
            this.viewAllNoticeButton.TabIndex = 7;
            this.viewAllNoticeButton.Text = "전체보기";
            this.viewAllNoticeButton.UseVisualStyleBackColor = false;
            this.viewAllNoticeButton.Click += new System.EventHandler(this.viewAllNoticeButton_Click);
            // 
            // writeNoticeButton
            // 
            this.writeNoticeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.writeNoticeButton.FlatAppearance.BorderSize = 0;
            this.writeNoticeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.writeNoticeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.writeNoticeButton.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.writeNoticeButton.ForeColor = System.Drawing.Color.White;
            this.writeNoticeButton.Location = new System.Drawing.Point(977, 40);
            this.writeNoticeButton.Name = "writeNoticeButton";
            this.writeNoticeButton.Size = new System.Drawing.Size(86, 31);
            this.writeNoticeButton.TabIndex = 8;
            this.writeNoticeButton.Text = "작성";
            this.writeNoticeButton.UseVisualStyleBackColor = false;
            this.writeNoticeButton.Click += new System.EventHandler(this.writeNoticeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(52, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "원료 검사목록";
            // 
            // componentTestDataGridView
            // 
            this.componentTestDataGridView.AllowUserToAddRows = false;
            this.componentTestDataGridView.AllowUserToDeleteRows = false;
            this.componentTestDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.componentTestDataGridView.Location = new System.Drawing.Point(40, 258);
            this.componentTestDataGridView.Name = "componentTestDataGridView";
            this.componentTestDataGridView.ReadOnly = true;
            this.componentTestDataGridView.RowTemplate.Height = 23;
            this.componentTestDataGridView.Size = new System.Drawing.Size(1023, 147);
            this.componentTestDataGridView.TabIndex = 11;
            // 
            // productTextLabel
            // 
            this.productTextLabel.AutoSize = true;
            this.productTextLabel.BackColor = System.Drawing.Color.Transparent;
            this.productTextLabel.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.productTextLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.productTextLabel.Location = new System.Drawing.Point(52, 430);
            this.productTextLabel.Name = "productTextLabel";
            this.productTextLabel.Size = new System.Drawing.Size(156, 19);
            this.productTextLabel.TabIndex = 13;
            this.productTextLabel.Text = "완제품 검사목록";
            // 
            // reportGroupBox
            // 
            this.reportGroupBox.Controls.Add(this.finalQualityManagementReportViewButton);
            this.reportGroupBox.Controls.Add(this.manufactureManagementReportViewButton);
            this.reportGroupBox.Controls.Add(this.qualityManagementReportViewButton);
            this.reportGroupBox.Controls.Add(this.componentDrugTestReportViewButton);
            this.reportGroupBox.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.reportGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.reportGroupBox.Location = new System.Drawing.Point(40, 640);
            this.reportGroupBox.Name = "reportGroupBox";
            this.reportGroupBox.Size = new System.Drawing.Size(1023, 129);
            this.reportGroupBox.TabIndex = 15;
            this.reportGroupBox.TabStop = false;
            this.reportGroupBox.Text = "Report";
            // 
            // finalQualityManagementReportViewButton
            // 
            this.finalQualityManagementReportViewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.finalQualityManagementReportViewButton.Enabled = false;
            this.finalQualityManagementReportViewButton.FlatAppearance.BorderSize = 0;
            this.finalQualityManagementReportViewButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.finalQualityManagementReportViewButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(219)))));
            this.finalQualityManagementReportViewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.finalQualityManagementReportViewButton.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.finalQualityManagementReportViewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.finalQualityManagementReportViewButton.Location = new System.Drawing.Point(755, 33);
            this.finalQualityManagementReportViewButton.Name = "finalQualityManagementReportViewButton";
            this.finalQualityManagementReportViewButton.Size = new System.Drawing.Size(234, 65);
            this.finalQualityManagementReportViewButton.TabIndex = 19;
            this.finalQualityManagementReportViewButton.Text = "최종포장 성적서";
            this.finalQualityManagementReportViewButton.UseVisualStyleBackColor = false;
            this.finalQualityManagementReportViewButton.Click += new System.EventHandler(this.finalQualityManagementReportViewButton_Click);
            // 
            // manufactureManagementReportViewButton
            // 
            this.manufactureManagementReportViewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.manufactureManagementReportViewButton.FlatAppearance.BorderSize = 0;
            this.manufactureManagementReportViewButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.manufactureManagementReportViewButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(219)))));
            this.manufactureManagementReportViewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.manufactureManagementReportViewButton.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.manufactureManagementReportViewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.manufactureManagementReportViewButton.Location = new System.Drawing.Point(515, 33);
            this.manufactureManagementReportViewButton.Name = "manufactureManagementReportViewButton";
            this.manufactureManagementReportViewButton.Size = new System.Drawing.Size(234, 65);
            this.manufactureManagementReportViewButton.TabIndex = 18;
            this.manufactureManagementReportViewButton.Text = "제조관리 기록서";
            this.manufactureManagementReportViewButton.UseVisualStyleBackColor = false;
            this.manufactureManagementReportViewButton.Click += new System.EventHandler(this.manufactureManagementReportViewButton_Click);
            // 
            // qualityManagementReportViewButton
            // 
            this.qualityManagementReportViewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.qualityManagementReportViewButton.FlatAppearance.BorderSize = 0;
            this.qualityManagementReportViewButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.qualityManagementReportViewButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(219)))));
            this.qualityManagementReportViewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.qualityManagementReportViewButton.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.qualityManagementReportViewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.qualityManagementReportViewButton.Location = new System.Drawing.Point(275, 33);
            this.qualityManagementReportViewButton.Name = "qualityManagementReportViewButton";
            this.qualityManagementReportViewButton.Size = new System.Drawing.Size(234, 65);
            this.qualityManagementReportViewButton.TabIndex = 17;
            this.qualityManagementReportViewButton.Text = "품질관리 기록서";
            this.qualityManagementReportViewButton.UseVisualStyleBackColor = false;
            this.qualityManagementReportViewButton.Click += new System.EventHandler(this.qualityManagementReportViewButton_Click);
            // 
            // componentDrugTestReportViewButton
            // 
            this.componentDrugTestReportViewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.componentDrugTestReportViewButton.FlatAppearance.BorderSize = 0;
            this.componentDrugTestReportViewButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.componentDrugTestReportViewButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(219)))));
            this.componentDrugTestReportViewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.componentDrugTestReportViewButton.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.componentDrugTestReportViewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.componentDrugTestReportViewButton.Location = new System.Drawing.Point(35, 33);
            this.componentDrugTestReportViewButton.Name = "componentDrugTestReportViewButton";
            this.componentDrugTestReportViewButton.Size = new System.Drawing.Size(234, 65);
            this.componentDrugTestReportViewButton.TabIndex = 16;
            this.componentDrugTestReportViewButton.Text = "원료 시험 성적서";
            this.componentDrugTestReportViewButton.UseVisualStyleBackColor = false;
            this.componentDrugTestReportViewButton.Click += new System.EventHandler(this.componentDrugTestReportViewButton_Click);
            // 
            // productTextPictureBox
            // 
            this.productTextPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("productTextPictureBox.BackgroundImage")));
            this.productTextPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.productTextPictureBox.Location = new System.Drawing.Point(44, 432);
            this.productTextPictureBox.Name = "productTextPictureBox";
            this.productTextPictureBox.Size = new System.Drawing.Size(2, 12);
            this.productTextPictureBox.TabIndex = 12;
            this.productTextPictureBox.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(44, 229);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(2, 12);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(330, 47);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(2, 12);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // productTestDataGridView
            // 
            this.productTestDataGridView.AllowUserToAddRows = false;
            this.productTestDataGridView.AllowUserToDeleteRows = false;
            this.productTestDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productTestDataGridView.Location = new System.Drawing.Point(40, 461);
            this.productTestDataGridView.Name = "productTestDataGridView";
            this.productTestDataGridView.ReadOnly = true;
            this.productTestDataGridView.RowTemplate.Height = 23;
            this.productTestDataGridView.Size = new System.Drawing.Size(1023, 147);
            this.productTestDataGridView.TabIndex = 34;
            // 
            // homeTimer
            // 
            this.homeTimer.Interval = 10000;
            this.homeTimer.Tick += new System.EventHandler(this.homeTimer_Tick);
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.productTestDataGridView);
            this.Controls.Add(this.reportGroupBox);
            this.Controls.Add(this.productTextLabel);
            this.Controls.Add(this.productTextPictureBox);
            this.Controls.Add(this.componentTestDataGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.writeNoticeButton);
            this.Controls.Add(this.viewAllNoticeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.noticeDataGridView);
            this.Controls.Add(this.monthCalendar1);
            this.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1102, 795);
            this.Load += new System.EventHandler(this.MainControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.noticeDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentTestDataGridView)).EndInit();
            this.reportGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.productTextPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productTestDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.DataGridView noticeDataGridView;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button viewAllNoticeButton;
        private System.Windows.Forms.Button writeNoticeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView componentTestDataGridView;
        private System.Windows.Forms.Label productTextLabel;
        private System.Windows.Forms.PictureBox productTextPictureBox;
        private System.Windows.Forms.GroupBox reportGroupBox;
        private System.Windows.Forms.Button componentDrugTestReportViewButton;
        private System.Windows.Forms.Button finalQualityManagementReportViewButton;
        private System.Windows.Forms.Button manufactureManagementReportViewButton;
        private System.Windows.Forms.Button qualityManagementReportViewButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column25;
        private System.Windows.Forms.DataGridView productTestDataGridView;
        private System.Windows.Forms.Timer homeTimer;
    }
}
