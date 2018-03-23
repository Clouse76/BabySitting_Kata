using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySitterKata
{
    /// <summary>
    /// this class performs the calculations for the work done.  
    /// There could be an arguement made to move alot of the data validations to this class. 
    /// </summary>
    class HoursCalculator
    {
        public int TotalStandardTime = 0;
        public int TotalBedTime = 0;
        public int TotalOverTime = 0;
        public double StandardTimePrice = 0;
        public double BedTimePrice = 0;
        public double OverTimePrice = 0;

        //I figure these should become adjustable at some point.
        private double standardRate = 12;
        private double bedRate = 8;
        private double overTimeRate = 16;

        public void CalculatePay(string startTime, String endTime, String bedTime, bool EndPM, bool BedPM)
        {
            TimeSpan startTimeSpan = TimeSpan.Parse(startTime);
            startTimeSpan = startTimeSpan.Add(new TimeSpan(12, 0, 0));
            TimeSpan endTimeSpan = TimeSpan.Parse(endTime);
            if (EndPM)
            {
                endTimeSpan = endTimeSpan.Add(new TimeSpan(12, 0, 0));
            }
            int totalBedTime = 0;
            int totalStandardTime = 0;
            int totalOverTime = 0;
            if (bedTime.Length > 0)                                                                               //check to see if the kid went to bed.
            {
                TimeSpan bedTimespan = TimeSpan.Parse(bedTime);
                if (BedPM)
                {
                    bedTimespan = bedTimespan.Add(new TimeSpan(12, 0, 0));
                }
                if(endTimeSpan < new TimeSpan(24, 0, 0) && endTimeSpan > new TimeSpan(4,0,0))                   //Make sure no overtime is due.
                {
                    totalBedTime = (endTimeSpan - bedTimespan).Hours;                                           //calculate bedtime before endtime if the dont' make it midnight
                }
                else
                {
                    if(bedTimespan > new TimeSpan(4, 0, 0))
                    {
                        totalBedTime = (new TimeSpan(24, 0, 0) - bedTimespan).Hours;                           //still at work after bed time and after midnight - overtime will be awarded. I mean really what kind of babysitter are you?
                    }
                }
                if(bedTimespan < new TimeSpan(4, 0, 0))                                                        //if the kid didn't get to bed until after midnight bed time doesn't need to calculated.
                {
                    totalStandardTime = 7;
                }
                else
                {
                    totalStandardTime = bedTimespan.Hours - startTimeSpan.Hours;                                //calculate standard time before bed time hours begin.
                }
            }
            else                                                                                               //The kid stayed up.                    
            {
                if(endTimeSpan > new TimeSpan(4, 0, 0))
                {
                    totalStandardTime = (endTimeSpan - startTimeSpan).Hours;                                   //no bed but ended before midnight
                }
                else
                {
                    totalStandardTime = 7;                                                                    //no bed before midnight full standard hours earned.
                }
            }
            if(endTimeSpan <= new TimeSpan(4, 0, 0))                                                           //Check for overtime.
            {
                totalOverTime = endTimeSpan.Hours;
            }

            //total up the pay.
            TotalBedTime = totalBedTime;
            TotalOverTime = totalOverTime;
            TotalStandardTime = totalStandardTime;
            StandardTimePrice = totalStandardTime * standardRate;
            BedTimePrice = totalBedTime * bedRate;
            OverTimePrice = totalOverTime * overTimeRate;
            
            //assign the results.
        }
    }
}
