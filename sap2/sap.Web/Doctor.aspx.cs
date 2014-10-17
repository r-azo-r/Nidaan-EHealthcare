using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    public partial class Doctor : HealthServicePage
    {
         AKAV1.PersonHealthData phd;
         public Doctor()
         {
             phd = new AKAV1.PersonHealthData();
         }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            smsnote.Visible = false;
            phd.abc = PersonInfo.GetSelfRecord();
            Session["DoctorLoginId"] = "doc1";
            populatePendingCaseTable();
            if (Request["caseId"] != null)
            {
                if (!IsPostBack)
                {
                    phd.abc = PersonInfo.AuthorizedRecords[AKAV1.Operations.getCaseDetails(Decimal.Parse(Request["caseId"])).patientId];
                    populateDiseaseListBox();
                    populateBasicInformation();

                    populateDiseasesAndPrescription();
                    populateQuestionsList();
                    populatePastCases();
                    
                }
                addingMedicalDetails();
        
                //phd.abc = PersonInfo.GetSelfRecord();
                //   phd.abc  = PersonInfo.AuthorizedRecords[medcase.patientId];
            }
        }
        private void populateQuestionsList()
        {
            string[] Questions ={
                                    "Question1",
                                    "Question2",
                                    "Question3",
                                    "Question4",
                                    "Question5",
                                    "Question6",
                                    "Question7",
                                    "Question8",
                                    "Question9",
                                    "Question10",
                               };
            for (int i = 0; i < Questions.Length; i++)
            {
                list1.Items.Add(Questions[i]);
            }
            decimal caseId=Decimal.Parse(Request["caseId"]);
            if(caseId!=-1)
            {
                AKAV1.QuestionAnswer[] qas = AKAV1.Operations.getQuestionAnswer(caseId);
                
                HtmlTableRow htr;
                HtmlTableCell htc;
                if (qas != null)
                {
                    foreach (AKAV1.QuestionAnswer qa in qas)
                    {
                        htr = new HtmlTableRow();
                        htc = new HtmlTableCell();
                        htc.InnerHtml = qa.question;
                        htr.Cells.Add(htc);

                        htc = new HtmlTableCell();
                        htc.InnerHtml = qa.answer;
                        htr.Cells.Add(htc);

                        htc = new HtmlTableCell();
                        htc.InnerHtml = qa.qAsked.ToString();
                        htr.Cells.Add(htc);

                        tblQuestions.Rows.Add(htr);

                    }
                }
                
            }
        }
        protected void btnAddQuestion_Click(Object sender, EventArgs e)
        {
            list2.Items.Add(list1.SelectedItem.Text);
            list1.Items.Remove(list1.SelectedItem.Text);
            
        }
        protected void btnAddOwnQuestion_Click(Object sender, EventArgs e)
        {
            list2.Items.Add(txtQuestion.Text);
            txtQuestion.Text = "";
        }
        protected void btnSendQuestionToPatient_Click(Object sender, EventArgs e)
        {
            decimal caseId=Decimal.Parse(Request["caseId"]);
            foreach(ListItem li in list2.Items)
            {
             AKAV1.Operations.askQuestion(li.Text,caseId);
            }
        }
        public string getPhone(HealthRecordInfo hrf)
        {

            HealthRecordSearcher searcher = hrf.CreateSearcher();
            HealthRecordFilter filter = new HealthRecordFilter(Contact.TypeId);
            searcher.Filters.Add(filter);
            HealthRecordItemCollection collection = searcher.GetMatchingItems()[0];
            Contact p = new Contact();
            if (collection.Count != 0)
                p = collection[0] as Contact;

            if (p != null)
            {
                String phone = p.ToString().Remove(9);
                return phone;
            }
            else
                return "NA";
        }  
        private void populatePendingCaseTable()
        {
            TableCell tc;
            TableRow tr;
            HyperLink hl;
   
            AKAV1.PendingCases[] pc = AKAV1.Operations.getPendingCases((string)Session["DoctorLoginId"]);
            if (pc != null)
            {
                for (int i = 0; i < pc.Length; i++)
                {

                    hl = new HyperLink();
                    hl.Text = "" + pc[i].caseId;
                    hl.NavigateUrl = "Doctor.aspx?caseId=" + pc[i].caseId+"&dtAllotted="+pc[i].dateTimeAllotted;
                    tc = new TableCell();
                    tc.Controls.Add(hl);
                    tr = new TableRow();
                    tr.CssClass = "tablerow1";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = "" + pc[i].dateTimeAllotted;
                    tr.Cells.Add(tc);
                    tblPendingCases.Rows.Add(tr);
                    
                }
            }
        }
            private void populateDiseaseListBox()
            {
                string[] diseases=AKAV1.Operations.getDiseaseList();
                ListItem li;
                if (diseases != null)
                {
                    for (int i = 0; i < diseases.Length; i++)
                    {
                        li = new ListItem(diseases[i]);
                        DiseaseListBox.Items.Add(li);
                    }
                }
                //AKAV1.DiseasePredicted[] dp = AKAV1.Operations.predictDiseases(Decimal.Parse(Request["caseId"]), Session["DoctorLoginId"].ToString(), DateTime.Parse(Request["dtAllotted"]), PersonInfo.SelectedRecord);
                //lblPredictedDisease.Text += "\n ";
                //for (int i = 0; dp!=null&&i < dp.Length; i++)
                //    lblPredictedDisease.Text += "Disease:" + dp[i].disease + "     Probability:" + dp[i].probability + "\n";
            }
            private void populateBasicInformation()
            {
                 if (Request["caseId"] != null)
                {
                    
                    Table t=new Table();
                    TableRow tr;
                    TableCell tc;

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Case ID:";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = Request["caseId"];
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Name:";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = phd.getPersonName();
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Gender:";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = phd.getPersonGender();
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Age:";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = phd.getPersonAge();
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Blood Group:";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = phd.getPersonBloodGroup();
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Diet:";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = phd.getPersonDiet();
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Martial Status:";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = phd.getPersonMartitalStatus();
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Alcohol Consumption";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = phd.getAlcoholStatus();
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Smoking:";
                    tr.Cells.Add(tc);
                    tc = new TableCell();
                    tc.Text = phd.getPersonSmoking();
                    tr.Cells.Add(tc);
                    t.Rows.Add(tr);

                    basicInformationTab.Controls.Add(t);
                 }
            }
            private void populateDiseasesAndPrescription()
            {

                if (Request["caseId"] != null)
                {
                    decimal caseId = Decimal.Parse(Request["caseId"]);
                    AKAV1.MedicalCase medcase = AKAV1.Operations.getCaseDetails(caseId);
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
                        thc.Text = "Diagonsis Date Time";
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
                        diseasesDiagonized.Controls.Add(t);
                    }



                    if (medcase.prescriptions != null)
                    {
                        t = new Table();
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
                        thc.Text = "Prescription Date Time";
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
                        prescriptionGiven.Controls.Add(t);

                    }



                }
            }
            private void addingTemperatureDetails()
            {
                Table t = new Table();
                ParameterDisplay pd = new ParameterDisplay();
                VitalSigns[] k = pd.getTemperature(phd.abc);

                TableHeaderRow trh = new TableHeaderRow();
                TableHeaderCell tch1 = new TableHeaderCell();
                TableHeaderCell tchi = new TableHeaderCell();
                TableHeaderCell tch2 = new TableHeaderCell();
                tch1.Text = "Value(°C)";
                tchi.Text = "Date";
                tch2.Text = "Time";
                
                trh.Cells.Add(tch1);
                trh.Cells.Add(tchi);
                trh.Cells.Add(tch2);
                //trh.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //trh.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                //trh.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                t.Rows.Add(trh);
                for (int i = 0; i < k.Length; i++)
                {
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    TableCell tci = new TableCell();
                    TableCell tc2 = new TableCell();
                    //tc1.Text=k[i].

                    tc1.Text = k[i].VitalSignsResults[0].ToString();
                    tci.Text = k[i].When.Date.ToString() ;
                    tc2.Text = "   "+k[i].When.Time.ToString();
                   
                    tr.Cells.Add(tc1);
                    tr.Cells.Add(tci);
                    tr.Cells.Add(tc2);
                    //tr.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    t.Rows.Add(tr);
                    
                }
                temperatureData.Controls.Add(t);
            }
            private void addingWeightDetails()
            {
                Table t = new Table();
                ParameterDisplay pd = new ParameterDisplay();


                Weight[] k = pd.getWeight(phd.abc);

                TableRow trh = new TableRow();
                TableHeaderCell tch1 = new TableHeaderCell();
                TableHeaderCell tchi = new TableHeaderCell();
                TableCell tch2 = new TableCell();
                tch1.Text = "Value(kg)";
                tchi.Text = "Date";
                //tch2.Text = "Time";
                trh.Cells.Add(tch1);
                trh.Cells.Add(tchi);
               // trh.Cells.Add(tch2);
                t.Rows.Add(trh);
                //trh.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //trh.Cells[1].HorizontalAlign = HorizontalAlign.Center;
               // trh.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                //trh.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                for (int i = 0; i < k.Length; i++)
                {
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    TableCell tci = new TableCell();
                   // TableCell tc2 = new TableCell();
                    //tc1.Text=k[i].
                    int ki = (Int32)(k[i].Value.Kilograms);
                    tc1.Text = ki.ToString();
                    tci.Text = k[i].When.Date.ToString();
                 //   tc2.Text = "   "+k[i].When.Time.ToString();
                    tr.Cells.Add(tc1);
                    tr.Cells.Add(tci);
                  //  tr.Cells.Add(tc2);
                    t.Rows.Add(tr);
                    //tr.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                 //   tr.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                   // tr.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                }
                weightData.Controls.Add(t);

            }
            private void addingBloodPressureDetails()
            {
                Table t = new Table();
                ParameterDisplay pd = new ParameterDisplay();

                //temperature
                BloodPressure[] k = pd.getBloodPressure(phd.abc);

                TableHeaderRow trh = new TableHeaderRow();
                TableHeaderCell tch1 = new TableHeaderCell();
                TableHeaderCell tchi = new TableHeaderCell();
                TableHeaderCell tch2 = new TableHeaderCell();
                tch1.Text = "Systolic";
                tchi.Text = "Diastolic";
                tch2.Text = "Date";

                trh.Cells.Add(tch1);
                trh.Cells.Add(tchi);
                trh.Cells.Add(tch2);
                //trh.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //trh.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                //trh.Cells[2].HorizontalAlign = HorizontalAlign.Center;
               // trh.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                t.Rows.Add(trh);
                for (int i = 0; i < k.Length; i++)
                {
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    TableCell tci1 = new TableCell();
                    TableCell tc2 = new TableCell();
                    //tc1.Text=k[i].

                    tc1.Text = k[i].Systolic.ToString();
                    tci1.Text = k[i].Diastolic.ToString();
                    tc2.Text = k[i].When.Date.ToString() ;
                    tr.Cells.Add(tc1);
                    tr.Cells.Add(tci1);
                    tr.Cells.Add(tc2);
                    //tr.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                   // tr.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    t.Rows.Add(tr);

                }
                bloodPressureData.Controls.Add(t);
            }
            private void addingBloodGlucoseDetails()
            {
                Table t = new Table();
                ParameterDisplay pd = new ParameterDisplay();
                BloodGlucose[] k = pd.getBloodGlucose(phd.abc);

                TableHeaderRow trh = new TableHeaderRow();
                TableHeaderCell tch1 = new TableHeaderCell();
                TableHeaderCell tci = new TableHeaderCell();
                TableHeaderCell tch2 = new TableHeaderCell();
                tch1.Text = "Value()";
                tci.Text = "Date";
              //  tch2.Text = "Date     Time";
                trh.Cells.Add(tch1);
                trh.Cells.Add(tci);
             //   trh.Cells.Add(tch2);
                t.Rows.Add(trh);
                //trh.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //trh.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                for (int i = 0; i < k.Length; i++)
                {
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    TableCell tci1 = new TableCell();
                  //  TableCell tc2 = new TableCell();
                    //tc1.Text=k[i].

                    tc1.Text = k[i].Value.DisplayValue.ToString();
                    tci1.Text = k[i].When.Date.ToString();
                 //   tc2.Text = k[i].When.ToDateTime().ToString() + " IST";
                    tr.Cells.Add(tc1);
                    tr.Cells.Add(tci1);
                  //  tr.Cells.Add(tc2);

                    t.Rows.Add(tr);
                    //tr.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                }
                bloodGlucoseData.Controls.Add(t);
            }
            private void addingHaemoglobinDetails()
            {
                Table t = new Table();
                ParameterDisplay pd = new ParameterDisplay();

                HbA1C[] k = pd.getHaemoglobin(phd.abc);

                TableHeaderRow trh = new TableHeaderRow();
                TableHeaderCell tch1 = new TableHeaderCell();

                TableHeaderCell tci = new TableHeaderCell();
                TableHeaderCell tch2 = new TableHeaderCell();
                tch1.Text = "Value(gm/L)";
                tci.Text = "Date";
                //tch2.Text = "Date     Time";
                trh.Cells.Add(tch1);
                trh.Cells.Add(tci);
                trh.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                trh.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                //trh.Cells.Add(tch2);
                t.Rows.Add(trh);
                for (int i = 0; i < k.Length; i++)
                {
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                  //  TableCell tci1 = new TableCell();
                    TableCell tc2 = new TableCell();
                    //tc1.Text=k[i].

                    tc1.Text = k[i].Value.ToString();
                    //tci1.Text = "     ";
                    tc2.Text = k[i].When.Date.ToString();
                    tr.Cells.Add(tc1);
                    //tr.Cells.Add(tci1);
                    tr.Cells.Add(tc2);
                    tr.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    t.Rows.Add(tr);

                }
                haemoglobinData.Controls.Add(t);


            }
            private void addingCholesterolDetails()
            {
                Table t = new Table();
                ParameterDisplay pd = new ParameterDisplay();

                //temperature
                CholesterolProfile[] k = pd.getCholestrolProfile(phd.abc);

                TableHeaderRow trh = new TableHeaderRow();
                TableHeaderCell tch1 = new TableHeaderCell();

                TableHeaderCell tci = new TableHeaderCell();
                TableHeaderCell tch2 = new TableHeaderCell();
                TableHeaderCell tch3 = new TableHeaderCell();
                tch1.Text = "HDL(mg/dl)";
                tci.Text = "LDL(mg/dl)";
                tch2.Text = "Triglyceride(mg/dl)";
                tch3.Text = "Date";
                trh.Cells.Add(tch1);
                trh.Cells.Add(tci);
                trh.Cells.Add(tch2);
                trh.Cells.Add(tch3);
                t.Rows.Add(trh);
                trh.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                trh.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                trh.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                trh.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                for (int i = 0; i < k.Length; i++)
                {
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    TableCell tci1 = new TableCell();
                    TableCell tc2 = new TableCell();
                    TableCell tc3 = new TableCell();
                    //tc1.Text=k[i].

                    tc1.Text = k[i].HDL.ToString();
                    tci1.Text = k[i].LDL.ToString();
                    tc2.Text = k[i].Triglyceride.ToString();


                    tc3.Text = k[i].When.Day + ":" + k[i].When.Month + ":" + k[i].When.Year;
                    tr.Cells.Add(tc1);
                    tr.Cells.Add(tci1);
                    tr.Cells.Add(tc2);
                    tr.Cells.Add(tc3);
                    t.Rows.Add(tr);
                    //tr.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                }
                cholesterolData.Controls.Add(t);


            }

            private void addingAllergyDetails()
            {
                Table t = new Table();
                ParameterDisplay pd = new ParameterDisplay();


                Allergy[] k = pd.getAllergy(phd.abc);

                TableHeaderRow trh = new TableHeaderRow();
                TableHeaderCell tch1 = new TableHeaderCell();

                TableHeaderCell tci = new TableHeaderCell();
                TableHeaderCell tch2 = new TableHeaderCell();
                tch1.Text = "Type";

                tci.Text = "Description";
             //   tch2.Text = "Date     Time";
                trh.Cells.Add(tch1);
                trh.Cells.Add(tci);

              //  trh.Cells.Add(tch2);
                t.Rows.Add(trh);
                //trh.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //trh.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                for (int i = 0; i < k.Length; i++)
                {
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    TableCell tci1 = new TableCell();
                  //  TableCell tc2 = new TableCell();
                    //tc1.Text=k[i].

                    tc1.Text = k[i].AllergenType.ToString();
              //      tc1.ToolTip = k[i].CommonData.Note;
                    tci1.Text = k[i].CommonData.Note;
                  //  tc2.Text = k[i].EffectiveDate.ToString() + " IST";
                    tr.Cells.Add(tc1);
                    tr.Cells.Add(tci1);
                 //   tr.Cells.Add(tc2);
                    t.Rows.Add(tr);
                    //tr.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    //tr.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                }
                allergyData.Controls.Add(t);
            }

            private void addingMedicalDetails()
            {
                addingTemperatureDetails();
                addingWeightDetails();
                addingBloodPressureDetails();
                addingBloodGlucoseDetails();
                addingHaemoglobinDetails();
                addingCholesterolDetails();
                addingAllergyDetails();
            }
            private void populatePastCases()
            {
                if (Request["caseId"] != null)
                {
                    AKAV1.MedicalCase medcase = AKAV1.Operations.getCaseDetails(Decimal.Parse(Request["caseId"]));
                    List<decimal> caseIds = AKAV1.Operations.getPastCases(medcase.patientId);

                    foreach (decimal cid in caseIds)
                    {
                        medcase = AKAV1.Operations.getCaseDetails(cid);
                        string html = "";
                        html += "<div><h4><a href='#'>Case " + cid + "</a></h4><div>";
                        html += "<table cellspacing='10px' cellpadding='5px'><trborder='2px'><th>Disease</th><th>Doctor ID</th><th>Diagonsis Date Time</th></tr>";
                        if (medcase.diseases != null)
                        {
                            foreach (AKAV1.DiseaseDiagonized d in medcase.diseases)
                            {
                                html += "<tr><td>"+d.disease+"</td><td>"+d.doctorId+"</td><td>"+d.diagonsisDateTime.ToString()+"</td></tr>";
                            }
                        }
                        html += "</table>";

                        html += "<table cellspacing='10px' cellpadding='5px'><tr><th>Medicine</th><th>Quantity</th><th>Doctor ID</th><th>Prescription Date Time</th></tr>";
                        if (medcase.prescriptions != null)
                        {
                            foreach (AKAV1.Prescription p in medcase.prescriptions)
                            {
                                html += "<tr><td>" + p.medicine + "</td><td>" + p.quantity+" "+p.unit+ "</td><td>"+p.doctorId+"</td><td>" + p.prescriptionDateTime.ToString() + "</td></tr>";
                            }
                        }
                        html += "</table>";

                        html += "</div></div>";
                        accordion1.InnerHtml+= html;
                    }
                }
            }

            protected void btnCloseCase_Click(object sender, EventArgs e)
            {
                decimal caseId = Decimal.Parse(Request["caseId"]);
                AKAV1.Operations.closeCase(caseId,(string)Session["DoctorLoginId"],DateTime.Parse(Request["dtAllotted"]));
                populatePastCases();
            }
            protected void btnDiagonizeDisease_Click(object sender, EventArgs e)
            {
               decimal caseId = Decimal.Parse(Request["caseId"]);
               AKAV1.Operations.diagonizeDisease(caseId,(string)Session["DoctorLoginId"],DiseaseListBox.Text,DateTime.Parse(Request["dtAllotted"]),PersonInfo.SelectedRecord);
               populateDiseasesAndPrescription();
            }

            protected void btnPrescribeMedicine_Click(object sender, EventArgs e)
            {
                decimal caseId = Decimal.Parse(Request["caseId"]);
                AKAV1.Operations.givePrescription(new AKAV1.Prescription(caseId,Request["txtMedicineName"],Int32.Parse( Request["txtQuantity"]),Request["txtUnit"],Request["txtInstructions"],(string)Session["DoctorLoginID"],DateTime.Now), DateTime.Parse(Request["dtAllotted"]));
                populateDiseasesAndPrescription();
            }
            protected void btnRequestParameter_Click(object sender, EventArgs e)
            {
                AKAV1.Operations.requestParameter(parameterList.Text,Decimal.Parse(Request["caseId"]));
            }
            protected void btnSMS_Click(object sender, EventArgs e)
            {
                //Send SMS
                MailSender.SendEmail("icmailid@gmail.com","akav5669",getPhone(phd.abc),"Prescription","Your case has been attended and prescptn given is:",System.Web.Mail.MailFormat.Html,"");
                smsnote.Visible=true;
            }
         public void populatebp()
            {
                ParameterDisplay pd=new ParameterDisplay();
                BloodPressure[] bp=pd.getBloodPressure(phd.abc);
                CholesterolProfile[] cp = pd.getCholestrolProfile(phd.abc);
                VitalSigns[] vs = pd.getTemperature(phd.abc);
                
                SqlConnection con = new SqlConnection(AKAV1.Operations.connectionString);
                
                con.Open();
                for(int i=0;i<bp.Length;i++)
                {
                    SqlCommand comm = new SqlCommand("", con);
                    comm.CommandText="insert into BloodPressure values("+bp[i].Systolic+","+bp[i].Diastolic+",'"+bp[i].When.Date.ToString()+"')";
                    comm.ExecuteNonQuery();
                }
                con.Close();
                con.Open();
                for (int i = 0; i < cp.Length; i++)
                {
                    SqlCommand comm = new SqlCommand("", con);
                    comm.CommandText = "insert into Cholesterol values(" + cp[i].HDL + "," + cp[i].LDL + "," + cp[i].Triglyceride + "," + cp[i].TotalCholesterol + ",'" + new DateTime(cp[i].When.Year, cp[i].When.Month, cp[i].When.Day).ToString() + "')";
                    comm.ExecuteNonQuery();
                }
                con.Close();
                con.Open();
                for (int i = 0; i < vs.Length; i++)
                {
                    SqlCommand comm = new SqlCommand("", con);
                    comm.CommandText = "insert into Temperature values(" + vs

[i].VitalSignsResults[0].ToString() + ",'"+ vs[i].When.Date.ToString() + "')";
                    comm.ExecuteNonQuery();
                }
                con.Close();
            }
            public void flushbp()
            {
                //string connectionString;
                SqlConnection con = new SqlConnection(AKAV1.Operations.connectionString);
                SqlCommand comm = new SqlCommand("", con);
                con.Open();
                try
                {
                    comm.CommandText = "delete from BloodPressure";
                    comm.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    comm.CommandText = "delete from Cholesterol";
                    comm.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                        comm.CommandText = "delete from Temperature";
                    comm.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception e)
                {
                }
                            }
    }
}
