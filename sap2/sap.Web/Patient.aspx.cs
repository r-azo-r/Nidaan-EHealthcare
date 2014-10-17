using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.IO;

using Microsoft.Health;
using Microsoft.Health.Web;
using Microsoft.Health.ItemTypes;

namespace sap.Web
{
    public partial class Patient :HealthServicePage
    {
        string answer;
        HealthRecordInfo hri;
        protected void Page_Load(object sender, EventArgs e)
        {
            //setting the hri to a record
            if (Session["PatientHVId"]!=null)
            hri = PersonInfo.AuthorizedRecords[(Guid)Session["PatientHVId"]];
            else
            hri = PersonInfo.GetSelfRecord();
           AddMedicalDetails(hri);

            answer = "";
        }

        public void AddMedicalDetails(HealthRecordInfo abc)
        {
            PersonName.Text = getPersonName(hri);
            PersonAge.Text = getPersonAge(hri);
            PersonGender.Text = getPersonGender(hri);
            PersonReligion.Text = getPersonReligion(hri);
            PersonMarital.Text = getPersonMartitalStatus(hri);
            
            PersonDiet.Text = getPersonDiet(hri);
            PersonBloodGroup.Text = getPersonBloodGroup(hri);

            showMedicalDetails();
        }

        public void AddTemperature(HealthRecordInfo abc)
        {
            VitalSigns vs = new VitalSigns();
            vs.VitalSignsResults[0].Value = double.Parse(Temperature.Text);
            abc.NewItem(vs);
        }
        public void AddHeight(HealthRecordInfo abc)
        {
            Height vs = new Height();
            vs.Value.Meters = double.Parse(Height.Text);
            abc.NewItem(vs);
        }
        public void AddWeight(HealthRecordInfo abc)
        {
            Weight vs = new Weight();
            vs.Value.Kilograms = double.Parse(Weight.Text);
            abc.NewItem(vs);
        }
        public void AddHaemoglobin(HealthRecordInfo abc)
        {
            HbA1C vs = new HbA1C();
            vs.Value = double.Parse(Haemoglobin.Text);
            abc.NewItem(vs);
        }
        public void AddBloodGlucose(HealthRecordInfo abc)
        {

            BloodGlucoseMeasurement bgm = new BloodGlucoseMeasurement();
            bgm.Value = double.Parse(BloodGlucose1.Text);
          
        }
        public void AddBloodPressure(HealthRecordInfo abc)
        {
            BloodPressure vs = new BloodPressure();
            vs.Diastolic = Int32.Parse(BloodPressure1.Text);
            vs.Systolic = Int32.Parse(BloodPressure2.Text);
            //can insert pulse too
            abc.NewItem(vs);
        }
        public void AddCholesterol(HealthRecordInfo abc)
        {
            CholesterolProfile vs = new CholesterolProfile();
            vs.HDL = Int32.Parse(Cholesterol1.Text);
            vs.LDL = Int32.Parse(Cholesterol2.Text);
            vs.Triglyceride = Int32.Parse(Cholesterol3.Text);
            vs.TotalCholesterol = Int32.Parse(Cholesterol1.Text) + Int32.Parse(Cholesterol2.Text) + Int32.Parse(Cholesterol3.Text);
            abc.NewItem(vs);
        }

        public void showMedicalDetails()
        {
            AKAV1.ParameterValues pv = new AKAV1.ParameterValues();
            Weightlbl.Text = pv.getWeight(hri);
            Templbl.Text = pv.getTemperature(hri);
            Bglbl.Text = pv.getBloodGlucose_AfterEating(hri);
            Bplbl.Text = "Systolic:" + pv.getBloodPressure_Systolic(hri) + " Disastolic:" + pv.getBloodPressure_Diastolic(hri); ;
            Haemoglobinlbl.Text = pv.getHaemoglobin(hri);
            Cholesterollbl.Text = "HDL:" + pv.getHighDensityLipid(hri) +" LDL:" + pv.getLowDensityLipid(hri) + " Triglyceride:" + pv.getTriglyceride(hri);

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
                        answer = CalculateAge(a).ToString();
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
                        answer = k.BloodType.Text;
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
                        answer = k.Gender.ToString();
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

        public int CalculateAge(DateTime birthDate)
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
                    answer = "NA";
                }
                return answer;
            }
            catch (Exception e1)
            {
                answer = "NA";
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
                        answer = k.CommonData.Note;
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



        protected void wtbtn_Click(object sender, EventArgs e)
        {
            AddWeight(hri);
        }

        protected void htbtn_Click(object sender, EventArgs e)
        {
            AddHeight(hri);
        }

         protected void tempbtn_Click(object sender, EventArgs e)
        {
            AddTemperature(hri);
        }

        protected void bpbtn_Click(object sender, EventArgs e)
        {
            AddBloodPressure(hri);
        }

        protected void haembtn_Click(object sender, EventArgs e)
        {
            AddHaemoglobin(hri);
        }

        protected void bgbtn_Click(object sender, EventArgs e)
        {
            AddBloodGlucose(hri);
        }

        protected void cholesbtn_Click(object sender, EventArgs e)
        {
            AddCholesterol(hri);
        }
        #region code
        /*
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
                        answer = CalculateAge(a).ToString();
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
                        answer = k.BloodType.Text;
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
                        answer = k.Gender.ToString();
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
                    answer = "NA";
                }
                return answer;
            }
            catch (Exception e1)
            {
                answer = "NA";
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
                        answer = k.CommonData.Note;
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

        public int CalculateAge(DateTime birthDate)
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
       */
        #endregion

        

        
    }
}