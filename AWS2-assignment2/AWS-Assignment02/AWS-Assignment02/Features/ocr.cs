using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace AWS_Assignment02.Features
{
    public partial class ocr : Form
    {
        public ocr()
        {
            InitializeComponent();
        }

        private void ocr_Load(object sender, EventArgs e)
        {
            this.textURL.ReadOnly = true;
            txtRes.ScrollBars = ScrollBars.Both;
            txtRes.SelectionStart = txtRes.Text.Length;
            txtRes.ScrollToCaret();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\nguyenhuuhoa\Desktop",
                Title = "Tìm hình ảnh",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = ".jpg",
                Filter = "JPG files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                this.textURL.Text = openFile.FileName;
            }
        }

        private void btnOCR_Click(object sender, EventArgs e)
        {
            if (textURL.Text == "")
            {
                MessageBox.Show("Vui lòng chọn hình ảnh để phân tích!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap bmp = new Bitmap(textURL.Text);
            pb.Image = bmp;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            var source = ToByteStream(textURL.Text);

            var client = new AmazonRekognitionClient();
            var request = new DetectTextRequest
            {
                Image = source
            };

            var response = client.DetectText(request);
            var tmp = new StringBuilder();
            foreach (var item in response.TextDetections)
            {
                //MessageBox.Show(item.DetectedText);
                if (item.Type == "LINE") // LINE or WORD
                {
                    tmp.Append(item.DetectedText);
                    tmp.Append("\r\n");
                }    
            }
            txtRes.Text = tmp.ToString();
            //MessageBox.Show(txtRes.Text);
        }

        private Amazon.Rekognition.Model.Image ToByteStream(string FileName)
        {
            var image = new Amazon.Rekognition.Model.Image();

            using (var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                var data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                image.Bytes = new MemoryStream(data);
            }
            return image;
        }

    }
}
