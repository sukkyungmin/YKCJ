using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Setting
{
    public partial class SettingControl : UserControl
    {
        private string _currentMenuName = "";
        private ComponentMasterControl _componentMasterControl = null;
        private ProductMasterControl _productMasterControl = null;
        private MemberControl _memberControl = null;


        public SettingControl()
        {
            InitializeComponent();
        }

        private void SettingControl_Load(object sender, EventArgs e)
        {
            InitControls();
            // 초기 메뉴 선택
            componentMgtButton.Checked = true;
            //ResetMenuButtonStatus(componentMgtButton.Name);
            ResetContent(componentMgtButton.Name);
        }

        private void InitControls()
        {
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            string menuName = (sender as RadioButton).Name;
            //ResetMenuButtonStatus(menuName);
            ResetContent(menuName);

            _currentMenuName = menuName;

            ResizeControls();
        }

        private void ResetContent(string menuName)
        {
            if (menuName == _currentMenuName)
                return;


            contentPanel.Controls.Clear();

            if (componentMgtButton.Checked == true)
            {
                if (_componentMasterControl == null)
                    _componentMasterControl = new ComponentMasterControl();

                _componentMasterControl.GetData();
                contentPanel.Controls.Add(_componentMasterControl);
                //_componentMasterControl.ResizeControls();
                //_componentMasterControl.ResetAuth();
            }
            else if (productMgtButton.Checked == true)
            {
                if (_productMasterControl == null)
                    _productMasterControl = new ProductMasterControl();

                _productMasterControl.GetData();
                contentPanel.Controls.Add(_productMasterControl);
                //_completeProductMasterControl.ResizeControls();
                //_completeProductMasterControl.ResetAuth();
            }
            else if (memberMgtButton.Checked == true)
            {
                if (_memberControl == null)
                    _memberControl = new MemberControl();

                _memberControl.GetData();
                contentPanel.Controls.Add(_memberControl);
                //_userControl.ResizeControls();
                //_userControl.ResetAuth();
            }
        }

        public void ResizeControls()
        {
            if (Parent == null)
                return;

            this.Left = 0;
            this.Top = 0;
            this.Width = Parent.Width;
            this.Height = Parent.Height;

            this.contentPanel.Top = componentMgtButton.Bottom + 6;
            this.contentPanel.Width = Parent.Width;
            this.contentPanel.Height = Parent.Height - 109;

            memberMgtButton.Left = contentPanel.Right - (memberMgtButton.Width + 40);
            productMgtButton.Left = memberMgtButton.Left - (6 + productMgtButton.Width);
            componentMgtButton.Left = productMgtButton.Left - (6 + componentMgtButton.Width);


            if (_componentMasterControl != null)
                _componentMasterControl.ResizeControls();

            if (_productMasterControl != null)
                _productMasterControl.ResizeControls();

            if (_memberControl != null)
                _memberControl.ResizeControls();
        }
    }
}
