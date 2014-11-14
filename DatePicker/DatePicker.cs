using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Agile.ThinkingCap.DatePickerCtrl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:DatePicker runat=server></{0}:DatePicker>")]
    [ValidationPropertyAttribute("CalendarDateString")]
    public class DatePicker : CompositeControl
    {

        TextBox datepicker;

        [Description("Fires when the date has been changed and AutoPostBack is set to 'true'.")]
        public event EventHandler<DateChangedEventArgs> DateChanged;
    
        private bool allowType;  // I KEEP It For Compatibility
        public bool AllowType
        {
            get
            {
                EnsureChildControls();
                return allowType;
            }
            set
            {
                EnsureChildControls();
                allowType = value;
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        public string CalendarDateString
        {
            get
            {
                EnsureChildControls();
                return datepicker.Text;
            }
        }

        [Category("Appearance")]
        [Bindable(true, BindingDirection.TwoWay)]
        [Browsable(true)]
        public DateTime CalendarDate
        {
            get
            {
                EnsureChildControls();
                DateTime date;
                string SelectedDate = datepicker.Text ;
                if (DateTime.TryParseExact(SelectedDate, DateFormat, null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                    return DateTime.MinValue;
            }
            set
            {

                EnsureChildControls();
                if (value != null)
                {
                    datepicker.Text = value.ToString(DateFormat);
                }
            }
        }

        public bool IsValidDate
        {
            get
            {
                EnsureChildControls();
                DateTime date;
                string SelectedDate = datepicker.Text;
                return DateTime.TryParseExact(SelectedDate, DateFormat, null, System.Globalization.DateTimeStyles.None, out date);
            }
        }

        private string dateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToLower().Replace("yyyy","yy");
        [Category("Appearance")]
        [Description("Date format, e.g. 'dd.MM.yyyy' or 'MM/dd/yyyy'.")]
        [Browsable(true)]
        public string DateFormat
        {
            get
            {
                EnsureChildControls();
                string result = dateFormat.Replace("yy", "yyyy");
                result = result.Replace("M", "MMM"); 
                result = result.Replace("mm", "MM");
                return result;
            }
            set
            {
                EnsureChildControls();
                string result = value.Replace("yyyy", "yy").Replace("YYYY", "yy");
                result = result.Replace("MMM", "M");
                result = result.Replace("MM", "mm");
                dateFormat = result;
            }
        }

        private void ResetDateFormat()
        {
            DateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        }

        private bool ShouldSerializeDateFormat()
        {
            return (!DateFormat.Equals(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected override void CreateChildControls()
        {
            Controls.Clear();

            datepicker = new TextBox();
            datepicker.Style.Add("background", "url(images/calendar/calendar.gif) no-repeat 100px 0px; padding:2px");
            datepicker.Style.Add("width", "120px");
            datepicker.Attributes.Add ("onkeydown", "if (event.keyCode != 8) return false; else this.value = '';" );
            datepicker.Attributes.Add("onmousedown", "$(this).datepicker({yearRange: '-100:+15', changeMonth: true, changeYear: true, showOtherMonths: true, dateFormat: \"" + dateFormat + "\" });");
            this.Controls.Add(datepicker);
        }
    }
}
