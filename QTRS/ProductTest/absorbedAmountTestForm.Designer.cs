namespace QTRS.ProductTest
{
    partial class AbsorbedAmountTestForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.absorbedAmountDataGridView = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyDataButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.absorbedAmountAverageTextBox = new QTRS.CustomControl.DigitTextBox();
            this.beforeAbsorbedAmountAverageTextBox = new QTRS.CustomControl.DigitTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.absorbedAmountDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.absorbedAmountAverageTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.beforeAbsorbedAmountAverageTextBox);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.absorbedAmountDataGridView);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(20, 20);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(667, 219);
            this.panel1.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(483, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 178;
            this.label1.Text = "흡수배";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label14.Location = new System.Drawing.Point(274, 185);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 15);
            this.label14.TabIndex = 176;
            this.label14.Text = "시험기준";
            // 
            // absorbedAmountDataGridView
            // 
            this.absorbedAmountDataGridView.AllowUserToAddRows = false;
            this.absorbedAmountDataGridView.AllowUserToDeleteRows = false;
            this.absorbedAmountDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.absorbedAmountDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.absorbedAmountDataGridView.Location = new System.Drawing.Point(0, 2);
            this.absorbedAmountDataGridView.Name = "absorbedAmountDataGridView";
            this.absorbedAmountDataGridView.RowTemplate.Height = 23;
            this.absorbedAmountDataGridView.Size = new System.Drawing.Size(667, 166);
            this.absorbedAmountDataGridView.TabIndex = 116;
            this.absorbedAmountDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.absorbedAmountDataGridView_CellEndEdit);
            this.absorbedAmountDataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.absorbedAmountDataGridView_CellLeave);
            this.absorbedAmountDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.absorbedAmountDataGridView_EditingControlShowing);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(667, 2);
            this.panel2.TabIndex = 13;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(601, 261);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 31);
            this.cancelButton.TabIndex = 30;
            this.cancelButton.Text = "취소";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // applyDataButton
            // 
            this.applyDataButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.applyDataButton.FlatAppearance.BorderSize = 0;
            this.applyDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyDataButton.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.applyDataButton.ForeColor = System.Drawing.Color.White;
            this.applyDataButton.Location = new System.Drawing.Point(509, 261);
            this.applyDataButton.Name = "applyDataButton";
            this.applyDataButton.Size = new System.Drawing.Size(86, 31);
            this.applyDataButton.TabIndex = 29;
            this.applyDataButton.Text = "적용";
            this.applyDataButton.UseVisualStyleBackColor = false;
            this.applyDataButton.Click += new System.EventHandler(this.applyDataButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(300, 261);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // absorbedAmountAverageTextBox
            // 
            this.absorbedAmountAverageTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.absorbedAmountAverageTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.absorbedAmountAverageTextBox.Location = new System.Drawing.Point(540, 180);
            this.absorbedAmountAverageTextBox.Name = "absorbedAmountAverageTextBox";
            this.absorbedAmountAverageTextBox.Size = new System.Drawing.Size(116, 25);
            this.absorbedAmountAverageTextBox.TabIndex = 177;
            // 
            // beforeAbsorbedAmountAverageTextBox
            // 
            this.beforeAbsorbedAmountAverageTextBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.beforeAbsorbedAmountAverageTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.beforeAbsorbedAmountAverageTextBox.Location = new System.Drawing.Point(345, 180);
            this.beforeAbsorbedAmountAverageTextBox.Name = "beforeAbsorbedAmountAverageTextBox";
            this.beforeAbsorbedAmountAverageTextBox.Size = new System.Drawing.Size(116, 25);
            this.beforeAbsorbedAmountAverageTextBox.TabIndex = 175;
            // 
            // AbsorbedAmountTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(706, 313);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyDataButton);
            this.Controls.Add(this.panel1);
            this.Name = "AbsorbedAmountTestForm";
            this.Text = "흡수량 테스트";
            this.Load += new System.EventHandler(this.absorbedAmountTestForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.absorbedAmountDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView absorbedAmountDataGridView;
        private CustomControl.DigitTextBox beforeAbsorbedAmountAverageTextBox;
        private System.Windows.Forms.Label label14;
        private CustomControl.DigitTextBox absorbedAmountAverageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyDataButton;
        private System.Windows.Forms.Button button1;
    }
}