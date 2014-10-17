using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Health;
using Microsoft.Health.Web;
using Microsoft.Health.ItemTypes;


namespace sap.Web
{   
    public partial class _Default : HealthServicePage
    {
        //double[] temps;
        static int i=0;
        string pid;
        int count;
        string answer;
        HealthRecordInfo hri;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //explicit redirection for now
                PersonInfo.SelectedRecord = PersonInfo.GetSelfRecord();
                Response.Redirect("Doctor.aspx?caseId=1");
               
                //Redirecting to Doctors Home Page When doc logs in for the first time
                //Also Storing the list of patients in Session object so that it is accessible to any page
                
                if (Session["firstTime"]==null)
                {
                    Dictionary<Guid, HealthRecordInfo> l = PersonInfo.AuthorizedRecords;
                    List<HealthRecordInfo> list = new List<HealthRecordInfo>(l.Values);
                    int k = 0;
                    foreach (HealthRecordInfo h in list)
                    {
                        Session["patient" + k] = h;
                        k++;
                    }
                    Session["patientCount"] = list.Count;
                  //  Response.Redirect("Home.aspx");
                    Response.Redirect("TestCentre.aspx");
                }
                else
                {
                    // PersonInfo.SelectedRecord = PersonInfo.GetSelfRecord();
                    ApplicationInfo info = ApplicationConnection.GetApplicationInfo();
                    TextBox1.Text = info.Name;

                    //Showing/storing the patient id when the page is not called for the first time
                    //in short,page has been called from hyperlink in home.aspx
                    if (Request["pid"] != null)
                    {
                       // this.PersonAge.Text = (string)Request["pid"];
                        pid = (string)Request.QueryString["pid"];
                    }

                    //get healthrecordinfo from patientid
                    Guid a = new Guid(pid);
                    HealthRecordInfo hri = PersonInfo.AuthorizedRecords[a];

                    // calls to methods for retriving personal info
                    PersonReligion.Text = getPersonReligion(hri);
                    PersonName.Text = getPersonName(hri);
                    PersonAge.Text = getPersonAge(hri);
                    PersonSex.Text = getPersonGender(hri);
                    PersonBloodGroup.Text = getPersonBloodGroup(hri);
                    PersonDiet.Text = getPersonDiet(hri);
                    PersonMarital.Text = getPersonMartitalStatus(hri);
                    PersonAlcohol.Text = getAlcoholStatus(hri);

                    //allergy
                    getPersonAllergy(hri);
                  //  PersonInfo.SelectedRecord = list[0];
              //getPersonImage(PersonInfo.SelectedRecord);
                Pdata();
                System.IO.File.Copy(@"c:\213.jpg", "../213.jpg", true);
                System.IO.File.Copy(@"c:\213.jpg", "../214.jpg", true);
                Image1.ImageUrl = "c:\\213.jpg";
               
              //  System.Web.UI.WebControls.Image i = k.;
               // Pdata();
               
              
               
                
             
               Image1.AlternateText = "prob";
            }
                    }   // StartupData.SetActiveView(StartupData.Views[0]);
                 
                
                
