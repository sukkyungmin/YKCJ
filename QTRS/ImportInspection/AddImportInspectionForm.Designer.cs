namespace QTRS.ImportInspection
{
    partial class AddImportInspectionForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.addDataButton = new System.Windows.Forms.Button();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.componentCodeComboBox = new System.Windows.Forms.ComboBox();
            this.mainLotNoTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lotNoTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.makerTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.warehousingDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.componentNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.productAreaTypeComboBox = new System.Windows.Forms.ComboBox();
            this.innerComponentNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(403, 425);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 31);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "취소";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addDataButton
            // 
            this.addDataButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.addDataButton.FlatAppearance.BorderSize = 0;
            this.addDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addDataButton.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.addDataButton.ForeColor = System.Drawing.Color.White;
            this.addDataButton.Location = new System.Drawing.Point(311, 425);
            this.addDataButton.Name = "addDataButton";
            this.addDataButton.Size = new System.Drawing.Size(86, 31);
            this.addDataButton.TabIndex = 7;
            this.addDataButton.Text = "추가";
            this.addDataButton.UseVisualStyleBackColor = false;
            this.addDataButton.Click += new System.EventHandler(this.addDataButton_Click);
            // 
            // noteTextBox
            // 
            this.noteTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.noteTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.noteTextBox.Location = new System.Drawing.Point(139, 276);
            this.noteTextBox.Multiline = true;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(350, 116);
            this.noteTextBox.TabIndex = 6;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label19.Location = new System.Drawing.Point(40, 279);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 15);
            this.label19.TabIndex = 171;
            this.label19.Text = "비고";
            // 
            // componentCodeComboBox
            // 
            this.componentCodeComboBox.DropDownWidth = 700;
            this.componentCodeComboBox.Enabled = false;
            this.componentCodeComboBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.componentCodeComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.componentCodeComboBox.FormattingEnabled = true;
            this.componentCodeComboBox.Location = new System.Drawing.Point(139, 94);
            this.componentCodeComboBox.Name = "componentCodeComboBox";
            this.componentCodeComboBox.Size = new System.Drawing.Size(183, 23);
            this.componentCodeComboBox.TabIndex = 2;
            this.componentCodeComboBox.SelectedIndexChanged += new System.EventHandler(this.componentCodeComboBox_SelectedIndexChanged);
            // 
            // mainLotNoTextBox
            // 
            this.mainLotNoTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mainLotNoTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.mainLotNoTextBox.Location = new System.Drawing.Point(139, 245);
            this.mainLotNoTextBox.Name = "mainLotNoTextBox";
            this.mainLotNoTextBox.Size = new System.Drawing.Size(183, 25);
            this.mainLotNoTextBox.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label7.Location = new System.Drawing.Point(40, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 15);
            this.label7.TabIndex = 153;
            this.label7.Text = "Main LOT";
            // 
            // lotNoTextBox
            // 
            this.lotNoTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lotNoTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.lotNoTextBox.Location = new System.Drawing.Point(139, 215);
            this.lotNoTextBox.Name = "lotNoTextBox";
            this.lotNoTextBox.Size = new System.Drawing.Size(183, 25);
            this.lotNoTextBox.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label6.Location = new System.Drawing.Point(40, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 151;
            this.label6.Text = "LOT NO.";
            // 
            // makerTextBox
            // 
            this.makerTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.makerTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.makerTextBox.Location = new System.Drawing.Point(139, 185);
            this.makerTextBox.Name = "makerTextBox";
            this.makerTextBox.Size = new System.Drawing.Size(183, 25);
            this.makerTextBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label5.Location = new System.Drawing.Point(40, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 148;
            this.label5.Text = "메이커";
            // 
            // warehousingDateTimePicker
            // 
            this.warehousingDateTimePicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(57)))));
            this.warehousingDateTimePicker.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.warehousingDateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.warehousingDateTimePicker.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.warehousingDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.warehousingDateTimePicker.Location = new System.Drawing.Point(139, 35);
            this.warehousingDateTimePicker.Name = "warehousingDateTimePicker";
            this.warehousingDateTimePicker.Size = new System.Drawing.Size(183, 25);
            this.warehousingDateTimePicker.TabIndex = 0;
            this.warehousingDateTimePicker.Value = new System.DateTime(2016, 9, 28, 7, 54, 0, 0);
            // 
            // componentNameTextBox
            // 
            this.componentNameTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.componentNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.componentNameTextBox.Location = new System.Drawing.Point(139, 123);
            this.componentNameTextBox.Name = "componentNameTextBox";
            this.componentNameTextBox.ReadOnly = true;
            this.componentNameTextBox.Size = new System.Drawing.Size(183, 25);
            this.componentNameTextBox.TabIndex = 132;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label3.Location = new System.Drawing.Point(40, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 141;
            this.label3.Text = "원료 코드";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(40, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 138;
            this.label2.Text = "입고 일자";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(40, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 134;
            this.label1.Text = "원료 이름";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label23.Location = new System.Drawing.Point(40, 69);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(55, 15);
            this.label23.TabIndex = 177;
            this.label23.Text = "생산지";
            // 
            // productAreaTypeComboBox
            // 
            this.productAreaTypeComboBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.productAreaTypeComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.productAreaTypeComboBox.FormattingEnabled = true;
            this.productAreaTypeComboBox.Location = new System.Drawing.Point(139, 65);
            this.productAreaTypeComboBox.Name = "productAreaTypeComboBox";
            this.productAreaTypeComboBox.Size = new System.Drawing.Size(183, 23);
            this.productAreaTypeComboBox.TabIndex = 1;
            // 
            // innerComponentNameTextBox
            // 
            this.innerComponentNameTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.innerComponentNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.innerComponentNameTextBox.Location = new System.Drawing.Point(139, 154);
            this.innerComponentNameTextBox.Name = "innerComponentNameTextBox";
            this.innerComponentNameTextBox.ReadOnly = true;
            this.innerComponentNameTextBox.Size = new System.Drawing.Size(183, 25);
            this.innerComponentNameTextBox.TabIndex = 178;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label4.Location = new System.Drawing.Point(40, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 15);
            this.label4.TabIndex = 179;
            this.label4.Text = "내부원료이름";
            // 
            // AddImportInspectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(526, 485);
            this.Controls.Add(this.innerComponentNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.productAreaTypeComboBox);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addDataButton);
            this.Controls.Add(this.noteTextBox);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.componentCodeComboBox);
            this.Controls.Add(this.mainLotNoTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lotNoTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.makerTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.warehousingDateTimePicker);
            this.Controls.Add(this.componentNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddImportInspectionForm";
            this.Text = "수입 검사 추가";
            this.Load += new System.EventHandler(this.AddImportInspectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addDataButton;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox componentCodeComboBox;
        private System.Windows.Forms.TextBox mainLotNoTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox lotNoTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox makerTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker warehousingDateTimePicker;
        private System.Windows.Forms.TextBox componentNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox productAreaTypeComboBox;
        private System.Windows.Forms.TextBox innerComponentNameTextBox;
        private System.Windows.Forms.Label label4;
    }
}