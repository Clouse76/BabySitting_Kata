﻿using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BabySitterKata
{
    /// <summary>
    /// this class performs the calculations for the work done.  
    /// There could be an arguement made to move alot of the data validations to this class.
    /// on one hand I feel like like the UI should do those checks, but if this class was really to stand on it's own
    /// the validation can go in here.
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
        private TimeSpan millitaryTime = new TimeSpan(12, 0, 0);
        private TimeSpan latestEndTime = new TimeSpan(4, 0, 0);
        private TimeSpan earliestStartTime = new TimeSpan(17, 0, 0);
        private bool endPM = false;
        private bool bedPM = false;

        /// <summary>
        /// This Function executes all the buisness rules to calculate how many hours in each catagory and what the charges will be.
        /// I did make a decision that working after midnight takes precadence over bed time. 
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="bedTime"></param>
        /// <param name="EndPM"></param>
        /// <param name="BedPM"></param>
        public void CalculatePay(string startTime, String endTime, String bedTime, bool EndPM, bool BedPM)
        {
            //take the string inputs and convert them to a Timespan.
            TimeSpan startTimeSpan = TimeSpan.Parse(startTime);
            startTimeSpan = startTimeSpan.Add(millitaryTime);  //since we already checked that this is a pm time get the military time.
            TimeSpan endTimeSpan = TimeSpan.Parse(endTime);

            if (EndPM)
            {
                endTimeSpan = endTimeSpan.Add(millitaryTime);
            }
            int totalBedTime = 0;
            int totalStandardTime = 0;
            int totalOverTime = 0;
            if (bedTime.Length > 0)                                                                        //check to see if the kid went to bed.
            {
                TimeSpan bedTimespan = TimeSpan.Parse(bedTime);
                if (BedPM)
                {
                    bedTimespan = bedTimespan.Add(millitaryTime);
                }
                if (endTimeSpan < new TimeSpan(24, 0, 0) && endTimeSpan > latestEndTime)                //Make sure no overtime is due.
                {
                    totalBedTime = (endTimeSpan - bedTimespan).Hours;                                      //calculate bedtime before endtime if the dont' make it midnight
                }
                else
                {
                    if (bedTimespan > latestEndTime)
                    {
                        totalBedTime = (new TimeSpan(24, 0, 0) - bedTimespan).Hours;                       //still at work after bed time and after midnight - overtime will be awarded. I mean really what kind of babysitter are you?
                    }
                }
                if (bedTimespan < latestEndTime)                                                        //if the kid didn't get to bed until after midnight bed time doesn't need to calculated.
                {
                    totalStandardTime = 7;
                }
                else
                {
                    totalStandardTime = bedTimespan.Hours - startTimeSpan.Hours;                           //calculate standard time before bed time hours begin.
                }
            }
            else                                                                                           //The kid stayed up.                    
            {
                if (endTimeSpan > latestEndTime)
                {
                    totalStandardTime = (endTimeSpan - startTimeSpan).Hours;                              //no bed but ended before midnight
                }
                else
                {
                    totalStandardTime = 7;                                                                //no bed before midnight full standard hours earned.
                }
            }
            if (endTimeSpan <= latestEndTime)                                                          //Check for overtime.
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
        }

        /// <summary>
        /// This function makes sure that all of the user imput is in the correct format and logically accurate.
        /// will return true if any issues are detected.  there is a great deal of code here and has an oppertunity to be refactored.
        /// </summary>
        public StringBuilder CheckForBadData(string startTime, String endTime, String bedTime, bool EndPM, bool BedPM)
        {
            //Create a stringbuilder to hold all the potential issues.
            StringBuilder issues = new StringBuilder();
            bool goodStart = true, goodEnd = true, goodBed = true;

            //check to see if the time is in a valid format inform the user wich one is incorrect. mark them as false if they are bad so further calculations will not occur.
            if (IsValidTime(startTime) == false)
            {
                issues.AppendLine("Start Time is not in the correct time format.");
                goodStart = false;
            }
            if (bedTime.Length > 0 && IsValidTime(bedTime) == false)
            {
                issues.AppendLine("Bed Time is not in the correct time format.");
                goodBed = false;
            }
            if (IsValidTime(endTime) == false)
            {
                issues.AppendLine("End Time is not in the correct time format.");
                goodEnd = false;
            }

            //make sure we have good formatting before checking if they are logically correct.
            if (goodStart && goodEnd)
            {
                TimeSpan startTimeSpan = TimeSpan.Parse(startTime);
                startTimeSpan = startTimeSpan.Add(millitaryTime);                      //put the start time in the 24-hour format
                TimeSpan endTimeSpan = TimeSpan.Parse(endTime);
                if (EndPM)
                {
                    endTimeSpan = endTimeSpan.Add(millitaryTime);                      //if "PM" is selected put end time in the 24-hour format
                }
                if (startTimeSpan < earliestStartTime)
                {
                    issues.AppendLine("Start time cannot be before 5pm");
                }
                if (endTimeSpan < earliestStartTime && endTimeSpan > latestEndTime)   // make sure end time is less than 4am and greater than 5pm.
                {
                    issues.AppendLine("End time can't be past 4AM or before 5PM");
                }
                //Check to see if there is a valid bed time and make sure it happens durring the shift.
                if (goodBed && bedTime.Length > 0)
                {
                    TimeSpan bedTimeSpan = TimeSpan.Parse(bedTime);
                    if (BedPM)                                       //if "PM" is selected put end time in the 24-hour format
                    {
                        bedTimeSpan = bedTimeSpan.Add(millitaryTime);
                    }
                    if ((bedTimeSpan < startTimeSpan && bedTimeSpan > latestEndTime) || (bedTimeSpan > endTimeSpan && endTimeSpan > latestEndTime))
                    {
                        issues.AppendLine("Please enter a valid bed time. It has to be durring your shift.");
                    }
                }
            }
            return issues;
        }

        /// <summary>
        /// This Function checks to make sure a valid time is entered.  
        /// will return "true" if the string is in a proper time format, "false" if it isn't 
        /// </summary>
        /// <param name="thetime"></param>
        /// <returns></returns>
        public bool IsValidTime(string thetime)
        {
            Regex checktime =
                new Regex(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");

            return checktime.IsMatch(thetime);
        }
    }
}