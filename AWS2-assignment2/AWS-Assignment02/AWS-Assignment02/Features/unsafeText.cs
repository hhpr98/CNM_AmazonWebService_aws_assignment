using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace AWS_Assignment02.Features
{
    public partial class unsafeText : Form
    {
        public unsafeText()
        {
            InitializeComponent();
        }

        private void unsafeText_Load(object sender, EventArgs e)
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
            //if (bmp.Height>bmp.Width)
            //{
            //    // 200, 100 (khổ ngang) => 50,100 (khổ dọc)
            //    var sztmp = pb.Size;
            //    pb.Size = new Size(sztmp.Height, 370);
            //}    
            pb.Image = bmp;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            var source = ToByteStream(textURL.Text);

            var client = new AmazonRekognitionClient();
            var request = new DetectModerationLabelsRequest
            {
                Image = source,
                MinConfidence = 50F
            };

            var response = client.DetectModerationLabels(request);
            if (response.ModerationLabels.Count == 0)
            {
                txtRes.Text = "No moderation label be found!";
                return;
            }

            var tmp = new StringBuilder();
            tmp.Append("There are some moderation label has found below: \r\n");
            foreach (var label in response.ModerationLabels)
            {
                tmp.Append(label.Name);
                tmp.Append("\t(" + label.Confidence.ToString() + "%)\r\n");
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
