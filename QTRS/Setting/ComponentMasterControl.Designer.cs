namespace QTRS.Setting
{
    partial class ComponentMasterControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentMasterControl));
            this.exportButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.deleteComponentButton = new System.Windows.Forms.Button();
            this.addComponentButton = new System.Windows.Forms.Button();
            this.componentDataGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.componentCodeTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.componentNameTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.componentDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.exportButton.FlatAppearance.BorderSize = 0;
            this.exportButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportButton.Font = new System.Drawing.Font("Dotum", 11.25F);
            this.exportButton.ForeColor = System.Drawing.Color.White;
            this.exportButton.Location = new System.Drawing.Point(976, 635);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(86, 31);
            this.exportButton.TabIndex = 31;
            this.exportButton.Text = "내보내기";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // importButton
            // 
            this.importButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.importButton.FlatAppearance.BorderSize = 0;
            this.importButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.importButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.importButton.Font = new System.Drawing.Font("Dotum", 11.25F);
            this.importButton.ForeColor = System.Drawing.Color.White;
            this.importButton.Location = new System.Drawing.Point(40, 635);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(86, 31);
            this.importButton.TabIndex = 30;
            this.importButton.Text = "불러오기";
            this.importButton.UseVisualStyleBackColor = false;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // deleteComponentButton
            // 
            this.deleteComponentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.deleteComponentButton.FlatAppearance.BorderSize = 0;
            this.deleteComponentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.deleteComponentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteComponentButton.Font = new System.Drawing.Font("Dotum", 11.25F);
            this.deleteComponentButton.ForeColor = System.Drawing.Color.White;
            this.deleteComponentButton.Location = new System.Drawing.Point(976, 123);
            this.deleteComponentButton.Name = "deleteComponentButton";
            this.deleteComponentButton.Size = new System.Drawing.Size(86, 31);
            this.deleteComponentButton.TabIndex = 28;
            this.deleteComponentButton.Text = "삭제";
            this.deleteComponentButton.UseVisualStyleBackColor = false;
            this.deleteComponentButton.Click += new System.EventHandler(this.deleteComponentButton_Click);
            // 
            // addComponentButton
            // 
            this.addComponentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.addComponentButton.FlatAppearance.BorderSize = 0;
            this.addComponentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(108)))), ((int)(((byte)(125)))));
            this.addComponentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addComponentButton.Font = new System.Drawing.Font("Dotum", 11.25F);
            this.addComponentButton.ForeColor = System.Drawing.Color.White;
            this.addComponentButton.Location = new System.Drawing.Point(884, 123);
            this.addComponentButton.Name = "addComponentButton";
            this.addComponentButton.Size = new System.Drawing.Size(86, 31);
            this.addComponentButton.TabIndex = 27;
            this.addComponentButton.Text = "추가";
            this.addComponentButton.UseVisualStyleBackColor = false;
            this.addComponentButton.Click += new System.EventHandler(this.addComponentButton_Click);
            // 
            // componentDataGridView
            // 
            this.componentDataGridView.AllowUserToAddRows = false;
            this.componentDataGridView.AllowUserToDeleteRows = false;
            this.componentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.componentDataGridView.Location = new System.Drawing.Point(40, 162);
            this.componentDataGridView.Name = "componentDataGridView";
            this.componentDataGridView.ReadOnly = true;
            this.componentDataGridView.RowTemplate.Height = 23;
            this.componentDataGridView.Size = new System.Drawing.Size(1022, 467);
            this.componentDataGridView.TabIndex = 29;
            this.componentDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.componentDataGridView_CellContentClick);
            this.componentDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.componentDataGridView_CurrentCellDirtyStateChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label3.Location = new System.Drawing.Point(48, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 19);
            this.label3.TabIndex = 26;
            this.label3.Text = "원료 데이터 관리";
            // 
            // componentCodeTextBox
            // 
            this.componentCodeTextBox.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.componentCodeTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.componentCodeTextBox.Location = new System.Drawing.Point(141, 36);
            this.componentCodeTextBox.Name = "componentCodeTextBox";
            this.componentCodeTextBox.Size = new System.Drawing.Size(183, 29);
            this.componentCodeTextBox.TabIndex = 24;
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(177)))), ((int)(((byte)(224)))));
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Dotum", 11.25F);
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(331, 36);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(82, 60);
            this.searchButton.TabIndex = 25;
            this.searchButton.Text = "검색";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(40, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 19);
            this.label2.TabIndex = 23;
            this.label2.Text = "원료 이름";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(40, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 19);
            this.label1.TabIndex = 22;
            this.label1.Text = "원료 코드";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(40, 125);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(2, 12);
            this.pictureBox2.TabIndex = 32;
            this.pictureBox2.TabStop = false;
            // 
            // componentNameTextBox
            // 
            this.componentNameTextBox.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.componentNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.componentNameTextBox.Location = new System.Drawing.Point(141, 67);
            this.componentNameTextBox.Name = "componentNameTextBox";
            this.componentNameTextBox.Size = new System.Drawing.Size(183, 29);
            this.componentNameTextBox.TabIndex = 35;
            // 
            // ComponentMasterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.componentNameTextBox);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.deleteComponentButton);
            this.Controls.Add(this.addComponentButton);
            this.Controls.Add(this.componentDataGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.componentCodeTextBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Name = "ComponentMasterControl";
            this.Size = new System.Drawing.Size(1102, 686);
            this.Load += new System.EventHandler(this.ComponentMasterControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.componentDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button deleteComponentButton;
        private System.Windows.Forms.Button addComponentButton;
        public System.Windows.Forms.DataGridView componentDataGridView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox componentCodeTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox componentNameTextBox;
    }
}
