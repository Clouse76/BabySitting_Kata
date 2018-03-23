using System;
using System.Text;
using System.Windows;

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
                if(rbBedPM.IsChecked == true)
                {
                    bedPM = true;
                }  //since the raido button isClicked can have a null value I have to do this check.  messier than a checkbox but i like the look better.
                else
                {
                    bedPM = false;
                }
                if(rbEndPM.IsChecked == true)
                {
                    endPM = true;
                }
                else
                {
                    endPM = false;
                }
                HoursCalculator DailyResults = new HoursCalculator();
                StringBuilder ValidationIssues = DailyResults.CheckForBadData(txtStartTime.Text, txtEndTime.Text, txtBedTime.Text, endPM, bedPM);
                if (ValidationIssues.Length == 0)
                {   

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
                else
                {
                    MessageBox.Show(ValidationIssues.ToString(), "The Following issues need resolved.", MessageBoxButton.OK);
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
    }
}