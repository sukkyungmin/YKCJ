namespace QTRS.ProductTest
{
    partial class SerialPortSetup
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
            this.serialPortComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openSerialPortButton = new System.Windows.Forms.Button();
            this.closeSerialPortButton = new System.Windows.Forms.Button();
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serialPortComboBox
            // 
            this.serialPortComboBox.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.serialPortComboBox.FormattingEnabled = true;
            this.serialPortComboBox.Location = new System.Drawing.Point(42, 70);
            this.serialPortComboBox.Name = "serialPortComboBox";
            this.serialPortComboBox.Size = new System.Drawing.Size(147, 23);
            this.serialPortComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "저울과 연결된 시리얼 포트를 선택해 주십시오.";
            // 
            // openSerialPortButton
            // 
            this.openSerialPortButton.Location = new System.Drawing.Point(203, 70);
            this.openSerialPortButton.Name = "openSerialPortButton";
            this.openSerialPortButton.Size = new System.Drawing.Size(75, 23);
            this.openSerialPortButton.TabIndex = 1;
            this.openSerialPortButton.Text = "열기";
            this.openSerialPortButton.UseVisualStyleBackColor = true;
            this.openSerialPortButton.Click += new System.EventHandler(this.openSerialPortButton_Click);
            // 
            // closeSerialPortButton
            // 
            this.closeSerialPortButton.Location = new System.Drawing.Point(284, 70);
            this.closeSerialPortButton.Name = "closeSerialPortButton";
            this.closeSerialPortButton.Size = new System.Drawing.Size(75, 23);
            this.closeSerialPortButton.TabIndex = 2;
            this.closeSerialPortButton.Text = "닫기";
            this.closeSerialPortButton.UseVisualStyleBackColor = true;
            this.closeSerialPortButton.Click += new System.EventHandler(this.closeSerialPortButton_Click);
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Location = new System.Drawing.Point(108, 107);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(81, 12);
            this.connectionStatusLabel.TabIndex = 16;
            this.connectionStatusLabel.Text = "연결되지 않음";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "연결 상태 :";
            // 
            // SerialPortSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(398, 154);
            this.Controls.Add(this.connectionStatusLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.closeSerialPortButton);
            this.Controls.Add(this.openSerialPortButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serialPortComboBox);
            this.Name = "SerialPortSetup";
            this.Text = "시리얼 포트 연결";
            this.Load += new System.EventHandler(this.SerialPortSetup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox serialPortComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openSerialPortButton;
        private System.Windows.Forms.Button closeSerialPortButton;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.Label label2;
    }
}