using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS
{
    public partial class MainForm : Form
    {
        public bool _isClosedLoadingBar = false;


        private string _currentMenuName = "";
        private MainControl _mainControl = null; 
        //private DataManagementControl _dataManagementControl = null; 
        private ImportInspection.ImportInspectionControl _importInspectionControl = null; 
        private ComponentTest.ComponentTestControl _componentTestControl = null; 
        private ProductTest.ProductTestControl _productTestControl = null;
        private Report.ReportControl _reportControl = null; 
        private PackingTest.PackingTestControl _packingTestControl = null; 
        private Analysis.AnalysisControl _analysisControl = null;
        private Notice.NoticeControl _noticeControl = null; 
        private Setting.SettingControl _settingControl = null;

        private Point _mousePoint; 

        public MainForm()
        {
            InitializeComponent();
            label1.Text = "Quality Test Report System";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 초기 메뉴 선택
            mainMenuButton.Checked = true;
            //ResetMenuButtonStatus(mainMenuButton.Name);
            ResetContent(mainMenuButton.Name);

            // 사용자 정보 설정
            userNameLabel.Text = Global.loginInfo.name;

            // 현재시간 출력
            currentTimer.Start();

            if (Global.loginInfo.authorityId != 102)
                settingMenuButton.Visible = false;

            GetConfig();
        }

        private void userNameLabel_Click(object sender, EventArgs e)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            contextMenuStrip.Items.Add("Logout");

            contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(showUserContextMenu);

            contextMenuStrip.Show(userInfoPanel, new Point(userNameLabel.Left, userNameLabel.Bottom));
        }

        private void showUserContextMenu(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Logout":
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            string menuName = (sender as RadioButton).Name;
            //ResetMenuButtonStatus(menuName);
            ResetContent(menuName);

            _currentMenuName = menuName;

            ResizeControls();
        }

        public void ResetContent(string menuName)
        {
            if (menuName == _currentMenuName)
                return;

            contentPanel.Controls.Clear();

            if (_mainControl != null && mainMenuButton.Checked == false)
                _mainControl.StopHomeTimer(); 

            if (mainMenuButton.Checked == true)
            {
                if (_mainControl == null)
                    _mainControl = new MainControl(this);

                _mainControl.GetData();
                contentPanel.Controls.Add(_mainControl);
                _mainControl.StartHomeTimer(); 
                //_mainControl.ResizeControls();
                //_mainControl.ResetAuth();
            }
            else if (importInspectionMenuButton.Checked == true)
            {
                if (_importInspectionControl == null)
                    _importInspectionControl = new ImportInspection.ImportInspectionControl();

                _importInspectionControl.GetData();
                contentPanel.Controls.Add(_importInspectionControl);
                //_dataManagementControl.ResizeControls();
                //_dataManagementControl.ResetAuth();
            }
            else if (componentTestMenuButton.Checked == true)
            {
                if (_componentTestControl == null)
                    _componentTestControl = new ComponentTest.ComponentTestControl();

                _componentTestControl.GetData();
                contentPanel.Controls.Add(_componentTestControl);
                //_componentTestControl.ResizeControls();
                //_componentTestControl.ResetAuth();
            }
            else if (productTestMenuButton.Checked == true)
            {
                if (_productTestControl == null)
                    _productTestControl = new ProductTest.ProductTestControl();

                _productTestControl.GetData();
                contentPanel.Controls.Add(_productTestControl);
                //_productTestControl.ResizeControls();
                //_productTestControl.ResetAuth();
            }
            else if (reportMenuButton.Checked == true)
            {
                if (_reportControl == null)
                    _reportControl = new Report.ReportControl();

                _reportControl.GetData();
                contentPanel.Controls.Add(_reportControl);
            }
            else if (packingTestMenuButton.Checked == true)
            {
                if (_packingTestControl == null)
                    _packingTestControl = new PackingTest.PackingTestControl();

                _packingTestControl.GetData();
                contentPanel.Controls.Add(_packingTestControl);
                //_productTestControl.ResizeControls();
                //_productTestControl.ResetAuth();
            }

            else if (analysisMenuButton.Checked == true)
            {
                if (_analysisControl == null)
                    _analysisControl = new Analysis.AnalysisControl();

                //_analysisControl.GetData();
                contentPanel.Controls.Add(_analysisControl);
                //_analysisControl.ResizeControls();
                //_analysisControl.ResetAuth();
            }
            else if (noticeMenuButton.Checked == true)
            {
                if (_noticeControl == null)
                    _noticeControl = new Notice.NoticeControl();

                _noticeControl.GetData();
                contentPanel.Controls.Add(_noticeControl);
                //_noticeControl.ResizeControls();
                //_noticeControl.ResetAuth();
            }
            else if (settingMenuButton.Checked == true)
            {
                if (_settingControl == null)
                    _settingControl = new Setting.SettingControl();

                //_settingControl.GetData();
                contentPanel.Controls.Add(_settingControl);
                //_settingControl.ResizeControls();
                //_settingControl.ResetAuth();
            }
        }

        private void currentTimer_Tick(object sender, EventArgs e)
        {
            currentTimeLabel.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
        }

        private void helpPictureBox_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Quality Test Report System" + "\r\n" + "QTRS Ver 1.2.0" + "\r\n" + "한길에프에이(주)" + "\r\n" +"031 -695-8295~6");
            //string openpdffile = new System.IO.DirectoryInfo(Application.StartupPath+ "/helppdf/2018_J023_YKCJ_YKCJ_QTRS-User Manual(00-제출191101).pdf").ToString();
            //System.Diagnostics.Process.Start(openpdffile);
            Common.HelpForm form = new Common.HelpForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        public void SetCurrentMenuName(string currentMenuName)
        {
            _currentMenuName = currentMenuName; 
        }
        public void SetNoticeMenuButtonStatus(bool status)
        {
            this.noticeMenuButton.Checked = status; 
        }

        public void AddNoticeButton()
        {
            this._noticeControl.AddNoticeButton();
        }

        private void titlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mousePoint.X = e.X;
                _mousePoint.Y = e.Y;
            }
        }

        private void titlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(
                    this.Location.X + (e.X - _mousePoint.X), 
                    this.Location.Y + (e.Y - _mousePoint.Y));
            }
        }

        private void minimizeScreenButton_Click(object sender, EventArgs e)
        {
            if(this.WindowState != FormWindowState.Minimized)
                this.WindowState = FormWindowState.Minimized;
        }

        private void fullScreenButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                fullScreenButton.BackgroundImage = Properties.Resources.full_screen_18;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
                fullScreenButton.BackgroundImage = Properties.Resources.prev_screen_18;
            }

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contentPanel_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        private void ResizeControls()
        {
            if (_mainControl != null)
                _mainControl.ResizeControls();
            if (_importInspectionControl != null)
                _importInspectionControl.ResizeControls();
            if (_componentTestControl != null)
                _componentTestControl.ResizeControls();
            if (_productTestControl != null)
                _productTestControl.ResizeControls();
            if (_reportControl != null)
                _reportControl.ResizeControls();
            if (_noticeControl != null)
                _noticeControl.ResizeControls();
            if (_settingControl != null)
                _settingControl.ResizeControls();
        }

        private bool GetConfig()
        {
            try
            {
                string currentPath = Application.StartupPath;
                string iniFilePath = currentPath + "\\Config\\Config.ini";

                Global.comNumber = Utils.GetIniValue("DEFAULT", "COM_NUMBER", iniFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
    }
}
