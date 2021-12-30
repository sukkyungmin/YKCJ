using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.ProductTest
{
    public partial class DateTimePickerForm : Form
    {
        private AddProductTestForm _parent = null; 

        public DateTimePickerForm(AddProductTestForm parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void DateTimePickerForm_Load(object sender, EventArgs e)
        {
            monthCalendar.SelectionRange.Start = DateTime.Now; 
        }

      

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime firstDate = new DateTime(monthCalendar.SelectionRange.Start.Year, 1, 1);
            TimeSpan diffDate = monthCalendar.SelectionRange.Start - firstDate;
            int diffDay = diffDate.Days + 1;

            /* 날짜 조합 사용안하므로 막고 밑의 호기만 가져오게 작업
            string manufactureSerialNumber = monthCalendar.SelectionRange.Start.ToString("yyyy").Substring(3, 1) + string.Format("{0:D3}", diffDay);
            manufactureSerialNumber = manufactureSerialNumber += ((_parent.machineComboBox.SelectedItem as ComboBoxItem).Text.Trim().Length > 4) ? "CA10" : (_parent.machineComboBox.SelectedItem as ComboBoxItem).Text.Trim().Replace("#", "0"); 
            _parent.manufactureSerialNumberTextBox.Text = manufactureSerialNumber;
            */

            _parent.manufactureSerialNumberTextBox.Text = ((_parent.machineComboBox.SelectedItem as ComboBoxItem).Text.Trim().Length > 4) ? "CA10" : (_parent.machineComboBox.SelectedItem as ComboBoxItem).Text.Trim().Replace("#", "0");

            //_parent.manufactureDateTextBox.Text = monthCalendar.SelectionRange.Start.AddDays((365 * 3) - 1).ToString("yyyy-MM-dd");
            //_parent.manufactureDateTextBox.Text = monthCalendar.SelectionRange.Start.AddDays(365 * 3).ToString("yyyy-MM-dd");

            DateTime FrDate = DateTime.ParseExact(monthCalendar.SelectionRange.Start.ToString("yyyyMMdd"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            DateTime EnDate = FrDate.AddYears(3).AddDays(-1);

            TimeSpan DateDasy = EnDate - FrDate;

            int AddDays = DateDasy.Days;

            _parent.manufactureDateTextBox.Text = monthCalendar.SelectionRange.Start.AddDays(AddDays).ToString("yyyy-MM-dd");

            this.Close();
        }
    }
}
