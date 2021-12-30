using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.ComponentTest
{
    public partial class CompatibilityForm : Form
    {
        RunComponentTestForm _parent = null; 
        public CompatibilityForm(RunComponentTestForm parent)
        {
            InitializeComponent();
            _parent = parent; 
        }

        private void listBox_Click(object sender, EventArgs e)
        {
            if(listBox.SelectedIndex != -1)
            {
                _parent.componentTestDataGridView.SelectedRows[0].Cells[(int)RunComponentTestForm.eComponentTestResultList.compatibilityOx].Value = listBox.SelectedItem.ToString();
                Close();
            }
        }
    }
}
