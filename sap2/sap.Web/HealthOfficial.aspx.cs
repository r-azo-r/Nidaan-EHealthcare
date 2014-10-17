using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using Microsoft.Reporting.WebForms;
using AKAV1;
namespace sap.Web
{
    public partial class HealthOfficial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ReportViewer1.Visible = false;
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = true;          
            rendereports rr = new rendereports();
            rr.reports(ReportViewer1);
        }
    }
    public class rendereports:System.Web.UI.ScriptManager
    {
        public void  reports(ReportViewer r1  )
        {

            r1.Visible = true;           
           // r1.AsyncRendering = true;
        }
    }
}