            catch (Exception ex)
            {
                Image1.AlternateText = ex.ToString();
            }
            
       }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string q = "appid=e0fddfa0-44ba-4f7e-a507-5e31276f461f&onopt2=kshitij&ismra=true";
            RedirectToShellUrl("APPAUTH", q);
        }
        
        public string getPersonName(HealthRecordInfo abc)
        {
          
            if (abc != null)
            {
                return abc.Name.ToString();
            }
            else

            {
                return "NA";
            }
        }

        public string getPersonAge(HealthRecordInfo abc)
        {
            try
            {
                Personal k;
                if (abc != null)
                {

                    PersonInfo.SelectedRecord = abc;
                    List<HealthRecordItem> abc1 = GetValues<HealthRecordItem>(Personal.TypeId);
                    k = (Personal)abc1[0];
                    DateTime a = (DateTime)k.BirthDate;
                    //HealthServiceDateTime b = new HealthServiceDateTime(DateTime.Now);
                    //int c = b.CompareTo(a);
                    if (k != null)
                         answer=CalculateAge(a).ToString();
                    else answer="NA";
                }
                else
                {
                    answer= "NA";
                }
                return answer;
            }
            catch (Exception e2)
            {
                answer = "NA";
                return answer;
            }
            }

        public string getPersonBloodGroup(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {

                    PersonInfo.SelectedRecord = abc;
                    List<HealthRecordItem> abc1 = GetValues<HealthRecordItem>(Personal.TypeId);
                    Personal k = (Personal)abc1[0];
                    if (k != null)
                        answer=k.BloodType.Text;
                    else answer= "NA";
                }
                else
                {
                    answer= "NA";
                }
                return answer;
            }
            catch (Exception e2)
            {
                answer = "NA";
                return answer;
            }
            }

        public string getPersonGender(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {

                    PersonInfo.SelectedRecord = abc;
                    List<HealthRecordItem> abc1 = GetValues<HealthRecordItem>(BasicV2.TypeId);
                    BasicV2 k = (BasicV2)abc1[0];
                    if (k != null)
                        answer= k.Gender.ToString();
                    else answer= "NA";
                }
                else
                {
                    answer= "NA";
                }
                return answer;
            }
            catch (Exception e2)
            {
                answer = "NA";
                return answer;
            }
        }
       
        public string getPersonMartitalStatus(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {

                    PersonInfo.SelectedRecord = abc;
                    List<HealthRecordItem> abc1 = GetValues<HealthRecordItem>(Personal.TypeId);
                    Personal k = (Personal)abc1[0];
                    if (k != null)
                        answer = k.MaritalStatus.Text;
                    else answer = "NA";
                }
                else
                {
                    answer = "NA";
                }
                return answer;
            }
            catch (Exception e2)
            {
                answer = "NA";
                return answer;
            }
        }
        
        public string getPersonReligion(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {

                    PersonInfo.SelectedRecord = abc;

                    List<HealthRecordItem> abc1 = GetValues<HealthRecordItem>(Personal.TypeId);
                    Personal k = (Personal)abc1[0];
                    if (k != null)
                        answer = k.Religion.Text;
                    else
                        answer = "NA";

                }
                else
                {
                    answer= "NA";
                }
                return answer;
            }
            catch (Exception e1)
            { answer = "NA";
              return answer;
            }
        }
        
        public string getAlcoholStatus(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {

                    PersonInfo.SelectedRecord = abc;
                    List<HealthRecordItem> abc1 = GetValues<HealthRecordItem>(SleepJournalPM.TypeId);
                    SleepJournalPM k = (SleepJournalPM)abc1[0];
                    if (k != null)
                    {
                        if (k.Alcohol.Count != 0)
                            answer = "Yes";
                        else
                            answer = "No";
                    }
                    else answer = "NA";
                }
                else
                {
                    answer = "NA";
                }
                return answer;
            }
            catch (Exception e2)
            {
                answer = "NA";
                return answer;
            }
        }
        
        public string getPersonDiet(HealthRecordInfo abc)
        {

            try
            {
                if (abc != null)
                {

                    List<HealthRecordItem> abc1 = GetValues<HealthRecordItem>(DietaryDailyIntake.TypeId);
                    DietaryDailyIntake k = (DietaryDailyIntake)abc1[0];
                    if (k != null)
                        answer= k.CommonData.Note;
                    else answer= "NA";
                }
                else
                {
                    answer= "NA";
                }
                return answer;
            }
            catch (Exception e2)
            {
                answer = "NA";
                return answer;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
           // i++;
            //Page.
            if (i < (count - 1))
                i++;
            else
                i = 0;


            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //MultiView1.SetActiveView(MultiView1.Views[DropDownList1.SelectedIndex]);
            MultiView1.ActiveViewIndex = DropDownList1.SelectedIndex;
            MultiView1.EnableViewState = true;
            MultiView1.Visible = true;
        }

        
   
        List<T> GetValues<T>(Guid typeID) where T : HealthRecordItem
        {
            HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();
            HealthRecordFilter filter = new HealthRecordFilter(typeID);
            searcher.Filters.Add(filter);
            HealthRecordItemCollection items = searcher.GetMatchingItems()[0];
            List<T> typedList = new List<T>();
            foreach (HealthRecordItem item in items)
            { typedList.Add((T)item); }
            return typedList;
        }

        //public string getSmokingStatus()
        //{
        //    QuestionAnswer k = (QuestionAnswer)PersonInfo.SelectedRecord.GetItem(QuestionAnswer.TypeId);
        //    if (k != null)
        //        k.
        //    else
        //        return "NA";

        //}
        //public string getPersonOccupation()
        //{
        //    BasicV2 k=(BasicV2)PersonInfo.SelectedRecord.GetItem(BasicV2.TypeId);
            
        //}
        
        public  int CalculateAge(DateTime birthDate)
        {
            // cache the current time
            DateTime now = DateTime.Today; // today is fine, don't need the timestamp from now
            // get the difference in years
            int years = now.Year - birthDate.Year;
            // subtract another year if we're before the
            // birth day in the current year
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;

            return years;
        }

        public Allergy[] getAllergy(HealthRecordInfo abc)
        {
            
            HealthRecordSearcher searcher = abc.CreateSearcher();

            HealthRecordFilter filter = new HealthRecordFilter(Allergy.TypeId);

            searcher.Filters.Add(filter);

            HealthRecordItemCollection collection = searcher.GetMatchingItems()[0];

            Allergy[] allergy = new Allergy[collection.Count];
            for (int i = 0; i < collection.Count; i++)
            {

                allergy[i] = collection[i] as Allergy;
            }
            if (allergy.Length != 0)
                return allergy;
            else return null;

        }
       
        void AddRC(Table t1, int rowsCount, int colsCount, Control tb)
        {
            for (int i = 0; i < rowsCount; i++)
            {
                TableRow row = new TableRow();
                row.BorderStyle = BorderStyle.Solid;
                row.BorderWidth = 1;
                for (int j = 0; j < colsCount; j++)
                {
                    TableCell cell1 = new TableCell();
                    
                    cell1.BorderStyle = BorderStyle.Solid;
                    
                    cell1.BorderWidth = 1;
                   
                    // Set a unique ID for each TextBox added


                    // Add the control to the TableCell
                    cell1.Controls.Add(tb);
                    
                    // Add the TableCell to the TableRow
                    row.Cells.Add(cell1);
                  
                }
                // Add the TableRow to the Table
                t1.Rows.Add(row);
            }
        }

        void getPersonAllergy(HealthRecordInfo abc)
        {
            Allergy[] allergy;
            allergy = getAllergy(abc);
            if (allergy != null)
            {
                int i = 0;
                Label[] l = new Label[allergy.Length];
                Label[] lb = new Label[allergy.Length];
                Label AType = new Label();
                AType.Text = "Allergen Type";
                Label ADesc = new Label();
                ADesc.Text = "Description";
                AddRC(Table1, 1, 1, AType);
                AddRC(Table1, 1, 1, ADesc);
                foreach (Allergy a in allergy)
                {
                    l[i] = new Label();
                    l[i].Text = allergy[i].ToString();
                    lb[i] = new Label();
                    lb[i].Text = allergy[i].AllergenType.ToString();

                    AddRC(Table1, 1, allergy.Length, lb[i]);
                    AddRC(Table1, 1, allergy.Length,l[i]);
                    i++;

                }
            }
            else NoAllergy.Visible = true;
            
        }

        void getPersonImage(HealthRecordInfo abc)
        {
            using (Stream imageStream = System.IO.File.OpenRead(@"C:\Ksh\213.jpg"))
            {
                HealthRecordItemCollection collection = PersonInfo.SelectedRecord.GetItemsByType(PersonalImage.TypeId, HealthRecordItemSections.All);

                PersonalImage image = null;

                if (collection.Count != 0)
                {
                    image = collection[0] as PersonalImage;

                    using (Stream currentImageStream = image.ReadImage())
                    {
                        byte[] imageBytes = new byte[currentImageStream.Length];
                        currentImageStream.Read(imageBytes, 0, (int)currentImageStream.Length);
                        System.IO.File.Delete(@"c:\213.jpg");

                        using (FileStream outputImage = System.IO.File.OpenWrite(@"c:\213.jpg"))
                        {
                            outputImage.Write(imageBytes, 0, imageBytes.Length);
                        }
                    }
                }


                if (image == null)
                {
                    image = new PersonalImage();
                    image.WriteImage(imageStream, "image/jpg");
                    PersonInfo.SelectedRecord.NewItem(image);
                }
                else
                {
                    image.WriteImage(imageStream, "image/jpg");
                    PersonInfo.SelectedRecord.UpdateItem(image);
                }
            }
        }
        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {

        }
        void Pdata()
        {
            PersonalImage p_image;
             HealthRecordItemCollection hc1=PersonInfo.SelectedRecord.GetItemsByType(PersonalImage.TypeId,HealthRecordItemSections.All);
             p_image =hc1[0] as PersonalImage;
             if (p_image != null)
             {
                 using (System.IO.Stream currentImageStream = p_image.ReadImage())
                 {
                     if (currentImageStream != null)
                     {
                         byte[] imageBytes = new byte[currentImageStream.Length];
                         currentImageStream.Read(imageBytes, 0, (int)currentImageStream.Length);

                         using (System.IO.FileStream outputImage = System.IO.File.OpenWrite(@"c:\213.jpg"))
                         {
                             outputImage.Write(imageBytes, 0, imageBytes.Length);
                            

                         }
                     }
                     
                 }
             }
             

        }

     }
 

        

       

    }

