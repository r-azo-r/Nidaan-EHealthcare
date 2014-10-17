using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sap.Web
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request["msg"]!=null)
                lblerror.Text=Request["msg"];
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int type=1;
            string url = "";
            string sessionvar = "";
            if (Request["rdotype"] != null)
                type = int.Parse(Request["rdotype"]);
           switch(type)
           {
               case 1:
               url = "Doctor.aspx";
               sessionvar = "DoctorLoginId";
               break;
               case 2:
               url = "Patient.aspx";
               sessionvar = "PatientId";
               break;
               case 3:
                url = "Kiosk.aspx";
                sessionvar = "KioskId";
                   break;
               case 4:
               url = "HealthOfficial.aspx";
               sessionvar = "HealthOfficalId";
               break;
           }

           if (AKAV1.Operations.login(Request["txtlogin"], Request["txtpassword"], type))
           {
               Session[sessionvar] = Request["txtlogin"];
               if (type == 2)
                   Session["PatientHVId"] = AKAV1.Operations.getPatientHVId(Request["txtlogin"]);
               Response.Redirect(url);
               
           }
           else
               Response.Redirect("HomePage.aspx?msg=Invalid Login");

        }
    }
}