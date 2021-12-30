namespace QTRS.PackingTest
{
    partial class RunQualityTestForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.testFormatPanel = new System.Windows.Forms.Panel();
            this.productQtDataGridView = new System.Windows.Forms.DataGridView();
            this.testFormatPanelTopBorder = new System.Windows.Forms.Panel();
            this.addDataButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.testItemTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.compatibilityOxComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.testScoreTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.deleteTestButton = new System.Windows.Forms.Button();
            this.addTestButton = new System.Windows.Forms.Button();
            this.testMethodTextBox = new System.Windows.Forms.TextBox();
            this.productQtPackingDataGridView = new System.Windows.Forms.DataGridView();
            this.testFormatPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productQtDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productQtPackingDataGridView)).BeginInit();
            this.SuspendLayout();
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
            // testFormatPanel
            // 
            this.testFormatPanel.AutoScroll = true;
            this.testFormatPanel.BackColor = System.Drawing.Color.White;
            this.testFormatPanel.Controls.Add(this.productQtDataGridView);
            this.testFormatPanel.Controls.Add(this.testFormatPanelTopBorder);
            this.testFormatPanel.Location = new System.Drawing.Point(20, 20);
            this.testFormatPanel.Margin = new System.Windows.Forms.Padding(0);
            this.testFormatPanel.Name = "testFormatPanel";
            this.testFormatPanel.Size = new System.Drawing.Size(954, 280);
            this.testFormatPanel.TabIndex = 26;
            // 
            // productQtDataGridView
            // 
            this.productQtDataGridView.AllowUserToAddRows = false;
            this.productQtDataGridView.AllowUserToDeleteRows = false;
            this.productQtDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productQtDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productQtDataGridView.Location = new System.Drawing.Point(0, 2);
            this.productQtDataGridView.Name = "productQtDataGridView";
            this.productQtDataGridView.RowTemplate.Height = 23;
            this.productQtDataGridView.Size = new System.Drawing.Size(954, 278);
            this.productQtDataGridView.TabIndex = 115;
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
            // addDataButton
            // 
            this.addDataButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.addDataButton.FlatAppearance.BorderSize = 0;
            this.addDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addDataButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.addDataButton.ForeColor = System.Drawing.Color.White;
            this.addDataButton.Location = new System.Drawing.Point(817, 740);
            this.addDataButton.Name = "addDataButton";
            this.addDataButton.Size = new System.Drawing.Size(82, 35);
            this.addDataButton.TabIndex = 0;
            this.addDataButton.Text = "검사완료";
            this.addDataButton.UseVisualStyleBackColor = false;
            this.addDataButton.Click += new System.EventHandler(this.addDataButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(904, 740);
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
            this.panel1.Controls.Add(this.testItemTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.compatibilityOxComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.testScoreTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.deleteTestButton);
            this.panel1.Controls.Add(this.addTestButton);
            this.panel1.Controls.Add(this.testMethodTextBox);
            this.panel1.Controls.Add(this.productQtPackingDataGridView);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(20, 315);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(954, 410);
            this.panel1.TabIndex = 0;
            // 
            // testItemTextBox
            // 
            this.testItemTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testItemTextBox.Location = new System.Drawing.Point(92, 11);
            this.testItemTextBox.Name = "testItemTextBox";
            this.testItemTextBox.Size = new System.Drawing.Size(744, 25);
            this.testItemTextBox.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label3.Location = new System.Drawing.Point(672, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 215;
            this.label3.Text = "적합여부";
            // 
            // compatibilityOxComboBox
            // 
            this.compatibilityOxComboBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.compatibilityOxComboBox.FormattingEnabled = true;
            this.compatibilityOxComboBox.Location = new System.Drawing.Point(745, 72);
            this.compatibilityOxComboBox.Name = "compatibilityOxComboBox";
            this.compatibilityOxComboBox.Size = new System.Drawing.Size(91, 23);
            this.compatibilityOxComboBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(15, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 213;
            this.label2.Text = "포장성적";
            // 
            // testScoreTextBox
            // 
            this.testScoreTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testScoreTextBox.Location = new System.Drawing.Point(92, 72);
            this.testScoreTextBox.Name = "testScoreTextBox";
            this.testScoreTextBox.Size = new System.Drawing.Size(554, 25);
            this.testScoreTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(15, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 211;
            this.label1.Text = "포장방법";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label5.Location = new System.Drawing.Point(15, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 210;
            this.label5.Text = "포장항목";
            // 
            // deleteTestButton
            // 
            this.deleteTestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.deleteTestButton.FlatAppearance.BorderSize = 0;
            this.deleteTestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteTestButton.Font = new System.Drawing.Font("Malgun Gothic", 9.75F);
            this.deleteTestButton.ForeColor = System.Drawing.Color.White;
            this.deleteTestButton.Location = new System.Drawing.Point(868, 60);
            this.deleteTestButton.Name = "deleteTestButton";
            this.deleteTestButton.Size = new System.Drawing.Size(70, 35);
            this.deleteTestButton.TabIndex = 5;
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
            this.addTestButton.TabIndex = 4;
            this.addTestButton.Text = "추 가";
            this.addTestButton.UseVisualStyleBackColor = false;
            this.addTestButton.Click += new System.EventHandler(this.addTestButton_Click);
            // 
            // testMethodTextBox
            // 
            this.testMethodTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.testMethodTextBox.Location = new System.Drawing.Point(92, 41);
            this.testMethodTextBox.Name = "testMethodTextBox";
            this.testMethodTextBox.Size = new System.Drawing.Size(744, 25);
            this.testMethodTextBox.TabIndex = 1;
            // 
            // productQtPackingDataGridView
            // 
            this.productQtPackingDataGridView.AllowUserToAddRows = false;
            this.productQtPackingDataGridView.AllowUserToDeleteRows = false;
            this.productQtPackingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productQtPackingDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.productQtPackingDataGridView.Location = new System.Drawing.Point(0, 114);
            this.productQtPackingDataGridView.Name = "productQtPackingDataGridView";
            this.productQtPackingDataGridView.RowTemplate.Height = 23;
            this.productQtPackingDataGridView.Size = new System.Drawing.Size(954, 296);
            this.productQtPackingDataGridView.TabIndex = 116;
            // 
            // RunQualityTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(998, 796);
            this.ControlBox = false;
            this.Controls.Add(this.testFormatPanel);
            this.Controls.Add(this.addDataButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RunQualityTestForm";
            this.Text = "완제품 품질관리 포장검사 실행";
            this.Load += new System.EventHandler(this.RunProductTestForm_Load);
            this.testFormatPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.productQtDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productQtPackingDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel testFormatPanel;
        private System.Windows.Forms.DataGridView productQtDataGridView;
        private System.Windows.Forms.Panel testFormatPanelTopBorder;
        private System.Windows.Forms.Button addDataButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView productQtPackingDataGridView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox compatibilityOxComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox testScoreTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button deleteTestButton;
        private System.Windows.Forms.Button addTestButton;
        private System.Windows.Forms.TextBox testMethodTextBox;
        private System.Windows.Forms.TextBox testItemTextBox;
    }
}