using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AWS_Assignment01
{
    public partial class ImageForm : Form
    {
        private String imageUrl = "";
        private Bitmap analyzeBitmap = null;
        private int bmpWidth = 0;
        private int bmpHeight = 0;

        private List<PositionClass> boundingEnum = new List<PositionClass>();

        public ImageForm()
        {
            InitializeComponent();
        }

        public ImageForm(String imageUrl)
        {
            InitializeComponent();
            this.imageUrl = imageUrl;
            this.Text = imageUrl;
            analyzeBitmap = new Bitmap(imageUrl);
            //MessageBox.Show("CONST",analyzeBitmap.Width.ToString());
            bmpHeight = analyzeBitmap.Height;
            bmpWidth = analyzeBitmap.Width;
            this.Size = new Size(bmpWidth + 15,bmpHeight + 40);
            this.BackgroundImage = analyzeBitmap;
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            // Load images

            var image = new Amazon.Rekognition.Model.Image();
            try
            {
                using (var fs = new FileStream(imageUrl, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    image.Bytes = new MemoryStream(data);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể mở tệp " + imageUrl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var rekognitionClient = new AmazonRekognitionClient();

            var detectlabelsRequest = new DetectLabelsRequest()
            {
                Image = image,
                MaxLabels = 10,
                MinConfidence = 77F
            };

            try
            {
                DetectLabelsResponse detectLabelsResponse = rekognitionClient.DetectLabels(detectlabelsRequest);
                //MessageBox.Show("Detected labels for " + imageUrl);
                foreach (var label in detectLabelsResponse.Labels)
                {
                    //MessageBox.Show(label.Name + " : " + label.Confidence);
                    foreach (var item in label.Instances)
                    {
                        //MessageBox.Show("Left : " + item.BoundingBox.Left);
                        boundingEnum.Add(new PositionClass(
                            item.BoundingBox.Top * bmpHeight,
                            item.BoundingBox.Left * bmpWidth,
                            item.BoundingBox.Width * bmpWidth,
                            item.BoundingBox.Height * bmpHeight)
                            );
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể phân tích hình ảnh","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }


        }

        private void Image_Paint(object sender, PaintEventArgs e)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Red, 4);

            foreach (var itemBounding in boundingEnum)
            {
                // Create rectangle.
                Rectangle rect = new Rectangle((int)itemBounding.Left, (int)itemBounding.Top, (int)itemBounding.Width, (int)itemBounding.Height);

                // Draw rectangle to screen.
                e.Graphics.DrawRectangle(blackPen, rect);
            }    
        }

    }
}
