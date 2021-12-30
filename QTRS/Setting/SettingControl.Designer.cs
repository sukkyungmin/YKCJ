namespace QTRS.Setting
{
    partial class SettingControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingControl));
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.memberMgtButton = new System.Windows.Forms.RadioButton();
            this.productMgtButton = new System.Windows.Forms.RadioButton();
            this.componentMgtButton = new System.Windows.Forms.RadioButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(127)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(48, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 19);
            this.label2.TabIndex = 22;
            this.label2.Text = "설정";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(40, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(2, 12);
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // memberMgtButton
            // 
            this.memberMgtButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.memberMgtButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.memberMgtButton.FlatAppearance.BorderSize = 0;
            this.memberMgtButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.memberMgtButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.memberMgtButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(219)))));
            this.memberMgtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.memberMgtButton.Font = new System.Drawing.Font("Dotum", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.memberMgtButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.memberMgtButton.Location = new System.Drawing.Point(799, 40);
            this.memberMgtButton.Name = "memberMgtButton";
            this.memberMgtButton.Size = new System.Drawing.Size(263, 63);
            this.memberMgtButton.TabIndex = 39;
            this.memberMgtButton.Text = "사용자 관리";
            this.memberMgtButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.memberMgtButton.UseVisualStyleBackColor = false;
            this.memberMgtButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // productMgtButton
            // 
            this.productMgtButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.productMgtButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.productMgtButton.FlatAppearance.BorderSize = 0;
            this.productMgtButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.productMgtButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.productMgtButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(219)))));
            this.productMgtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.productMgtButton.Font = new System.Drawing.Font("Dotum", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.productMgtButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.productMgtButton.Location = new System.Drawing.Point(530, 40);
            this.productMgtButton.Name = "productMgtButton";
            this.productMgtButton.Size = new System.Drawing.Size(263, 63);
            this.productMgtButton.TabIndex = 38;
            this.productMgtButton.Text = "완제품 데이터 관리";
            this.productMgtButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.productMgtButton.UseVisualStyleBackColor = false;
            this.productMgtButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // componentMgtButton
            // 
            this.componentMgtButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.componentMgtButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.componentMgtButton.Checked = true;
            this.componentMgtButton.FlatAppearance.BorderSize = 0;
            this.componentMgtButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.componentMgtButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.componentMgtButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(219)))));
            this.componentMgtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.componentMgtButton.Font = new System.Drawing.Font("Dotum", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.componentMgtButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.componentMgtButton.Location = new System.Drawing.Point(261, 40);
            this.componentMgtButton.Name = "componentMgtButton";
            this.componentMgtButton.Size = new System.Drawing.Size(263, 63);
            this.componentMgtButton.TabIndex = 37;
            this.componentMgtButton.TabStop = true;
            this.componentMgtButton.Text = "원료 데이터 관리";
            this.componentMgtButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.componentMgtButton.UseVisualStyleBackColor = false;
            this.componentMgtButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.Location = new System.Drawing.Point(0, 109);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1102, 686);
            this.contentPanel.TabIndex = 40;
            // 
            // SettingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.memberMgtButton);
            this.Controls.Add(this.productMgtButton);
            this.Controls.Add(this.componentMgtButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Name = "SettingControl";
            this.Size = new System.Drawing.Size(1102, 795);
            this.Load += new System.EventHandler(this.SettingControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton memberMgtButton;
        private System.Windows.Forms.RadioButton productMgtButton;
        private System.Windows.Forms.RadioButton componentMgtButton;
        private System.Windows.Forms.Panel contentPanel;
    }
}
