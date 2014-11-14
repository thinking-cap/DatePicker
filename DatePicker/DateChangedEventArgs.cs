/*
 *	DatePickerCtrl
 *	
 *  A simple DatePicker control for ASP.NET.
 *  Created in 2011 by Simon Baer, originally based on code by Tan Ling	Wee.
 * 
 *  Licensed under the Code Project Open License (CPOL).
 */

using System;

namespace Agile.ThinkingCap.DatePickerCtrl
{
    /// <summary>
    /// Arguments of the DateChanged event.
    /// </summary>
    public class DateChangedEventArgs : EventArgs
    {
        private DateTime date;

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="date"></param>
        public DateChangedEventArgs(DateTime date)
        {
            this.date = date;
        }

        /// <summary>
        /// Gets the new date.
        /// </summary>
        public DateTime Date
        {
            get { return date; }
        }
    }
}
