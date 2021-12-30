using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using System.Threading;
using System.IO;

namespace QTRS
{
    public partial class CaptureForm : Form
    {
        bool _isCapturing = false;
        bool _isThreading = false;
        DataGridView _imageDataGridView = null; 

        public CaptureForm(DataGridView imageDataGridView)
        {
            InitializeComponent();
            _imageDataGridView = imageDataGridView; 
        }

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            _isCapturing = true;
            StartCameraThread();
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            if (_isCapturing == true)
            {
                _isCapturing = false;
                captureButton.Text = "다시 시작";
                //addFileButton.Enabled = true;
                cancelButton.Enabled = true; 
            }
            else
            {
                _isCapturing = true; 
                captureButton.Text = "캡쳐";
                //addFileButton.Enabled = false;
                cancelButton.Enabled = false;
                StartCameraThread(); 
            }

            Thread.Sleep(100);
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
             string filePath = string.Format("{0}{1}{2}{3}",
                  System.Windows.Forms.Application.StartupPath,
                  Path.DirectorySeparatorChar,
                  "CAPTURE",
                  Path.DirectorySeparatorChar
                  );

            if (Directory.Exists(filePath) == false)
                Directory.CreateDirectory(filePath);

            string uniqueFileName= Utils.GetUniquFileNameByIndex(filePath, "QTRS-CAPTURE-IMAGE-" + DateTime.Now.ToString("yyyy-MM-dd") + ".jpg");

            captureBox.Image.Save(uniqueFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            AddImageFromCapture(uniqueFileName);
           // MessageBox.Show("이미지를 추가했습니다."); 
        }


        private void StartCameraThread()
        {
            if (_isThreading == true)
                return; 

            Thread cameraThread = new Thread(new ThreadStart(CaptureCameraCallback));
            cameraThread.IsBackground = true;
            cameraThread.Start();
        }
        private void CaptureCameraCallback()
        {
            _isThreading = true; 

            VideoCapture capture = new VideoCapture(0);
            //using (Window window = new Window("Camera"))
            using (Mat image = new Mat()) // Frame image buffer
            {
                // When the movie playback reaches end, Mat.data becomes NULL.
                while (true)
                {
                    if (_isCapturing == false)
                        break;

                    try
                    {
                        capture.Read(image); // same as cvQueryFrame
                        if (image.Empty()) break;
                        //window.ShowImage(image);
                        captureBox.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
                        Cv2.WaitKey(30);
                    }
                    catch(System.AccessViolationException ex)
                    {
                        Console.WriteLine(ex.Message); 
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message); 
                    }
                }
            }
            capture.Release();
            capture.Dispose(); 
            _isThreading = false; 
        }

        private void AddImageFromCapture(string filePath)
        {
            Image fileImage = Image.FromFile(filePath);
            _imageDataGridView.Rows.Add(_imageDataGridView.Rows.Count + 1, false, fileImage,
                filePath.Substring(filePath.LastIndexOf(Path.DirectorySeparatorChar)+1), "", filePath, "");
        }

        private void CapturecancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
