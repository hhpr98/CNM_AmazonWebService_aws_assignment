using System;
using System.Drawing;
using System.Windows.Forms;
using Amazon.Translate;
using Amazon.Translate.Model;

namespace AWS_Assignment02.Features
{
    public partial class Translate : Form
    {
        public Translate()
        {
            InitializeComponent();
        }

        private TextBox txtEn = new TextBox();
        private TextBox txtVi = new TextBox();

        private void Translate_Load(object sender, EventArgs e)
        {
            this.Size = new Size(820, 350);

            txtEn.Location = new System.Drawing.Point(50, 50);
            txtEn.Size = new Size(300, 200);
            txtEn.Multiline = true;
            txtEn.ScrollBars = ScrollBars.Both;
            txtEn.SelectionStart = txtEn.Text.Length;
            txtEn.ScrollToCaret();
            txtEn.BackColor = Color.LightCyan;
            txtEn.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.Controls.Add(txtEn);

            txtVi.Location = new System.Drawing.Point(450, 50);
            txtVi.Size = new Size(300, 200);
            txtVi.Multiline = true;
            txtVi.ScrollBars = ScrollBars.Both;
            txtVi.SelectionStart = txtVi.Text.Length;
            txtVi.ScrollToCaret();
            txtVi.BackColor = Color.LightCyan;
            txtVi.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            txtVi.ReadOnly = true;
            this.Controls.Add(txtVi);

            PictureBox btnTrans = new PictureBox();
            btnTrans.Location = new System.Drawing.Point(360, 120);
            btnTrans.Size = new Size(80, 60);
            btnTrans.BackgroundImage = System.Drawing.Image.FromFile(Application.StartupPath + "\\Resources\\ic_arrow.png");
            btnTrans.BackgroundImageLayout = ImageLayout.Stretch;
            btnTrans.Click += BtnTrans_Click;
            this.Controls.Add(btnTrans);
        }

        private void BtnTrans_Click(object sender, EventArgs e)
        {
            var client = new AmazonTranslateClient();

            var request = new TranslateTextRequest
            {
                Text = txtEn.Text,
                SourceLanguageCode = "en",
                TargetLanguageCode = "vi"
            };
            var result = client.TranslateText(request);

            txtVi.Text = result.TranslatedText;

        }
    }
}
