using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Health;
using Microsoft.Health.Web;
using Microsoft.Health.ItemTypes;
namespace AKAV1
{

    public class Parameters : HealthServicePage
    {
        public static string answer;
        public enum parameterMapping
        {
            Temperature = 1,
            Weight = 2,
            BloodPressureSystolic = 3,
            BloodPressureDiastolic = 4,
            BloodSugarLevelBeforeEating = 5,
            BloodSugarLevelAfterEating = 6,
            Haemoglobin = 7,
            HighDensityLipid = 8,
            LowDensityLipid = 9,
            triglyceride = 10,
            allergy = 11,
            Age = 12
        };
        public string getPersonAge(HealthRecordInfo abc)
        {

            try
            {
                Personal k;

                if (abc != null)
                {

                    PersonInfo.SelectedRecord = abc;

                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(Personal.TypeId);
                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);
                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection age = searcher.GetMatchingItems()[0];
                    k = (Personal)age[0];

                    DateTime a = (DateTime)k.BirthDate;

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
        public int CalculateAge(DateTime birthDate)
        {
            // cache the current time
            DateTime now = DateTime.Today;
            // get the difference in years
            int years = now.Year - birthDate.Year;
            // subtract another year if we're before the
            // birth day in the current year
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;
            return years;

        }
        public string getBloodPressure_Systolic(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(BloodPressure.TypeId);

                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection bp = searcher.GetMatchingItems()[0];

                    if (bp.Count.ToString().Equals("0"))
                        answer = "NA";

                    else
                    {
                        BloodPressure bpressure = bp[0] as BloodPressure;
                        answer=bpressure.Systolic.ToString();
                        return answer;
                    }
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
        public string getBloodPressure_Diastolic(HealthRecordInfo abc)
        {

            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(BloodPressure.TypeId);

                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection bp = searcher.GetMatchingItems()[0];

                    if (bp.Count.ToString().Equals("0"))
                        answer = "NA";

                    else
                    {
                        BloodPressure bpressure = bp[0] as BloodPressure;
                        answer = bpressure.Diastolic.ToString();
                    }
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
        public string getWeight(HealthRecordInfo abc)
        {

            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(Weight.TypeId);

                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection weights = searcher.GetMatchingItems()[0];


                    if (weights.Count.ToString().Equals("0"))
                        answer = "NA ";
                    else
                    {
                        Weight weight = weights[0] as Weight;
                        answer = weights[0].ToString();
                    }
                }
                else
                    answer = "NA";
                return answer;
            }
            catch (Exception e2)
            {
                answer = "NA";
                return answer;
            }
        }
        public string getAllergy(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(Allergy.TypeId);
                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);
                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection allergy = searcher.GetMatchingItems()[0];

                    int i = 0;

                    if (allergy.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                        answer = allergy[i].ToString();
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
        public string getTemperature(HealthRecordInfo abc)
        {


            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(VitalSigns.TypeId);

                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection temperature = searcher.GetMatchingItems()[0];
                    if (temperature.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                    {
                        VitalSigns vt = temperature[0] as VitalSigns;
                        answer = vt.VitalSignsResults[0].ToString();
                    }
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
        public string getHaemoglobin(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(HbA1C.TypeId);
                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);
                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection dp1 = searcher.GetMatchingItems()[0];

                    if (dp1.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                    {
                        HbA1C hb = dp1[0] as HbA1C;
                        answer = hb.ToString();
                    }
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
        public string getBloodGlucose_BeforeEating(HealthRecordInfo abc)
        {
            try
            {

                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(BloodGlucose.TypeId);
                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);
                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection bg1 = searcher.GetMatchingItems()[0];


                    if (bg1.Count.ToString().Equals("0"))
                        answer = "NA";

                    else
                    {
                        for (int i = 0; i < bg1.Count; i++)
                        {
                            BloodGlucose bg = bg1[i] as BloodGlucose;
                            if (bg.MeasurementContext.ToString().Equals("before meal"))
                            {
                                answer = bg.ToString().Remove(2);
                                break;
                            }
                            else
                                answer = "NA";
                        }
                    }
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
        public string getBloodGlucose_AfterEating(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(BloodGlucose.TypeId);
                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);
                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection bg1 = searcher.GetMatchingItems()[0];



                    if (bg1.Count.ToString().Equals("0"))
                        answer = "NA";

                    else
                    {
                        for (int i = 0; i < bg1.Count; i++)
                        {
                            BloodGlucose bg = bg1[i] as BloodGlucose;
                            if (bg.MeasurementContext.ToString().Equals("after meal"))
                            {
                                answer = bg.ToString().Remove(2);
                                break;
                            }
                            else
                                answer = "NA";
                        }
                    }
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
        public string getHighDensityLipid(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(CholesterolProfile.TypeId);
                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);
                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection cp = searcher.GetMatchingItems()[0];

                    if (cp.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                    {
                        CholesterolProfile cholestrol = cp[0] as CholesterolProfile;
                        answer = cholestrol.HDL.ToString();
                    }
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
        public string getLowDensityLipid(HealthRecordInfo abc)
        {


            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(CholesterolProfile.TypeId);
                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);
                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection cp = searcher.GetMatchingItems()[0];

                    if (cp.Count.ToString().Equals("0"))
                        return "NA";
                    else
                    {
                        CholesterolProfile cholestrol = cp[0] as CholesterolProfile;
                        return cholestrol.LDL.ToString();
                    }
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
        public string getTriglyceride(HealthRecordInfo abc)
        {

            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(CholesterolProfile.TypeId);
                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(24, 0, 0);
                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection cp = searcher.GetMatchingItems()[0];

                    if (cp.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                    {
                        CholesterolProfile cholestrol = cp[0] as CholesterolProfile;
                        answer = cholestrol.Triglyceride.ToString();
                    }
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
        public void getParambyfiltering(HealthRecordInfo  abc, decimal caseid)
        {
            //HealthRecordInfo abc = PersonInfo.SelectedRecord;
            string answer; int x;
            answer = getTemperature(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case 

            }
            else
            {
                x = (int)parameterMapping.Temperature;
                //Add Paramter to Case(x,caseid);           
                Operations.addParameterToCase(x, caseid);
            }
            answer = getWeight(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case             
            }
            else
            {
                x = (int)parameterMapping.Weight;
                //Add Paramter to Case(x,caseid);           
                Operations.addParameterToCase(x, caseid);
            }

            answer = getBloodPressure_Systolic(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case             
            }
            else
            {
                x = (int)parameterMapping.BloodPressureSystolic;
                //Add Paramter to Case(x,caseid);        
                Operations.addParameterToCase(x, caseid);
            }

            answer = getBloodPressure_Diastolic(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case             
            }
            else
            {
                x = (int)parameterMapping.BloodPressureDiastolic;
                //Add Paramter to Case(x,caseid);          
                Operations.addParameterToCase(x, caseid);
            }

          
            answer = getBloodGlucose_BeforeEating(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case             
            }
            else
            {
                x = (int)parameterMapping.BloodSugarLevelBeforeEating;
                //Add Paramter to Case(x,caseid);        
                Operations.addParameterToCase(x, caseid);
            }

            answer = getBloodGlucose_AfterEating(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case            
            }
            else
            {
                x = (int)parameterMapping.BloodSugarLevelAfterEating;
                //Add Paramter to Case(x,caseid);            
                Operations.addParameterToCase(x, caseid);
            }

            answer = getHaemoglobin(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case             
            }
            else
            {
                x = (int)parameterMapping.Haemoglobin;
                //Add Paramter to Case(x,caseid);
                //TextBox1.Text = x.ToString();
                //return x;
                Operations.addParameterToCase(x, caseid);
            }

            answer = getHighDensityLipid(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case 
            }
            else
            {
                x = (int)parameterMapping.HighDensityLipid;
                //Add Paramter to Case(x,caseid);
                Operations.addParameterToCase(x, caseid);

            }
            answer = getLowDensityLipid(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case 
            }
            else
            {
                x = (int)parameterMapping.LowDensityLipid;
                //Add Paramter to Case(x,caseid);        
                Operations.addParameterToCase(x, caseid);
            }
            answer = getTriglyceride(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case 
            }
            else
            {
                x = (int)parameterMapping.triglyceride;
                //Add Paramter to Case(x,caseid);     
                Operations.addParameterToCase(x, caseid);
            }
            answer = getPersonAge(abc);
            if (answer.Equals("NA"))
            {
                //Do Not Add Paramter to Case 
            }
            else
            {
                x = (int)parameterMapping.Age;
                //Add Paramter to Case(x,caseid);           
                Operations.addParameterToCase(x, caseid);
            }

        }
    }

    public class ParameterValues : HealthServicePage
    {
        public static string answer;
        public enum parameterMapping
        {
            Temperature = 1,
            Weight = 2,
            BloodPressureSystolic = 3,
            BloodPressureDiastolic = 4,
            BloodSugarLevelBeforeEating = 5,
            BloodSugarLevelAfterEating = 6,
            Haemoglobin = 7,
            HighDensityLipid = 8,
            LowDensityLipid = 9,
            triglyceride = 10,
            allergy = 11,
            Age = 12
        };
        public string getPersonAge(HealthRecordInfo abc)
        {

            try
            {
                Personal k;

                if (abc != null)
                {

                    PersonInfo.SelectedRecord = abc;

                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(Personal.TypeId);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection age = searcher.GetMatchingItems()[0];
                    k = (Personal)age[0];

                    DateTime a = (DateTime)k.BirthDate;

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
        public int CalculateAge(DateTime birthDate)
        {
            // cache the current time
            DateTime now = DateTime.Today;
            // get the difference in years
            int years = now.Year - birthDate.Year;
            // subtract another year if we're before the
            // birth day in the current year
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;
            return years;

        }
        public string getBloodPressure_Systolic(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(BloodPressure.TypeId);


                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection bp = searcher.GetMatchingItems()[0];

                    if (bp.Count.ToString().Equals("0"))
                        answer = "NA";

                    else
                    {
                        BloodPressure bpressure = bp[0] as BloodPressure;
                        answer = bpressure.Systolic.ToString();
                        return answer;
                    }
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
        public string getBloodPressure_Diastolic(HealthRecordInfo abc)
        {

            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(BloodPressure.TypeId);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection bp = searcher.GetMatchingItems()[0];

                    if (bp.Count.ToString().Equals("0"))
                        answer = "NA";

                    else
                    {
                        BloodPressure bpressure = bp[0] as BloodPressure;
                        answer = bpressure.Diastolic.ToString();
                    }
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
        public string getWeight(HealthRecordInfo abc)
        {

            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(Weight.TypeId);


                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection weights = searcher.GetMatchingItems()[0];


                    if (weights.Count.ToString().Equals("0"))
                        answer = "NA ";
                    else
                    {
                        Weight weight = weights[0] as Weight;
                        answer = weights[0].ToString().Remove(2);
                    }
                }
                else
                    answer = "NA";
                return answer;
            }
            catch (Exception e2)
            {
                answer = "NA";
                return answer;
            }
        }
        public string getAllergy(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(Allergy.TypeId);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection allergy = searcher.GetMatchingItems()[0];

                    int i = 0;

                    if (allergy.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                        answer = allergy[i].ToString();
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
        public string getTemperature(HealthRecordInfo abc)
        {


            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(VitalSigns.TypeId);



                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection temperature = searcher.GetMatchingItems()[0];
                    if (temperature.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                    {
                        VitalSigns vt = temperature[0] as VitalSigns;
                        answer = vt.VitalSignsResults[0].ToString();
                    }
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
        public string getHaemoglobin(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(HbA1C.TypeId);
                    filter.UpdatedDateMin = DateTime.Now - new TimeSpan(500, 0, 0);
                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection dp1 = searcher.GetMatchingItems()[0];

                    if (dp1.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                    {
                        HbA1C hb = dp1[0] as HbA1C;
                        answer = hb.ToString().Remove(2);
                    }
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
        public string getBloodGlucose_BeforeEating(HealthRecordInfo abc)
        {
            try
            {

                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(BloodGlucose.TypeId);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection bg1 = searcher.GetMatchingItems()[0];


                    if (bg1.Count.ToString().Equals("0"))
                        answer = "NA";

                    else
                    {
                        for (int i = 0; i < bg1.Count; i++)
                        {
                            BloodGlucose bg = bg1[i] as BloodGlucose;
                            if (bg.MeasurementContext.ToString().Equals("before meal"))
                            {
                                answer = bg.ToString().Remove(2);
                                break;
                            }
                            else
                                answer = "NA";
                        }
                    }
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
        public string getBloodGlucose_AfterEating(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(BloodGlucose.TypeId);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection bg1 = searcher.GetMatchingItems()[0];



                    if (bg1.Count.ToString().Equals("0"))
                        answer = "NA";

                    else
                    {
                        for (int i = 0; i < bg1.Count; i++)
                        {
                            BloodGlucose bg = bg1[i] as BloodGlucose;
                            if (bg.MeasurementContext.ToString().Equals("after meal"))
                            {
                                answer = bg.ToString().Remove(2);
                                break;
                            }
                            else
                                answer = "NA";
                        }
                    }
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
        public string getHighDensityLipid(HealthRecordInfo abc)
        {
            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(CholesterolProfile.TypeId);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection cp = searcher.GetMatchingItems()[0];

                    if (cp.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                    {
                        CholesterolProfile cholestrol = cp[0] as CholesterolProfile;
                        answer = cholestrol.HDL.ToString();
                    }
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
        public string getLowDensityLipid(HealthRecordInfo abc)
        {


            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(CholesterolProfile.TypeId);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection cp = searcher.GetMatchingItems()[0];

                    if (cp.Count.ToString().Equals("0"))
                        return "NA";
                    else
                    {
                        CholesterolProfile cholestrol = cp[0] as CholesterolProfile;
                        return cholestrol.LDL.ToString();
                    }
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
        public string getTriglyceride(HealthRecordInfo abc)
        {

            try
            {
                if (abc != null)
                {
                    HealthRecordSearcher searcher = abc.CreateSearcher();

                    HealthRecordFilter filter = new HealthRecordFilter(CholesterolProfile.TypeId);

                    searcher.Filters.Add(filter);

                    HealthRecordItemCollection cp = searcher.GetMatchingItems()[0];

                    if (cp.Count.ToString().Equals("0"))
                        answer = "NA";
                    else
                    {
                        CholesterolProfile cholestrol = cp[0] as CholesterolProfile;
                        answer = cholestrol.Triglyceride.ToString();
                    }
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
        public string get_Parameter(int id, HealthRecordInfo hri)
        {
            
            String parameter = "";
            switch (id)
            {

                case 1:
                    parameter = getTemperature(hri);
                    return parameter;
                case 2:
                    parameter = getWeight(hri);
                    return parameter;
                case 3:
                    parameter = getBloodPressure_Systolic(hri);
                    return parameter;
                case 4:
                    parameter = getBloodPressure_Diastolic(hri);
                    return parameter;
                case 5:
                    parameter = getBloodGlucose_BeforeEating(hri);
                    return parameter;
                case 6:
                    parameter = getBloodGlucose_AfterEating(hri);
                    return parameter;
                case 7:
                    parameter = getHaemoglobin(hri);
                    return parameter;
                case 8:
                    parameter = getHighDensityLipid(hri);
                    return parameter;
                case 9:
                    parameter = getLowDensityLipid(hri);
                    return parameter;
                case 10:
                    parameter = getTriglyceride(hri);
                    return parameter;
                case 11:
                    parameter = getAllergy(hri);
                    return parameter;
                case 12:
                    parameter = getPersonAge(hri);
                    return parameter;
                default:
                    return "Incorrect Call To Method get_Parameter";
            }
        }
    }
}