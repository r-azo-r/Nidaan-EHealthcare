using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Health;
using Microsoft.Health.Web;
using Microsoft.Health.ItemTypes;
namespace AKAV1
{
public class ParameterDisplay : HealthServicePage
{
       
  //public string getPersonAge(HealthRecordInfo abc)
  //  {

  //      try
  //      {
  //          Personal k;

  //          if (abc != null)
  //          {

  //              PersonInfo.SelectedRecord = abc;

  //              HealthRecordSearcher searcher = abc.CreateSearcher();

  //              HealthRecordFilter filter = new HealthRecordFilter(Personal.TypeId);
                
  //              searcher.Filters.Add(filter);

  //              HealthRecordItemCollection age = searcher.GetMatchingItems()[0];
  //              k = (Personal)age[0];

  //              DateTime a = (DateTime)k.BirthDate;

  //              if (k != null)
  //                  answer = CalculateAge(a).ToString();
  //              else answer = "NA";

  //          }

  //          else
  //          {
  //              answer = "NA";
  //          }

  //          return answer;

  //      }

  //      catch (Exception e2)
  //      {
  //          answer = "NA";
  //          return answer;
  //      }

  //  }
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
    public BloodPressure[] getBloodPressure(HealthRecordInfo abc)
    {

        try
        {
            if (abc != null)
            {
                HealthRecordSearcher searcher = abc.CreateSearcher();

                HealthRecordFilter filter = new HealthRecordFilter(BloodPressure.TypeId);

                searcher.Filters.Add(filter);

                HealthRecordItemCollection collection = searcher.GetMatchingItems()[0];

                BloodPressure[] pressure = new BloodPressure[collection.Count];

                for (int i = 0; i < collection.Count; i++)
                {

                    pressure[i] = collection[i] as BloodPressure;
                }
                if (pressure.Length != 0)
                    return pressure;
                else
                    return null;

            }
            return null;
        }
        catch (Exception e2)
        {
            return null;
        }
    }
    public Weight[] getWeight(HealthRecordInfo abc)
    {

        try
        {
            if (abc != null)
            {
                HealthRecordSearcher searcher = abc.CreateSearcher();

                HealthRecordFilter filter = new HealthRecordFilter(Weight.TypeId);


                searcher.Filters.Add(filter);

                HealthRecordItemCollection collection = searcher.GetMatchingItems()[0];

                Weight[] weight = new Weight[collection.Count];

                for (int i = 0; i < collection.Count; i++)
                {

                    weight[i] = collection[i] as Weight;
                }
                if (weight.Length != 0)
                    return weight;
                else
                    return null;
            }
            return null;
        }
        catch (Exception e2)
        {
           return null;
        }
    }
    public Allergy[] getAllergy(HealthRecordInfo abc)
    {
        try
        {
            if (abc != null)
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
                else 
                    return null;
            }
            return null;
        }
        catch (Exception e2)
        {
          return null;
        }
    }
    public VitalSigns[] getTemperature(HealthRecordInfo abc)
    {


        try
        {
            if (abc != null)
            {
                HealthRecordSearcher searcher = abc.CreateSearcher();

                HealthRecordFilter filter = new HealthRecordFilter(VitalSigns.TypeId);               

                searcher.Filters.Add(filter);

                HealthRecordItemCollection collection = searcher.GetMatchingItems()[0];

                VitalSigns[] temp = new VitalSigns[collection.Count];

                for (int i = 0; i < collection.Count; i++)
                {
                    temp[i] = collection[i] as VitalSigns;
                }
                if (temp.Length != 0)
                    return temp;
                else
                    return null;

               /* if (temperature.Count.ToString().Equals("0"))
                    answer = "NA";
                else
                {
                    VitalSigns vt = temperature[0] as VitalSigns;
                    answer = vt.VitalSignsResults[0].ToString();
                }
                * */
            }
            
            return null;
        }
        catch (Exception e2)
        {
            return null;
        }
    }
    public  HbA1C[] getHaemoglobin(HealthRecordInfo abc)
    {
        try
        {
            if (abc != null)
            {
                HealthRecordSearcher searcher = abc.CreateSearcher();

                HealthRecordFilter filter = new HealthRecordFilter(HbA1C.TypeId);

                searcher.Filters.Add(filter);

                HealthRecordItemCollection collection = searcher.GetMatchingItems()[0];

                HbA1C[] temp = new HbA1C[collection.Count];

                for (int i = 0; i < collection.Count; i++)
                {
                    temp[i] = collection[i] as HbA1C;
                }
                if (temp.Length != 0)
                    return temp;
                else
                    return null;
            }
            return null;
        }
        catch (Exception e2)
        {
           return null;
        }
    }
    public BloodGlucose[] getBloodGlucose(HealthRecordInfo abc)
    {
        try
        {

            if (abc != null)
            {
                HealthRecordSearcher searcher = abc.CreateSearcher();

                HealthRecordFilter filter = new HealthRecordFilter(BloodGlucose.TypeId);
           
                searcher.Filters.Add(filter);

                HealthRecordItemCollection collection = searcher.GetMatchingItems()[0];

                BloodGlucose[] temp = new BloodGlucose[collection.Count];

                for (int i = 0; i < collection.Count; i++)
                {
                    temp[i] = collection[i] as BloodGlucose;
                }
                if (temp.Length != 0)
                    return temp;
                else
                    return null;
                //if (bg1.Count.ToString().Equals("0"))
                //    answer = "NA";

                //else
                //{
                //    for (int i = 0; i < bg1.Count; i++)
                //    {
                //        BloodGlucose bg = bg1[i] as BloodGlucose;
                //        if (bg.MeasurementContext.ToString().Equals("before meal"))
                //        {
                //            answer = bg.ToString().Remove(2);
                //            break;
                //        }
                //        else
                //            answer = "NA";
                //    }
                //}
            }
            return null;
           
        }
        catch (Exception e2)
        {
            return null;
        }
    }   
    public CholesterolProfile[] getCholestrolProfile(HealthRecordInfo abc)
    {
        try
        {
            if (abc != null)
            {
                HealthRecordSearcher searcher = abc.CreateSearcher();

                HealthRecordFilter filter = new HealthRecordFilter(CholesterolProfile.TypeId);
           
                searcher.Filters.Add(filter);

                HealthRecordItemCollection collection = searcher.GetMatchingItems()[0];

               CholesterolProfile[] temp = new CholesterolProfile[collection.Count];
                
                for (int i = 0; i < collection.Count; i++)
                {
                    temp[i] = collection[i] as CholesterolProfile;
                }
                if (temp.Length != 0)
                    return temp;
                else
                    return null;
                //if (cp.Count.ToString().Equals("0"))
                //    answer = "NA";
                //else
                //{
                //    CholesterolProfile cholestrol = cp[0] as CholesterolProfile;
                //    answer = cholestrol.HDL.ToString();
                //}
            }    
            return null;
        }
        catch (Exception e2)
        {
           return null;
        }
    }
   
    
}
}