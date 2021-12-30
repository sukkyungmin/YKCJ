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
    public partial class CompatibilityForm : Form
    {
        RunQualityTestForm _parent = null; 
        public CompatibilityForm(RunQualityTestForm parent)
        {
            InitializeComponent();
            _parent = parent; 
        }

        private void listBox_Click(object sender, EventArgs e)
        {
            if(listBox.SelectedIndex != -1)
            {
                _parent.productQtDataGridView.SelectedRows[0].Cells[(int)RunQualityTestForm.eProductQtTestResultList.compatibilityOx].Value = listBox.SelectedItem.ToString();
                Close();
            }
        }
    }
}
