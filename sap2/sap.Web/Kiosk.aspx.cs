using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Health;
using Microsoft.Health.Web;
using Microsoft.Health.ItemTypes;
using AKAV1;
namespace sap.Web
{
    public partial class Kiosk : HealthServicePage
    {
        static AKAV1.MedicalCase medcase=null;
        static TextBox[] answerBoxes = null;
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
           {
                populatePatientListBox();
                DropDownList1.SelectedIndex = 0;
                btnCreateNewCase.Enabled = false;
                btnSendCaseToDoctor.Enabled = false;
           }
           Session["KioskId"] = "TC1";
        
        }
        private void populateDiseasesDiagonized(AKAV1.MedicalCase medcase)
        {
            Table t;
            if (medcase.diseases != null)
            {
                t = new Table();
                TableRow tr;
                TableCell tc;
                TableHeaderRow thr = new TableHeaderRow();
                TableHeaderCell thc;
                thc = new TableHeaderCell();
                thc.Text = "Disease";
                thr.Cells.Add(thc);

                thc = new TableHeaderCell();
                thc.Text = "Doctor ID";
                thr.Cells.Add(thc);

                thc = new TableHeaderCell();
                thc.Text = "Diagonsis Date and Time";
                thr.Cells.Add(thc);

                t.Rows.Add(thr);
                t.Rows.Add(thr);
                for (int i = 0; i < medcase.diseases.Length; i++)
                {
                    tr = new TableRow();

                    tc = new TableCell();
                    tc.Text = medcase.diseases[i].disease;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = medcase.diseases[i].doctorId;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = medcase.diseases[i].diagonsisDateTime.ToString();
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);
                }
                DiseasesDiagonizedTab.Controls.Add(t);
            }
        }
        private void populatePrescriptionGiven(AKAV1.MedicalCase medcase)
        {
            if (medcase.prescriptions != null)
            {
                Table t = new Table();
                TableRow tr;
                TableCell tc;
                TableHeaderRow thr = new TableHeaderRow();
                TableHeaderCell thc;
                thc = new TableHeaderCell();
                thc.Text = "Medicine";
                thr.Cells.Add(thc);

                thc = new TableHeaderCell();
                thc.Text = "Quantity";
                thr.Cells.Add(thc);

                thc = new TableHeaderCell();
                thc.Text = "Doctor ID";
                thr.Cells.Add(thc);

                thc = new TableHeaderCell();
                thc.Text = "Prescription Date and Time";
                thr.Cells.Add(thc);

                t.Rows.Add(thr);
                t.Rows.Add(thr);
                for (int i = 0; i < medcase.prescriptions.Length; i++)
                {
                    tr = new TableRow();

                    tc = new TableCell();
                    tc.Text = medcase.prescriptions[i].medicine;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = medcase.prescriptions[i].quantity + medcase.prescriptions[i].unit;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = medcase.prescriptions[i].doctorId;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = medcase.prescriptions[i].prescriptionDateTime.ToString();
                    tr.Cells.Add(tc);

                    t.Rows.Add(tr);
                }
                MedicinesPrescribedTab.Controls.Add(t);

            }



        }
        private void populatePatientListBox()
        {
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add("--Select Patient--");
            foreach (Guid recordId in PersonInfo.AuthorizedRecords.Keys)
            {
                HealthRecordInfo healthRecordInfo = PersonInfo.AuthorizedRecords[recordId];
                ListItem listItem = new ListItem(healthRecordInfo.Name, recordId.ToString());
                if (recordId == PersonInfo.SelectedRecord.Id)
                {
                    listItem.Selected = true;
                } 

                DropDownList1.Items.Add(listItem);

            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                Session["caseId"] = -1;
                return;
            }
            PersonInfo.SelectedRecord = PersonInfo.AuthorizedRecords[new Guid(DropDownList1.SelectedValue)];

            patientName.Text = DropDownList1.SelectedItem.Text;
            AKAV1.Parameters p = new AKAV1.Parameters();
            lblTemperature.Text = p.getTemperature(PersonInfo.SelectedRecord);
            lblBloodPressure.Text = p.getBloodPressure_Systolic(PersonInfo.SelectedRecord) + "/" + p.getBloodPressure_Diastolic(PersonInfo.SelectedRecord);
            lblBloodSugar.Text = "Fasting Sugar:" + p.getBloodGlucose_BeforeEating(PersonInfo.SelectedRecord) + "  Post Prandial:" + p.getBloodGlucose_AfterEating(PersonInfo.SelectedRecord);
            lblHaemoglobin.Text = p.getHaemoglobin(PersonInfo.SelectedRecord);
            lblWeight.Text = p.getWeight(PersonInfo.SelectedRecord);
            lblCholesterol.Text = "HDL:" + p.getHighDensityLipid(PersonInfo.SelectedRecord) + "  LDL:" + p.getLowDensityLipid(PersonInfo.SelectedRecord) + "  Triglyceride:" + p.getTriglyceride(PersonInfo.SelectedRecord);
            Session["caseId"]= ""+AKAV1.Operations.getActiveCase(new Guid(DropDownList1.SelectedValue));
            decimal caseId=Decimal.Parse(Session["caseId"].ToString());
            if (caseId != -1)
            {
              medcase = AKAV1.Operations.getCaseDetails(caseId);
              populatePrescriptionGiven(medcase);
              populateDiseasesDiagonized(medcase);
              populateQuestions(caseId);
              btnCreateNewCase.Enabled = false;
              btnSendCaseToDoctor.Enabled = true;
            }
            else
            {
              btnCreateNewCase.Enabled = true;
              btnSendCaseToDoctor.Enabled = false;
            }
            
         }
        private void populateQuestions(decimal caseId)
        {
            Dictionary<decimal,string> questions=AKAV1.Operations.getQuestions(caseId);
            Table t = new Table();
            TableRow tr=null;
            TableCell tc=null;
            answerBoxes  = new TextBox[questions.Count];
            t.CssClass="width100";
            int i=0;
            foreach(KeyValuePair<decimal,string> kv in questions)
            {
                tr=new TableRow();
                tc = new TableCell();
                tc.Text = kv.Value.ToString();
                tr.Cells.Add(tc);
                t.Rows.Add(tr);

                tr = new TableRow();
                tc = new TableCell();
                answerBoxes[i] = new TextBox();
                answerBoxes [i].ID = ""+kv.Key.ToString();
                answerBoxes[i].Text = "";
                
                tc.Controls.Add(answerBoxes [i]);
                tr.Cells.Add(tc);
                t.Rows.Add(tr);
                i++;
            }
            QuestionsTab.Controls.Add(t);
            if (questions.Count > 0)
            {
               btnAnswer.Enabled=true;
            }
        }
        protected void btnAnswer_Click(Object sender, EventArgs e)
        {
            if (answerBoxes != null)
            {
                foreach (TextBox t in answerBoxes)
                {
                    AKAV1.Operations.answerQuestion(Decimal.Parse(t.ID), decimal.Parse(Session["caseId"].ToString()), Request[t.ID]);
                }
            }
            btnAnswer.Visible=false;
        }
        protected void btnAddParametersToCase_Click(object sender, EventArgs e)
        {
             AKAV1.Parameters p=new AKAV1.Parameters();
             p.getParambyfiltering(PersonInfo.SelectedRecord, Decimal.Parse(Session["caseId"].ToString()));
        }
        protected void btnCreateNewCase_Click(object sender, EventArgs e)
        {
            Session["caseId"]=AKAV1.Operations.createNewCase(PersonInfo.SelectedRecord.Id,(string)Session["KioskId"]);
            btnCreateNewCase.Enabled = false;
            btnSendCaseToDoctor.Enabled = true;
        }

        protected void btnSendCaseToDoctor_Click(object sender, EventArgs e)
        {
            AKAV1.Operations.allocateDoctorToCase(Decimal.Parse(Session["caseId"].ToString()));
            btnSendCaseToDoctor.Enabled = false;
        }

        


        
        }

      

    }
    

