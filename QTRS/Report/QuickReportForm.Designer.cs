namespace QTRS.Report
{
    partial class QuickReportForm
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.productQtTestDataSource1 = new QTRS.Report.ProductQtTestDataSource();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.saveReportButton = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.lotModifySaveButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.QTRSFlexViewer = new C1.Win.FlexViewer.C1FlexViewer();
            this.ProductQtTestDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ProductQtTestDataSource = new QTRS.Report.ProductQtTestDataSource();
            this.productQtTestHeaderDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ComponentDrugTestHeaderDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ComponentDrugTestDataSource = new QTRS.Report.ComponentDrugTestDataSource();
            this.ComponentDrugTestContentDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ProductMfTestHeaderDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ProductMfTestDataSource = new QTRS.Report.ProductMfTestDataSource();
            this.ProductMfTestContentDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FinalProductQtTestDataSource = new QTRS.Report.FinalProductQtTestDataSource();
            this.finalProductQtTestDataSource1 = new QTRS.Report.FinalProductQtTestDataSource();
            this.bindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.productMfTestDataSource1 = new QTRS.Report.ProductMfTestDataSource();
            this.bindingSource4 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource5 = new System.Windows.Forms.BindingSource(this.components);
            this.componentDrugTestDataSource1 = new QTRS.Report.ComponentDrugTestDataSource();
            this.bindingSource6 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productQtTestDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.topPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QTRSFlexViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductQtTestDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductQtTestDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productQtTestHeaderDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestHeaderDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestContentDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestHeaderDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestContentDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinalProductQtTestDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalProductQtTestDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productMfTestDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentDrugTestDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource6)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "ProductQtTestDataSet";
            this.bindingSource1.DataSource = this.productQtTestDataSource1;
            // 
            // productQtTestDataSource1
            // 
            this.productQtTestDataSource1.DataSetName = "ProductQtTestDataSource";
            this.productQtTestDataSource1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataMember = "ProductQtTestHeaderDataSet";
            this.bindingSource2.DataSource = this.productQtTestDataSource1;
            // 
            // saveReportButton
            // 
            this.saveReportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.saveReportButton.FlatAppearance.BorderSize = 0;
            this.saveReportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveReportButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.saveReportButton.ForeColor = System.Drawing.Color.White;
            this.saveReportButton.Location = new System.Drawing.Point(1454, 12);
            this.saveReportButton.Name = "saveReportButton";
            this.saveReportButton.Size = new System.Drawing.Size(101, 24);
            this.saveReportButton.TabIndex = 106;
            this.saveReportButton.Text = "리포트 저장";
            this.saveReportButton.UseVisualStyleBackColor = false;
            this.saveReportButton.Visible = false;
            this.saveReportButton.Click += new System.EventHandler(this.saveReportButton_Click);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.lotModifySaveButton);
            this.topPanel.Controls.Add(this.saveReportButton);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1584, 50);
            this.topPanel.TabIndex = 25;
            // 
            // lotModifySaveButton
            // 
            this.lotModifySaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(158)))), ((int)(((byte)(187)))));
            this.lotModifySaveButton.FlatAppearance.BorderSize = 0;
            this.lotModifySaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lotModifySaveButton.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lotModifySaveButton.ForeColor = System.Drawing.Color.White;
            this.lotModifySaveButton.Location = new System.Drawing.Point(39, 12);
            this.lotModifySaveButton.Name = "lotModifySaveButton";
            this.lotModifySaveButton.Size = new System.Drawing.Size(101, 24);
            this.lotModifySaveButton.TabIndex = 107;
            this.lotModifySaveButton.Text = "Lot 편집";
            this.lotModifySaveButton.UseVisualStyleBackColor = false;
            this.lotModifySaveButton.Visible = false;
            this.lotModifySaveButton.Click += new System.EventHandler(this.MianLotChange_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.QTRSFlexViewer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1584, 991);
            this.panel1.TabIndex = 27;
            // 
            // QTRSFlexViewer
            // 
            this.QTRSFlexViewer.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.QTRSFlexViewer.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.QTRSFlexViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QTRSFlexViewer.Location = new System.Drawing.Point(0, 0);
            this.QTRSFlexViewer.Name = "QTRSFlexViewer";
            this.QTRSFlexViewer.Size = new System.Drawing.Size(1584, 991);
            this.QTRSFlexViewer.TabIndex = 0;
            // 
            // ProductQtTestDataSetBindingSource
            // 
            this.ProductQtTestDataSetBindingSource.DataMember = "ProductQtTestDataSet";
            this.ProductQtTestDataSetBindingSource.DataSource = this.ProductQtTestDataSource;
            // 
            // ProductQtTestDataSource
            // 
            this.ProductQtTestDataSource.DataSetName = "ProductQtTestDataSource";
            this.ProductQtTestDataSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productQtTestHeaderDataSetBindingSource
            // 
            this.productQtTestHeaderDataSetBindingSource.DataMember = "ProductQtTestHeaderDataSet";
            this.productQtTestHeaderDataSetBindingSource.DataSource = this.ProductQtTestDataSource;
            // 
            // ComponentDrugTestHeaderDataSetBindingSource
            // 
            this.ComponentDrugTestHeaderDataSetBindingSource.DataMember = "ComponentDrugTestHeaderDataSet";
            this.ComponentDrugTestHeaderDataSetBindingSource.DataSource = this.ComponentDrugTestDataSource;
            // 
            // ComponentDrugTestDataSource
            // 
            this.ComponentDrugTestDataSource.DataSetName = "ComponentDrugTestDataSource";
            this.ComponentDrugTestDataSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ComponentDrugTestContentDataSetBindingSource
            // 
            this.ComponentDrugTestContentDataSetBindingSource.DataMember = "ComponentDrugTestContentDataSet";
            this.ComponentDrugTestContentDataSetBindingSource.DataSource = this.ComponentDrugTestDataSource;
            // 
            // ProductMfTestHeaderDataSetBindingSource
            // 
            this.ProductMfTestHeaderDataSetBindingSource.DataMember = "ProductMfTestHeaderDataSet";
            this.ProductMfTestHeaderDataSetBindingSource.DataSource = this.ProductMfTestDataSource;
            // 
            // ProductMfTestDataSource
            // 
            this.ProductMfTestDataSource.DataSetName = "ProductMfTestDataSource";
            this.ProductMfTestDataSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ProductMfTestContentDataSetBindingSource
            // 
            this.ProductMfTestContentDataSetBindingSource.DataMember = "ProductMfTestContentDataSet";
            this.ProductMfTestContentDataSetBindingSource.DataSource = this.ProductMfTestDataSource;
            // 
            // FinalProductQtTestDataSource
            // 
            this.FinalProductQtTestDataSource.DataSetName = "FinalProductQtTestDataSource";
            this.FinalProductQtTestDataSource.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // finalProductQtTestDataSource1
            // 
            this.finalProductQtTestDataSource1.DataSetName = "FinalProductQtTestDataSource";
            this.finalProductQtTestDataSource1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource3
            // 
            this.bindingSource3.DataMember = "ProductMfTestContentDataSet";
            this.bindingSource3.DataSource = this.productMfTestDataSource1;
            // 
            // productMfTestDataSource1
            // 
            this.productMfTestDataSource1.DataSetName = "ProductMfTestDataSource";
            this.productMfTestDataSource1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource4
            // 
            this.bindingSource4.DataMember = "ProductMfTestHeaderDataSet";
            this.bindingSource4.DataSource = this.productMfTestDataSource1;
            // 
            // bindingSource5
            // 
            this.bindingSource5.DataMember = "ComponentDrugTestContentDataSet";
            this.bindingSource5.DataSource = this.componentDrugTestDataSource1;
            // 
            // componentDrugTestDataSource1
            // 
            this.componentDrugTestDataSource1.DataSetName = "ComponentDrugTestDataSource";
            this.componentDrugTestDataSource1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource6
            // 
            this.bindingSource6.DataMember = "ComponentDrugTestHeaderDataSet";
            this.bindingSource6.DataSource = this.componentDrugTestDataSource1;
            // 
            // QuickReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 1041);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.Name = "QuickReportForm";
            this.Text = "QuickReportForm";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productQtTestDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.QTRSFlexViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductQtTestDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductQtTestDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productQtTestHeaderDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestHeaderDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentDrugTestContentDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestHeaderDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductMfTestContentDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinalProductQtTestDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalProductQtTestDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productMfTestDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentDrugTestDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource ProductQtTestDataSetBindingSource;
        private ProductQtTestDataSource ProductQtTestDataSource;
        private System.Windows.Forms.BindingSource productQtTestHeaderDataSetBindingSource;
        private System.Windows.Forms.BindingSource ComponentDrugTestHeaderDataSetBindingSource;
        private ComponentDrugTestDataSource ComponentDrugTestDataSource;
        private System.Windows.Forms.BindingSource ComponentDrugTestContentDataSetBindingSource;
        private System.Windows.Forms.BindingSource ProductMfTestHeaderDataSetBindingSource;
        private ProductMfTestDataSource ProductMfTestDataSource;
        private System.Windows.Forms.BindingSource ProductMfTestContentDataSetBindingSource;
        private FinalProductQtTestDataSource FinalProductQtTestDataSource;
        private FinalProductQtTestDataSource finalProductQtTestDataSource1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private ProductQtTestDataSource productQtTestDataSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.Button saveReportButton;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.BindingSource bindingSource3;
        private ProductMfTestDataSource productMfTestDataSource1;
        private System.Windows.Forms.BindingSource bindingSource4;
        private System.Windows.Forms.BindingSource bindingSource5;
        private ComponentDrugTestDataSource componentDrugTestDataSource1;
        private System.Windows.Forms.BindingSource bindingSource6;
        private System.Windows.Forms.Button lotModifySaveButton;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.FlexViewer.C1FlexViewer QTRSFlexViewer;
    }
}