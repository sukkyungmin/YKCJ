namespace QTRS.Notice
{
    partial class NoticeControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoticeControl));
            this.label2 = new System.Windows.Forms.Label();
            this.noticeDataGridView = new System.Windows.Forms.DataGridView();
            this.deleteNoticeButton = new System.Windows.Forms.Button();
            this.addNoticeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.applyPeriodCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.endDateTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.noticeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(48, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 20;
            this.label2.Text = "공지사항";
            // 
            // noticeDataGridView
            // 
            this.noticeDataGridView.AllowUserToAddRows = false;
            this.noticeDataGridView.AllowUserToDeleteRows = false;
            this.noticeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.noticeDataGridView.Location = new System.Drawing.Point(40, 152);
            this.noticeDataGridView.Name = "noticeDataGridView";
            this.noticeDataGridView.RowTemplate.Height = 23;
            this.noticeDataGridView.Size = new System.Drawing.Size(1022, 608);
            this.noticeDataGridView.TabIndex = 32;
            this.noticeDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.noticeDataGridView_CellDoubleClick);
            // 
            // deleteNoticeButton
            // 
            this.deleteNoticeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.deleteNoticeButton.FlatAppearance.BorderSize = 0;
            this.deleteNoticeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.deleteNoticeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteNoticeButton.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.deleteNoticeButton.ForeColor = System.Drawing.Color.White;
            this.deleteNoticeButton.Location = new System.Drawing.Point(976, 115);
            this.deleteNoticeButton.Name = "deleteNoticeButton";
            this.deleteNoticeButton.Size = new System.Drawing.Size(86, 31);
            this.deleteNoticeButton.TabIndex = 31;
            this.deleteNoticeButton.Text = "삭제";
            this.deleteNoticeButton.UseVisualStyleBackColor = false;
            this.deleteNoticeButton.Click += new System.EventHandler(this.deleteNoticeButton_Click);
            // 
            // addNoticeButton
            // 
            this.addNoticeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.addNoticeButton.FlatAppearance.BorderSize = 0;
            this.addNoticeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.addNoticeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addNoticeButton.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.addNoticeButton.ForeColor = System.Drawing.Color.White;
            this.addNoticeButton.Location = new System.Drawing.Point(884, 115);
            this.addNoticeButton.Name = "addNoticeButton";
            this.addNoticeButton.Size = new System.Drawing.Size(86, 31);
            this.addNoticeButton.TabIndex = 30;
            this.addNoticeButton.Text = "추가";
            this.addNoticeButton.UseVisualStyleBackColor = false;
            this.addNoticeButton.Click += new System.EventHandler(this.addNoticeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label3.Location = new System.Drawing.Point(48, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 19);
            this.label3.TabIndex = 29;
            this.label3.Text = "공지사항 목록";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(40, 40);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(2, 12);
            this.pictureBox3.TabIndex = 33;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(40, 115);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(2, 12);
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.applyPeriodCheckBox);
            this.searchPanel.Controls.Add(this.panel1);
            this.searchPanel.Controls.Add(this.label8);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.endDateTimeDateTimePicker);
            this.searchPanel.Controls.Add(this.startDateTimeDateTimePicker);
            this.searchPanel.Controls.Add(this.label10);
            this.searchPanel.Location = new System.Drawing.Point(416, 40);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(646, 52);
            this.searchPanel.TabIndex = 35;
            // 
            // applyPeriodCheckBox
            // 
            this.applyPeriodCheckBox.AutoSize = true;
            this.applyPeriodCheckBox.Checked = true;
            this.applyPeriodCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.applyPeriodCheckBox.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.applyPeriodCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.applyPeriodCheckBox.Location = new System.Drawing.Point(303, 15);
            this.applyPeriodCheckBox.Name = "applyPeriodCheckBox";
            this.applyPeriodCheckBox.Size = new System.Drawing.Size(167, 21);
            this.applyPeriodCheckBox.TabIndex = 98;
            this.applyPeriodCheckBox.Text = "기간 선택 (종료일 기준)";
            this.applyPeriodCheckBox.UseVisualStyleBackColor = true;
            this.applyPeriodCheckBox.CheckedChanged += new System.EventHandler(this.applyPeriodCheckBox_CheckedChanged);
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
            this.label8.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.label8.Location = new System.Drawing.Point(144, 16);
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
            this.searchButton.Font = new System.Drawing.Font("Dotum", 11.25F);
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(511, 9);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(118, 33);
            this.searchButton.TabIndex = 9;
            this.searchButton.Text = "검 색";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // endDateTimeDateTimePicker
            // 
            this.endDateTimeDateTimePicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.endDateTimeDateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.endDateTimeDateTimePicker.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.endDateTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimeDateTimePicker.Location = new System.Drawing.Point(169, 13);
            this.endDateTimeDateTimePicker.Name = "endDateTimeDateTimePicker";
            this.endDateTimeDateTimePicker.Size = new System.Drawing.Size(125, 25);
            this.endDateTimeDateTimePicker.TabIndex = 3;
            // 
            // startDateTimeDateTimePicker
            // 
            this.startDateTimeDateTimePicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.startDateTimeDateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.startDateTimeDateTimePicker.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.startDateTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimeDateTimePicker.Location = new System.Drawing.Point(13, 13);
            this.startDateTimeDateTimePicker.Name = "startDateTimeDateTimePicker";
            this.startDateTimeDateTimePicker.Size = new System.Drawing.Size(125, 25);
            this.startDateTimeDateTimePicker.TabIndex = 2;
            this.startDateTimeDateTimePicker.Value = new System.DateTime(2019, 1, 2, 23, 12, 0, 0);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.label10.Location = new System.Drawing.Point(13, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 17);
            this.label10.TabIndex = 0;
            // 
            // NoticeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.noticeDataGridView);
            this.Controls.Add(this.deleteNoticeButton);
            this.Controls.Add(this.addNoticeButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "NoticeControl";
            this.Size = new System.Drawing.Size(1102, 795);
            this.Load += new System.EventHandler(this.NoticeControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.noticeDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button deleteNoticeButton;
        private System.Windows.Forms.Button addNoticeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.CheckBox applyPeriodCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.DateTimePicker endDateTimeDateTimePicker;
        private System.Windows.Forms.DateTimePicker startDateTimeDateTimePicker;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.DataGridView noticeDataGridView;
    }
}
