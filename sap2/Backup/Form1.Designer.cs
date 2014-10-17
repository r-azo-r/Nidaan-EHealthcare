namespace ReportsApplication1
{
    partial class Form1
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.testcentreBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ByArea = new ReportsApplication1.ByArea();
            this.locationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ByTime = new ReportsApplication1.ByTime();
            this.disease_incidenceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ByDisease = new ReportsApplication1.ByDisease();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.testcentreTableAdapter = new ReportsApplication1.ByAreaTableAdapters.testcentreTableAdapter();
            this.locationTableAdapter = new ReportsApplication1.ByAreaTableAdapters.locationTableAdapter();
            this.disease_incidenceTableAdapter = new ReportsApplication1.ByDiseaseTableAdapters.disease_incidenceTableAdapter();
            this.timeTableAdapter = new ReportsApplication1.ByTimeTableAdapters.TimeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.testcentreBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.disease_incidenceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByDisease)).BeginInit();
            this.SuspendLayout();
            // 
            // testcentreBindingSource
            // 
            this.testcentreBindingSource.DataMember = "testcentre";
            this.testcentreBindingSource.DataSource = this.ByArea;
            // 
            // ByArea
            // 
            this.ByArea.DataSetName = "ByArea";
            this.ByArea.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // locationBindingSource
            // 
            this.locationBindingSource.DataMember = "location";
            this.locationBindingSource.DataSource = this.ByArea;
            // 
            // timeBindingSource
            // 
            this.timeBindingSource.DataMember = "Time";
            this.timeBindingSource.DataSource = this.ByTime;
            // 
            // ByTime
            // 
            this.ByTime.DataSetName = "ByTime";
            this.ByTime.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // disease_incidenceBindingSource
            // 
            this.disease_incidenceBindingSource.DataMember = "disease_incidence";
            this.disease_incidenceBindingSource.DataSource = this.ByDisease;
            // 
            // ByDisease
            // 
            this.ByDisease.DataSetName = "ByDisease";
            this.ByDisease.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ByArea_testcentre";
            reportDataSource1.Value = this.testcentreBindingSource;
            reportDataSource2.Name = "ByArea_location";
            reportDataSource2.Value = this.locationBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ReportsApplication1.ByArea.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(682, 386);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // testcentreTableAdapter
            // 
            this.testcentreTableAdapter.ClearBeforeFill = true;
            // 
            // locationTableAdapter
            // 
            this.locationTableAdapter.ClearBeforeFill = true;
            // 
            // disease_incidenceTableAdapter
            // 
            this.disease_incidenceTableAdapter.ClearBeforeFill = true;
            // 
            // timeTableAdapter
            // 
            this.timeTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 386);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.testcentreBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.disease_incidenceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByDisease)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource testcentreBindingSource;
        private ByArea ByArea;
        private System.Windows.Forms.BindingSource locationBindingSource;
        private ReportsApplication1.ByAreaTableAdapters.testcentreTableAdapter testcentreTableAdapter;
        private ReportsApplication1.ByAreaTableAdapters.locationTableAdapter locationTableAdapter;
        private ByDisease ByDisease;
        private System.Windows.Forms.BindingSource disease_incidenceBindingSource;
        private ReportsApplication1.ByDiseaseTableAdapters.disease_incidenceTableAdapter disease_incidenceTableAdapter;
        private ByTime ByTime;
        private System.Windows.Forms.BindingSource timeBindingSource;
        private ReportsApplication1.ByTimeTableAdapters.TimeTableAdapter timeTableAdapter;
    }
}

