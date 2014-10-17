using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
       
            String diseasename = "fever";
            // TODO: This line of code loads data into the 'ByDisease.disease_incidence' table. You can move, or remove it, as needed.
          this.disease_incidenceTableAdapter.Fill(this.ByDisease.disease_incidence,diseasename);
          this.timeTableAdapter.Fill(this.ByTime.Time, 1989, 3, 2); 
            // TODO: This line of code loads data into the 'ByArea.testcentre' table. You can move, or remove it, as needed.
          this.testcentreTableAdapter.Fill(this.ByArea.testcentre);
            // TODO: This line of code loads data into the 'ByArea.location' table. You can move, or remove it, as needed.
  
          this.locationTableAdapter.Fill(this.ByArea.location, "sindhisociety", "Mumbai", "Mumbai", "Maharashtra");          
          this.reportViewer1.RefreshReport();
            
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        
        }
    }
}