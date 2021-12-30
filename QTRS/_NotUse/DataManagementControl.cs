using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS
{
    public partial class DataManagementControl : UserControl
    {
        private string _currentMenuName = "";
        private ImportInspectionRmdControl _importInspectionRmdControl = null;
        private RegulationTestRmdControl _regulationTestRmdControl = null;
        private RegulationTestCpControl _regulationTestCpControl = null;

        public DataManagementControl()
        {
            InitializeComponent();
        }

        private void DataManagementControl_Load(object sender, EventArgs e)
        {
            // 초기 메뉴 선택
            importInspectionRmdMenuButton.Checked = true;
            //ResetMenuButtonStatus(mainMenuButton.Name);
            ResetContent(importInspectionRmdMenuButton.Name);
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            string menuName = (sender as RadioButton).Name;
            //ResetMenuButtonStatus(menuName);
            ResetContent(menuName);

            _currentMenuName = menuName;
        }

        private void ResetContent(string menuName)
        {
            if (menuName == _currentMenuName)
                return;

            contentPanel.Controls.Clear();

            if (importInspectionRmdMenuButton.Checked == true)
            {
                if (_importInspectionRmdControl == null)
                    _importInspectionRmdControl = new ImportInspectionRmdControl();

                _importInspectionRmdControl.GetData();
                contentPanel.Controls.Add(_importInspectionRmdControl);
                //_importInspectionRmdControl.ResizeControls();
                //_importInspectionRmdControl.ResetAuth();
            }
            else if (regulationTestRmdMenuButton.Checked == true)
            {
                if (_regulationTestRmdControl == null)
                    _regulationTestRmdControl = new RegulationTestRmdControl();

                _importInspectionRmdControl.GetData();
                contentPanel.Controls.Add(_regulationTestRmdControl);
                //_regulationTestRmdControl.ResizeControls();
                //_regulationTestRmdControl.ResetAuth();
            }
            else if (regulationTestCpMenuButton.Checked == true)
            {
                if (_regulationTestCpControl == null)
                    _regulationTestCpControl = new RegulationTestCpControl();

                _regulationTestCpControl.GetData();
                contentPanel.Controls.Add(_regulationTestCpControl);
                //_regulationTestCpControl.ResizeControls();
                //_regulationTestCpControl.ResetAuth();
            }
        }

        public void GetData()
        {

        }
    }
}
