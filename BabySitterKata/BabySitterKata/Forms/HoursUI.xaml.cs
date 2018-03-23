using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace BabySitterKata
{
    /// <summary>
    /// This function of this program is to calculate the nightly charge for a babysitter.
    /// The buisness rules are:
    /// starts no earlier than 5:00PM
    /// leaves no later than 4:00AM
    /// gets paid $12/hour from start-time to bedtime
    /// gets paid $8/hour from bedtime to midnight
    /// gets paid $16/hour from midnight to end of job
    /// gets paid for full hours(no fractional hours)
    /// </summary>
    public partial class MainWindow : Window
    {
        bool endPM = false;
        bool bedPM = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This Function Takes the values input on the UI checks for the validity of the data.
        /// if everything checks out it passes the data to the HoursCalculator class and returns the hours and pay earned.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(CheckForBadData() == false)
                {   
                    HoursCalculator DailyResults = new HoursCalculator();
                    DailyResults.CalculatePay(txtStartTime.Text, txtEndTime.Text, txtBedTime.Text, endPM, bedPM);
                    lblBedHours.Content = DailyResults.TotalBedTime;
                    lblstandardHours.Content = DailyResults.TotalStandardTime;
                    lblOvertimeHours.Content = DailyResults.TotalOverTime;
                    lblTotalHours.Content = DailyResults.TotalBedTime + DailyResults.TotalStandardTime + DailyResults.TotalOverTime;
                    lblGrossPay.Content = DailyResults.StandardTimePrice + DailyResults.BedTimePrice + DailyResults.OverTimePrice;
                    lblstandardPay.Content = DailyResults.StandardTimePrice;
                    lblOvertimePay.Content = DailyResults.OverTimePrice;
                    lblBedPay.Content = DailyResults.BedTimePrice;
                    endPM = false;
                    bedPM = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.InnerException + Environment.NewLine + ex.StackTrace, "Error", MessageBoxButton.OK);
            }

        }

        /// <summary>
        /// This Button closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// This function makes sure that all of the user imput is in the correct format and logically accurate.
        /// will return true if any issues are detected.  there is a great deal of code here and has an oppertunity to be refactored.
        /// </summary>
        private bool CheckForBadData()
        {
            //Create a stringbuilder to hold all the potential issues.
            StringBuilder issues = new StringBuilder();
            bool goodStart = true, goodEnd = true, goodBed = true;

            //check to see if the time is in a valid format inform the user wich one is incorrect. mark them as false if they are bad so further calculations will not occur.
            if (IsValidTime(txtStartTime.Text) == false)
            {
                issues.AppendLine("Start Time is not in the correct time format.");
                goodStart = false;
            }
            if(txtBedTime.Text.Length > 0 && IsValidTime(txtBedTime.Text) == false)
            {
                issues.AppendLine("Bed Time is not in the correct time format.");
                goodBed = false;
            }
            if(IsValidTime(txtEndTime.Text) == false)
            {
                issues.AppendLine("End Time is not in the correct time format.");
                goodEnd = false;
            }

            //make sure we have good formatting before checking if they are logically correct.
            if(goodStart && goodEnd)
            {
                TimeSpan startTime = TimeSpan.Parse(txtStartTime.Text);
                startTime = startTime.Add(new TimeSpan(12, 0, 0));                      //put the start time in the 24-hour format
                TimeSpan endTime = TimeSpan.Parse(txtEndTime.Text);
                if (rbEndPM.IsChecked == true)
                {
                    endTime = endTime.Add(new TimeSpan(12, 0, 0));                      //if "PM" is selected put end time in the 24-hour format
                    endPM = true;
                }
                if(startTime < new TimeSpan(17, 0, 0))
                {
                    issues.AppendLine("Start time cannot be before 5pm");
                }
                if(endTime < new TimeSpan(17,0,0) && endTime > new TimeSpan(4, 0, 0))   // make sure end time is less than 4am and greater than 5pm.
                {
                    issues.AppendLine("End time can't be past 4AM or before 5PM");
                }
                //Check to see if there is a valid bed time and make sure it happens durring the shift.
                if (goodBed && txtBedTime.Text.Length > 0)
                {
                    TimeSpan bedTime = TimeSpan.Parse(txtBedTime.Text);
                    if(rbBedPM.IsChecked == true)                                       //if "PM" is selected put end time in the 24-hour format
                    {
                        bedTime = bedTime.Add(new TimeSpan(12, 0, 0));
                        bedPM = true;
                    }
                    if((bedTime < startTime && bedTime > new TimeSpan(4,0,0)) || (bedTime > endTime && endTime > new TimeSpan(4,0,0)))
                    {
                        issues.AppendLine("Please enter a valid bed time. It has to be durring your shift.");
                    }
                }
            }

            //is there are issues then show them and return false.
            if (issues.Length > 0)
            {
                MessageBox.Show(issues.ToString(), "The Following issues need resolved.", MessageBoxButton.OK);
                return true;
            }
            else
            {
                //we are good to go.
                return false;
            }
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