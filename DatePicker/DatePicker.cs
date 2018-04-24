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

        private TextBox datepicker;

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

        private string dateFormat;// = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToLower().Replace("yyyy","yy");
        [Category("Appearance")]
        [Description("Date format, e.g. 'dd.MM.yyyy' or 'MM/dd/yyyy'.")]
        [Browsable(true)]
        //LMS date formats: "M/d/yyyy", "M/d/yy", "MM/dd/yy", "MM/dd/yyyy", "yy/MM/dd", "yyyy-MM-dd", "dd-MMM-yyyy", "dd/MM/yyyy"
        public string DateFormat
        {
            get
            {
                EnsureChildControls();
                //string result = dateFormat.Replace("yy", "yyyy");
                //result = result.Replace("M", "MMM"); 
                //result = result.Replace("mm", "MM");
                string result = "";
                switch (dateFormat)
                {
                    case "m/d/yy":
                        result = "M/d/yyyy";
                        break;
                    case "m/d/y":
                        result = "M/d/yy";
                        break;
                    case "mm/dd/y":
                        result = "MM/dd/yy";
                        break;
                    case "mm/dd/yy":
                        result = "MM/dd/yyyy";
                        break;
                    case "y/mm/dd":
                        result = "yy/MM/dd";
                        break;
                    case "yy-mm-dd":
                        result = "yyyy-MM-dd";
                        break;
                    case "dd-M-yy":
                        result = "dd-MMM-yyyy";
                        break;
                    case "dd/mm/yy":
                        result = "dd/MM/yyyy";
                        break;
                    case "MMM-dd-yyyy":
                        result = "M-dd-yy";
                        break;
                }
                return result;
            }
            set
            {
                EnsureChildControls();
                //string result = value.Replace("yyyy", "yy").Replace("YYYY", "yy");
                //result = result.Replace("MMM", "M");
                //result = result.Replace("MM", "mm");
                //dateFormat = result;
                string result = "";
                switch(value)
                {
                    case "M/d/yyyy":
                        result = "m/d/yy";
                        break;
                    case "M/d/yy":
                        result = "m/d/y";
                        break;
                    case "MM/dd/yy":
                        result = "mm/dd/y";
                        break;
                    case "MM/dd/yyyy":
                        result = "mm/dd/yy";
                        break;
                    case "yy/MM/dd":
                        result = "y/mm/dd";
                        break;
                    case "yyyy-MM-dd":
                        result = "yy-mm-dd";
                        break;
                    case "dd-MMM-yyyy":
                        result = "dd-M-yy";
                        break;
                    case "dd/MM/yyyy":
                        result = "dd/mm/yy";
                        break;
                    case "MMM-dd-yyyy":
                        result = "M-dd-yy";
                        break;
                }
                if (string.IsNullOrEmpty(result))
                    result = "dd/mm/yy";
                dateFormat = result;
            }
        }

        public void Clear()
        {
            EnsureChildControls();
            datepicker.Text = "";
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

        protected override void OnPreRender(EventArgs e)
        {
            if (string.IsNullOrEmpty(dateFormat))
                dateFormat = "dd/mm/yy";
            datepicker.Attributes.Add("onkeydown", "if (event.keyCode != 8) return false; else this.value = '';");
            datepicker.Attributes.Add("onmousedown", "$(this).datepicker({yearRange: '-100:+15', changeMonth: true, changeYear: true, showOtherMonths: true, dateFormat: \"" + dateFormat + "\" });");
            datepicker.Attributes.Add("class", "form-control datepicker");
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            datepicker = new TextBox();
            datepicker.Style.Add("background", "url(images/calendar/calendar.gif) no-repeat 100px 0px; padding:2px");
            datepicker.Style.Add("width", "120px");
            //datepicker.Attributes.Add ("onkeydown", "if (event.keyCode != 8) return false; else this.value = '';" );
            //datepicker.Attributes.Add("onmousedown", "$(this).datepicker({yearRange: '-100:+15', changeMonth: true, changeYear: true, showOtherMonths: true, dateFormat: \"" + controlDateFormat + "\" });");
            this.Controls.Add(datepicker);
        }
    }
}
