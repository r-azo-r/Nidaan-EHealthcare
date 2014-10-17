using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


namespace AKAV1
{
    public class PersonHealthData:HealthServicePage
    {
        
        static int i = 0;
        public string pid;
        public int count;
        public string answer;
        public HealthRecordInfo abc;

        public PersonHealthData()
        {
           
        }

        public string getPersonName()
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

        public string getPersonAge()
        {
            try
            {
                Personal k;
                if (abc != null)
                {

                   
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

       public string getPersonBloodGroup()
        {
            try
            {
                if (abc != null)
                {
                    
                    Personal k ;
                    k=GetValues<HealthRecordItem>(Personal.TypeId)[0] as Personal;
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

        public string getPersonGender()
        {
            try
            {
                if (abc != null)
                {

                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(BasicV2.TypeId);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection collection = searcher.GetMatchingItems()[0];

                 
                    
                    BasicV2 k = (BasicV2)collection[0];
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

        public string getPersonMartitalStatus()
        {
            try
            {
                if (abc != null)
                {

                  
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

        public string getPersonReligion()
        {
            try
            {
                if (abc != null)
                {

                   

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

        public string getAlcoholStatus()
        {
            try
            {
                if (abc != null)
                {

                    
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

        public string getPersonSmoking()
        {
            try
            {
                if (abc != null)
                {


                    List<HealthRecordItem> abc1 = GetValues<HealthRecordItem>(SleepJournalPM.TypeId);
                    SleepJournalPM k = (SleepJournalPM)abc1[0];
                    if (k != null)
                    {
                        if (k.CommonData.Note!=null && k.CommonData.Note.Equals("Yes"))
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
        public string getPersonDiet()
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
  
        List<T> GetValues<T>(Guid typeID) where T : HealthRecordItem
        {
            HealthRecordSearcher searcher = abc.CreateSearcher();
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

        public Allergy[] getAllergy()
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
       
    }
}