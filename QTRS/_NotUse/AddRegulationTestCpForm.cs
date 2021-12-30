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
    public partial class AddRegulationTestCpForm : Form
    {
        public AddRegulationTestCpForm()
        {
            InitializeComponent();
        }
        private void AddRegulationTestCpForm_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitControls()
        {

            InitDataGrid();
        }

        private void InitDataGrid()
        {
            qualityDetailDataGridView.RowHeadersVisible = false;
            manufactureDetailDataGridView.RowHeadersVisible = false;
        }

       
    }
}
