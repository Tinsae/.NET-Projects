using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC330_CSharp_Lab2_313
{
    public partial class MicrowaveOven : Form
    {

        // panel for the microwave's window
        //private System.Windows.Forms.Panel pnlWindow;
        // contains time entered as a string
        string m_strTime = "";

        // contains time entered
        Time m_objTime;

        public MicrowaveOven()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int intSecond;
            int intMinute;

            // ensure that m_strTime has 4 characters
            m_strTime = m_strTime.PadLeft(4, Convert.ToChar("0"));

            // extract seconds and minutes
            intSecond = Int32.Parse(m_strTime.Substring(2));
            intMinute = Int32.Parse(m_strTime.Substring(0, 2));

            // create Time object to contain time entered by user
            m_objTime = new Time(intMinute, intSecond);

            lblDisplay.Text = String.Format("{0:D2}:{1:D2}", m_objTime.Minute, m_objTime.Second);

            m_strTime = ""; // clear m_strTime for future input

            //tmrClock.Enabled = true; // start timer
            timer1.Enabled = true;
            pnlWindow.BackColor = Color.Yellow; // turn "light" on

        } // end method btnStart_Click

        // method to display formatted time is timer window
        private void DisplayTime()
        {
            int intSecond;
            int intMinute;

            string strDisplay; // string displays current input

            // if too much input entered
            if( m_strTime.Length > 4)
            {
                m_strTime = m_strTime.Substring(0, 4);
            }

            strDisplay = m_strTime.PadLeft(4, Convert.ToChar("0"));

            // extract seconds and minutes
            intSecond = Int32.Parse(strDisplay.Substring(2));
            intMinute = Int32.Parse(strDisplay.Substring(0, 2));

            // display number of minutes, ":" and number of seconds
            lblDisplay.Text = String.Format("{0:D2}:{1:D2}", intMinute, intSecond);

        } // end method DisplayTime

        //private void tmrClock_Tick(object sender, System.EventArgs e )
        //{
           

        //} // end method tmrClock_Tick

        private void btnOne_Click(object sender, EventArgs e)
        {
            m_strTime += "1"; // append digit to time input
            DisplayTime(); // display time input properly
        } // end method btnOne_click

        private void btnTwo_Click(object sender, EventArgs e)
        {
            m_strTime += "2"; // append digit to time input
            DisplayTime(); // display time input properly
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            m_strTime += "3"; // append digit to time input
            DisplayTime(); // display time input properly
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            m_strTime += "4"; // append digit to time input
            DisplayTime(); // display time input properly
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            m_strTime += "5"; // append digit to time input
            DisplayTime(); // display time input properly
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            m_strTime += "6"; // append digit to time input
            DisplayTime(); // display time input properly
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            m_strTime += "7"; // append digit to time input
            DisplayTime(); // display time input properly
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            m_strTime += "8"; // append digit to time input
            DisplayTime(); // display time input properly
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            m_strTime += "9"; // append digit to time input
            DisplayTime(); // display time input properly
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            m_strTime += "0"; // append digit to time input
            DisplayTime(); // display time input properly
        }

        private void lblDisplay_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // reset each property or variable to its initial setting
            lblDisplay.Text = "Microwave Oven";
            m_strTime = "";
            m_objTime = new Time(0, 0);
            // tmrClock.Enabled = false;
            timer1.Enabled = false;
            pnlWindow.BackColor = SystemColors.Control;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // perform countdown, subtract one second
            if (m_objTime.Second > 0)
            {
                m_objTime.Second--;
            }
            else if (m_objTime.Minute > 0)
            {
                m_objTime.Minute--; // subtract one minute
                m_objTime.Second = 59; // reset seconds for new minute
            }
            else // no more seconds
            {
                //tmrClock.Enabled = false;
                timer1.Enabled = false;
                lblDisplay.Text = "Done!";
                pnlWindow.BackColor = SystemColors.Control;
                return;
            }

            lblDisplay.Text = String.Format("{0:D2}:{1:D2}", m_objTime.Minute, m_objTime.Second);

        }
    }
}
