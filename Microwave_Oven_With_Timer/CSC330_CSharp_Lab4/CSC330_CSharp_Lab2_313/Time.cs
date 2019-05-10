using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC330_CSharp_Lab2_313
{
    /// <summary>
    /// Summary description for Time.
    /// </summary>
    public class Time
    {
        // declare ints for minute and second
        private int m_intMinute;
        private int m_intSecond;

        // Time constructor, minute and second supplied
        public Time(int minuteValue, int secondValue)
        {
            Minute = minuteValue; // invokes Minute set accessor
            Second = secondValue; // invokes Second set accessor

        } // end constructor Time

        // property Minute
        public int Minute
        {
            // return m_intMinute value
            get
            {
                return m_intMinute;

            } // end of get accessor

            // set m_intMinute value
            set
            {
                // if minute value entered is valid
                if (value < 60)
                {
                    m_intMinute = value;
                }
                else
                {
                    m_intMinute = 0; // set invalid input to 0
                }

            } // end of set accessor

        } // end property Minute

        // property Second
        public int Second
        {
            // return m_intSecond value
            get
            {
                return m_intSecond;

            } // end of get accessor

            // set m_intSecond value
            set
            {
                // if minute value entered is valid
                if (value < 60)
                {
                    m_intSecond = value;
                }
                else
                {
                    m_intSecond = 0; // set invalid input to 0
                }

            } // end of set accessor

        } // end property Second

    } // end class Time
}

