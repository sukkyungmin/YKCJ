namespace QTRS
{
    partial class CaptureForm
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
            this.captureButton = new System.Windows.Forms.Button();
            this.captureBox = new System.Windows.Forms.PictureBox();
            this.addFileButton = new System.Windows.Forms.Button();
            this.CapturecancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.captureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(0, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 129;
            // 
            // captureButton
            // 
            this.captureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.captureButton.FlatAppearance.BorderSize = 0;
            this.captureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.captureButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.captureButton.ForeColor = System.Drawing.Color.White;
            this.captureButton.Location = new System.Drawing.Point(358, 495);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(86, 31);
            this.captureButton.TabIndex = 0;
            this.captureButton.Text = "캡쳐";
            this.captureButton.UseVisualStyleBackColor = false;
            this.captureButton.Click += new System.EventHandler(this.captureButton_Click);
            // 
            // captureBox
            // 
            this.captureBox.BackColor = System.Drawing.Color.DimGray;
            this.captureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.captureBox.Location = new System.Drawing.Point(0, 0);
            this.captureBox.Name = "captureBox";
            this.captureBox.Size = new System.Drawing.Size(640, 480);
            this.captureBox.TabIndex = 128;
            this.captureBox.TabStop = false;
            // 
            // addFileButton
            // 
            this.addFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.addFileButton.FlatAppearance.BorderSize = 0;
            this.addFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addFileButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.addFileButton.ForeColor = System.Drawing.Color.White;
            this.addFileButton.Location = new System.Drawing.Point(450, 495);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(86, 31);
            this.addFileButton.TabIndex = 1;
            this.addFileButton.Text = "파일 추가";
            this.addFileButton.UseVisualStyleBackColor = false;
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // CapturecancelButton
            // 
            this.CapturecancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.CapturecancelButton.FlatAppearance.BorderSize = 0;
            this.CapturecancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CapturecancelButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CapturecancelButton.ForeColor = System.Drawing.Color.White;
            this.CapturecancelButton.Location = new System.Drawing.Point(542, 495);
            this.CapturecancelButton.Name = "CapturecancelButton";
            this.CapturecancelButton.Size = new System.Drawing.Size(86, 31);
            this.CapturecancelButton.TabIndex = 130;
            this.CapturecancelButton.Text = "나가기";
            this.CapturecancelButton.UseVisualStyleBackColor = false;
            this.CapturecancelButton.Click += new System.EventHandler(this.CapturecancelButton_Click);
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(640, 538);
            this.ControlBox = false;
            this.Controls.Add(this.CapturecancelButton);
            this.Controls.Add(this.addFileButton);
            this.Controls.Add(this.captureBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.captureButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CaptureForm";
            this.Text = "캡쳐";
            this.Load += new System.EventHandler(this.CaptureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.captureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.PictureBox captureBox;
        private System.Windows.Forms.Button addFileButton;
        private System.Windows.Forms.Button CapturecancelButton;
    }
}