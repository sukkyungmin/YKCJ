using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace QTRS.ProductTest
{
    
    public partial class SerialPortSetup : Form
    {
        SerialPort _serialPort = null;

        public SerialPortSetup(SerialPort serialPort)
        {
            InitializeComponent();
            _serialPort = serialPort;
        }

        private void SerialPortSetup_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                serialPortComboBox.Items.Add(ports[i]);
            }
            if (serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = serialPortComboBox.Items.Count - 1;
        }

        private void openSerialPortButton_Click(object sender, EventArgs e)
        {
            if (serialPortComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("포트를 선택해 주십시오.");
                return;
            }

            connectionStatusLabel.Text = "연결중..";
            _serialPort.PortName = serialPortComboBox.SelectedItem.ToString();
            _serialPort.BaudRate = 2400;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Parity = Parity.None;

            try
            {
                if (_serialPort.IsOpen == false)
                {
                    _serialPort.Open();
                    connectionStatusLabel.Text = serialPortComboBox.SelectedItem.ToString() + " 포트에 연결됨";
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(serialPortComboBox.SelectedItem.ToString() + " 포트를 열 수 없습니다. 저울과 연결된 포트인지 확인해 주십시오.");
            }
        }

        private void closeSerialPortButton_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen == true)
            {
                try
                {
                    _serialPort.Close();
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(serialPortComboBox.SelectedItem.ToString() + " 포트를 닫을 수 없습니다.");
                }
            }

            this.Close();
        }
    }
}
