namespace QTRS
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.reportMenuButton = new System.Windows.Forms.RadioButton();
            this.packingTestMenuButton = new System.Windows.Forms.RadioButton();
            this.settingMenuButton = new System.Windows.Forms.RadioButton();
            this.noticeMenuButton = new System.Windows.Forms.RadioButton();
            this.analysisMenuButton = new System.Windows.Forms.RadioButton();
            this.productTestMenuButton = new System.Windows.Forms.RadioButton();
            this.componentTestMenuButton = new System.Windows.Forms.RadioButton();
            this.importInspectionMenuButton = new System.Windows.Forms.RadioButton();
            this.mainMenuButton = new System.Windows.Forms.RadioButton();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userInfoPanel = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.fullScreenButton = new System.Windows.Forms.Button();
            this.minimizeScreenButton = new System.Windows.Forms.Button();
            this.helpPictureBox = new System.Windows.Forms.PictureBox();
            this.currentTimeLabel = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currentTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.titlePanel.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.userInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.helpPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.contentPanel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.titlePanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1270, 890);
            this.panel1.TabIndex = 0;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(168, 93);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1100, 795);
            this.contentPanel.TabIndex = 5;
            this.contentPanel.Resize += new System.EventHandler(this.contentPanel_Resize);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.reportMenuButton);
            this.panel3.Controls.Add(this.packingTestMenuButton);
            this.panel3.Controls.Add(this.settingMenuButton);
            this.panel3.Controls.Add(this.noticeMenuButton);
            this.panel3.Controls.Add(this.analysisMenuButton);
            this.panel3.Controls.Add(this.productTestMenuButton);
            this.panel3.Controls.Add(this.componentTestMenuButton);
            this.panel3.Controls.Add(this.importInspectionMenuButton);
            this.panel3.Controls.Add(this.mainMenuButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.panel3.Location = new System.Drawing.Point(0, 93);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(168, 795);
            this.panel3.TabIndex = 4;
            // 
            // reportMenuButton
            // 
            this.reportMenuButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.reportMenuButton.FlatAppearance.BorderSize = 0;
            this.reportMenuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.reportMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.reportMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.reportMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportMenuButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.reportMenuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.reportMenuButton.Image = global::QTRS.Properties.Resources.menu_report;
            this.reportMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.reportMenuButton.Location = new System.Drawing.Point(0, 252);
            this.reportMenuButton.Name = "reportMenuButton";
            this.reportMenuButton.Size = new System.Drawing.Size(168, 53);
            this.reportMenuButton.TabIndex = 10;
            this.reportMenuButton.Text = "  Report\r\n  (리포트)";
            this.reportMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.reportMenuButton.UseVisualStyleBackColor = true;
            this.reportMenuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // packingTestMenuButton
            // 
            this.packingTestMenuButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.packingTestMenuButton.FlatAppearance.BorderSize = 0;
            this.packingTestMenuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.packingTestMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.packingTestMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.packingTestMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.packingTestMenuButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.packingTestMenuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.packingTestMenuButton.Image = global::QTRS.Properties.Resources.menu_packing;
            this.packingTestMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.packingTestMenuButton.Location = new System.Drawing.Point(-1, 522);
            this.packingTestMenuButton.Name = "packingTestMenuButton";
            this.packingTestMenuButton.Size = new System.Drawing.Size(168, 53);
            this.packingTestMenuButton.TabIndex = 9;
            this.packingTestMenuButton.Text = "  Packing Test";
            this.packingTestMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.packingTestMenuButton.UseVisualStyleBackColor = true;
            this.packingTestMenuButton.Visible = false;
            this.packingTestMenuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // settingMenuButton
            // 
            this.settingMenuButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.settingMenuButton.FlatAppearance.BorderSize = 0;
            this.settingMenuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.settingMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.settingMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.settingMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingMenuButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.settingMenuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.settingMenuButton.Image = global::QTRS.Properties.Resources.menu_setting;
            this.settingMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.settingMenuButton.Location = new System.Drawing.Point(0, 411);
            this.settingMenuButton.Name = "settingMenuButton";
            this.settingMenuButton.Size = new System.Drawing.Size(168, 53);
            this.settingMenuButton.TabIndex = 8;
            this.settingMenuButton.Text = "  Setting\r\n  (설정)";
            this.settingMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.settingMenuButton.UseVisualStyleBackColor = true;
            this.settingMenuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // noticeMenuButton
            // 
            this.noticeMenuButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.noticeMenuButton.FlatAppearance.BorderSize = 0;
            this.noticeMenuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.noticeMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.noticeMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.noticeMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noticeMenuButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.noticeMenuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.noticeMenuButton.Image = global::QTRS.Properties.Resources.menu_notice;
            this.noticeMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.noticeMenuButton.Location = new System.Drawing.Point(0, 358);
            this.noticeMenuButton.Name = "noticeMenuButton";
            this.noticeMenuButton.Size = new System.Drawing.Size(168, 53);
            this.noticeMenuButton.TabIndex = 7;
            this.noticeMenuButton.Text = "  Notice\r\n  (알림)";
            this.noticeMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.noticeMenuButton.UseVisualStyleBackColor = true;
            this.noticeMenuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // analysisMenuButton
            // 
            this.analysisMenuButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.analysisMenuButton.Enabled = false;
            this.analysisMenuButton.FlatAppearance.BorderSize = 0;
            this.analysisMenuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.analysisMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.analysisMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.analysisMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.analysisMenuButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.analysisMenuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.analysisMenuButton.Image = global::QTRS.Properties.Resources.menu_analysis;
            this.analysisMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.analysisMenuButton.Location = new System.Drawing.Point(0, 305);
            this.analysisMenuButton.Name = "analysisMenuButton";
            this.analysisMenuButton.Size = new System.Drawing.Size(168, 53);
            this.analysisMenuButton.TabIndex = 6;
            this.analysisMenuButton.Text = "  Analysis\r\n  (분석)";
            this.analysisMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.analysisMenuButton.UseVisualStyleBackColor = true;
            this.analysisMenuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // productTestMenuButton
            // 
            this.productTestMenuButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.productTestMenuButton.FlatAppearance.BorderSize = 0;
            this.productTestMenuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.productTestMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.productTestMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.productTestMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.productTestMenuButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.productTestMenuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.productTestMenuButton.Image = global::QTRS.Properties.Resources.menu_finishedProduct;
            this.productTestMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.productTestMenuButton.Location = new System.Drawing.Point(0, 199);
            this.productTestMenuButton.Name = "productTestMenuButton";
            this.productTestMenuButton.Size = new System.Drawing.Size(168, 53);
            this.productTestMenuButton.TabIndex = 5;
            this.productTestMenuButton.Text = "  Product Test\r\n  (완제품 검사)";
            this.productTestMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.productTestMenuButton.UseVisualStyleBackColor = true;
            this.productTestMenuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // componentTestMenuButton
            // 
            this.componentTestMenuButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.componentTestMenuButton.FlatAppearance.BorderSize = 0;
            this.componentTestMenuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.componentTestMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.componentTestMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.componentTestMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.componentTestMenuButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.componentTestMenuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.componentTestMenuButton.Image = global::QTRS.Properties.Resources.menu_rawMaterial;
            this.componentTestMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.componentTestMenuButton.Location = new System.Drawing.Point(0, 146);
            this.componentTestMenuButton.Name = "componentTestMenuButton";
            this.componentTestMenuButton.Size = new System.Drawing.Size(168, 53);
            this.componentTestMenuButton.TabIndex = 4;
            this.componentTestMenuButton.Text = "  Component Test\r\n  (원료약품 검사)";
            this.componentTestMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.componentTestMenuButton.UseVisualStyleBackColor = true;
            this.componentTestMenuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // importInspectionMenuButton
            // 
            this.importInspectionMenuButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.importInspectionMenuButton.FlatAppearance.BorderSize = 0;
            this.importInspectionMenuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.importInspectionMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.importInspectionMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.importInspectionMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.importInspectionMenuButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.importInspectionMenuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.importInspectionMenuButton.Image = global::QTRS.Properties.Resources.menu_dataManagement;
            this.importInspectionMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.importInspectionMenuButton.Location = new System.Drawing.Point(0, 93);
            this.importInspectionMenuButton.Name = "importInspectionMenuButton";
            this.importInspectionMenuButton.Size = new System.Drawing.Size(168, 53);
            this.importInspectionMenuButton.TabIndex = 3;
            this.importInspectionMenuButton.Text = "  Import Inspection\r\n  (원표약품 입고목록)";
            this.importInspectionMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.importInspectionMenuButton.UseVisualStyleBackColor = true;
            this.importInspectionMenuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // mainMenuButton
            // 
            this.mainMenuButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.mainMenuButton.Checked = true;
            this.mainMenuButton.FlatAppearance.BorderSize = 0;
            this.mainMenuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.mainMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.mainMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.mainMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mainMenuButton.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mainMenuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.mainMenuButton.Image = global::QTRS.Properties.Resources.menu_main;
            this.mainMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mainMenuButton.Location = new System.Drawing.Point(0, 40);
            this.mainMenuButton.Name = "mainMenuButton";
            this.mainMenuButton.Size = new System.Drawing.Size(168, 53);
            this.mainMenuButton.TabIndex = 2;
            this.mainMenuButton.TabStop = true;
            this.mainMenuButton.Text = "  Main\r\n  (메인화면)";
            this.mainMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.mainMenuButton.UseVisualStyleBackColor = true;
            this.mainMenuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // titlePanel
            // 
            this.titlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(126)))), ((int)(((byte)(181)))));
            this.titlePanel.Controls.Add(this.panel5);
            this.titlePanel.Controls.Add(this.userInfoPanel);
            this.titlePanel.Controls.Add(this.label1);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(1268, 93);
            this.titlePanel.TabIndex = 3;
            this.titlePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlePanel_MouseDown);
            this.titlePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlePanel_MouseMove);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(168, 93);
            this.panel5.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::QTRS.Properties.Resources.QTRS_main_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(23, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 30);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // userInfoPanel
            // 
            this.userInfoPanel.Controls.Add(this.closeButton);
            this.userInfoPanel.Controls.Add(this.fullScreenButton);
            this.userInfoPanel.Controls.Add(this.minimizeScreenButton);
            this.userInfoPanel.Controls.Add(this.helpPictureBox);
            this.userInfoPanel.Controls.Add(this.currentTimeLabel);
            this.userInfoPanel.Controls.Add(this.pictureBox3);
            this.userInfoPanel.Controls.Add(this.userNameLabel);
            this.userInfoPanel.Controls.Add(this.pictureBox2);
            this.userInfoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.userInfoPanel.Location = new System.Drawing.Point(846, 0);
            this.userInfoPanel.Name = "userInfoPanel";
            this.userInfoPanel.Size = new System.Drawing.Size(422, 93);
            this.userInfoPanel.TabIndex = 3;
            this.userInfoPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlePanel_MouseDown);
            this.userInfoPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlePanel_MouseMove);
            // 
            // closeButton
            // 
            this.closeButton.BackgroundImage = global::QTRS.Properties.Resources.close_button_18;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(396, 8);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(18, 18);
            this.closeButton.TabIndex = 11;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // fullScreenButton
            // 
            this.fullScreenButton.BackgroundImage = global::QTRS.Properties.Resources.full_screen_18;
            this.fullScreenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.fullScreenButton.FlatAppearance.BorderSize = 0;
            this.fullScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fullScreenButton.Location = new System.Drawing.Point(374, 8);
            this.fullScreenButton.Margin = new System.Windows.Forms.Padding(0);
            this.fullScreenButton.Name = "fullScreenButton";
            this.fullScreenButton.Size = new System.Drawing.Size(18, 18);
            this.fullScreenButton.TabIndex = 10;
            this.fullScreenButton.UseVisualStyleBackColor = true;
            this.fullScreenButton.Click += new System.EventHandler(this.fullScreenButton_Click);
            // 
            // minimizeScreenButton
            // 
            this.minimizeScreenButton.BackgroundImage = global::QTRS.Properties.Resources.minimize_screen_18;
            this.minimizeScreenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.minimizeScreenButton.FlatAppearance.BorderSize = 0;
            this.minimizeScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeScreenButton.Location = new System.Drawing.Point(352, 8);
            this.minimizeScreenButton.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeScreenButton.Name = "minimizeScreenButton";
            this.minimizeScreenButton.Size = new System.Drawing.Size(18, 18);
            this.minimizeScreenButton.TabIndex = 9;
            this.minimizeScreenButton.UseVisualStyleBackColor = true;
            this.minimizeScreenButton.Click += new System.EventHandler(this.minimizeScreenButton_Click);
            // 
            // helpPictureBox
            // 
            this.helpPictureBox.BackgroundImage = global::QTRS.Properties.Resources.main_help;
            this.helpPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.helpPictureBox.Location = new System.Drawing.Point(353, 38);
            this.helpPictureBox.Name = "helpPictureBox";
            this.helpPictureBox.Size = new System.Drawing.Size(19, 19);
            this.helpPictureBox.TabIndex = 8;
            this.helpPictureBox.TabStop = false;
            this.helpPictureBox.Click += new System.EventHandler(this.helpPictureBox_Click);
            // 
            // currentTimeLabel
            // 
            this.currentTimeLabel.AutoSize = true;
            this.currentTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.currentTimeLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentTimeLabel.ForeColor = System.Drawing.Color.White;
            this.currentTimeLabel.Location = new System.Drawing.Point(132, 37);
            this.currentTimeLabel.Name = "currentTimeLabel";
            this.currentTimeLabel.Size = new System.Drawing.Size(199, 22);
            this.currentTimeLabel.TabIndex = 7;
            this.currentTimeLabel.Text = "2018. 12. 20 10:10:10";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::QTRS.Properties.Resources.main_time;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(109, 38);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(19, 19);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.userNameLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameLabel.ForeColor = System.Drawing.Color.White;
            this.userNameLabel.Location = new System.Drawing.Point(36, 37);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(50, 22);
            this.userNameLabel.TabIndex = 5;
            this.userNameLabel.Text = "User";
            this.userNameLabel.Click += new System.EventHandler(this.userNameLabel_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::QTRS.Properties.Resources.main_user;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(14, 40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 15);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(193, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Quality Test Report System v1.15.0";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlePanel_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlePanel_MouseMove);
            // 
            // currentTimer
            // 
            this.currentTimer.Interval = 1000;
            this.currentTimer.Tick += new System.EventHandler(this.currentTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 890);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Quality Test Report System";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.userInfoPanel.ResumeLayout(false);
            this.userInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.helpPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton mainMenuButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton settingMenuButton;
        private System.Windows.Forms.RadioButton noticeMenuButton;
        private System.Windows.Forms.RadioButton analysisMenuButton;
        private System.Windows.Forms.RadioButton productTestMenuButton;
        private System.Windows.Forms.RadioButton componentTestMenuButton;
        private System.Windows.Forms.RadioButton importInspectionMenuButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel userInfoPanel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.PictureBox helpPictureBox;
        private System.Windows.Forms.Label currentTimeLabel;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Timer currentTimer;
        private System.Windows.Forms.RadioButton packingTestMenuButton;
        private System.Windows.Forms.RadioButton reportMenuButton;
        private System.Windows.Forms.Button minimizeScreenButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button fullScreenButton;
        public System.Windows.Forms.Panel contentPanel;
    }
}

