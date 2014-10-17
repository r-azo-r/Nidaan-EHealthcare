using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AKAV1
{
    public class Calculator
    {
        public void calculateSmoke()
        { }
            public void calculateSmoke(int  years, int smokes,double price,out double w,out double p)
            {
                // DO CALCULATION OF NUMBER OF CIGARETTES SMOKED ROUNDED TO WHOLE NUMBER

                 int smoked = (int)(years * 365.25 * smokes);

                 // CALCULATE TODAY'S VALUE OF ALL CIGS ROUNDED TO TWO DECIMAL PLACES

                 double expense = (double)(((smoked / 20) * price) * 100) / 100;
                
                 // CALCULATE WEIGHT OF TOBACCO IN GRAMS AND POUNDS TO TWO DECIMAL PLACES

                 double weight = ((smoked * 0.68) * 100) / 100;
                 double pounds =(((weight / 1000) * 2.204) * 100) / 100;
                 w = weight;
                 p = pounds;
               
                //RESULT IN STRING FORM
                string res ="You smoked approximately " + smoked + " cigarettes.<br>At current prices, that would cost $"
                    + expense + ".<br>And the weight of the tobacco in all those cigarettes was " 
                    + pounds + " pounds (" + weight +" grams).";
             }

            public void dailyCalorieNeeds(int weight,string wunit,double height,string hunit,int age,int activity,string gender)
            {

                string res="";

                if (weight == 0) res="Results will be inaccurate.  Weight is not a valid number.";
               
                if (height == 0) res="Results will be inaccurate.  Height is not a valid number.";
                
                if (age == 0) res="Results will be inaccurate.  Age is not a valid number.";

                double wmult = (wunit.Equals("pounds") )? 2.204 : 1;
                double hmult = (hunit.Equals("inches") )? 2.54 : 1;
                double genvar = (gender.Equals("male") )? 5 : -161;

                int BMR = (int)((((weight / wmult) * 9.99) + ((height * hmult) * 6.25) - (age * 4.92) + genvar) * 1);

                int calburn = (int)((((weight / wmult) * 9.99) + ((height * hmult) * 6.25) - (age * 4.92) + genvar) * activity);

                res="Your Basal Metabolic Rate is: " + BMR +
                    " calories.<br><br>Your Average Daily Calorie Need Is: " + calburn + " calories";
                

            }

            public void BMIcalc(int weight,string wunit,double height,string hunit)
            {
                
                double wmult = (wunit.Equals("pounds")) ? 2.204 : 1;
                var hmult = (hunit.Equals("inches"))? .0254 : .01;

                
                double BMI=(((weight / wmult)/((height * hmult)*(height * hmult))) *10)/10;
                string result="";
                

                if(BMI < 16.5) result = "severely underweight";
                else if((BMI >=16.5)&&(BMI<=18.49)) result = "underweight";
                else if((BMI >=18.5)&&(BMI<=25)) result = "normal";
                else if((BMI >=25.01)&&(BMI<=30)) result = "overweight";
                if((BMI >=30.01)&&(BMI<=35)) result = "obese";
                else if((BMI >=35.01)&&(BMI<=40)) result = "clinically obese";
                else result = "morbidly obese";

                string res="";
                res="Your Body Mass Index (BMI) is: " + BMI + ".<br><br>This would be considered " 
                    + result + ".";


            }
    }
    
}