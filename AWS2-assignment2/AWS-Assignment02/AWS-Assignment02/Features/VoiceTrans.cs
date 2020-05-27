using Amazon.Polly;
using Amazon.Polly.Model;
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
using System.Windows.Media;

namespace AWS_Assignment02.Features
{
    public partial class VoiceTrans : Form
    {
        public VoiceTrans()
        {
            InitializeComponent();
        }

        private TextBox txtEn = new TextBox();
        private ComboBox cbVoice = new ComboBox();
        private void VoiceTrans_Load(object sender, EventArgs e)
        {
            this.Size = new Size(620, 500);

            txtEn.Location = new System.Drawing.Point(50, 50);
            txtEn.Size = new Size(500, 300);
            txtEn.Multiline = true;
            txtEn.ScrollBars = ScrollBars.Both;
            txtEn.SelectionStart = txtEn.Text.Length;
            txtEn.ScrollToCaret();
            txtEn.BackColor = System.Drawing.Color.LightCyan;
            txtEn.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            txtEn.Text = "Hello, I am Hòa. I'm from Vietnamese.";
            this.Controls.Add(txtEn);

            Label lbl = new Label();
            lbl.Text = "Chọn giọng đọc";
            lbl.Location = new System.Drawing.Point(50, 380);
            lbl.Size = new Size(120, 30);
            lbl.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            //lbl.BackColor = Color.Blue;
            lbl.ForeColor = System.Drawing.Color.Red;
            this.Controls.Add(lbl);

            cbVoice.Location = new System.Drawing.Point(200, 375);
            cbVoice.Size = new Size(200, 40);
            cbVoice.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            var voiceArr = new string[] { 
                "Salli, Female", "Joanna, Female", "Ivy, Female", "Kendra, Female",
                "Kimberly, Female", "Matthew, Male", "Justin, Male", "Joey, Male"
            };
            cbVoice.DataSource = voiceArr;
            cbVoice.SelectedIndex = cbVoice.Items.IndexOf("Salli, Female");
            this.Controls.Add(cbVoice);

            Button btnTrans = new Button();
            btnTrans.Location = new System.Drawing.Point(450, 370);
            btnTrans.Text = "Nghe thử";
            btnTrans.Size = new Size(100, 35);
            btnTrans.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btnTrans.BackColor = System.Drawing.Color.Blue;
            btnTrans.ForeColor = System.Drawing.Color.White;
            btnTrans.Click += BtnTrans_Click;
            this.Controls.Add(btnTrans);
        }

       

        private void BtnTrans_Click(object sender, EventArgs e)
        {
            this.Text = "Đang tải";
            var client = new AmazonPollyClient();

            var request = new SynthesizeSpeechRequest
            {
                Text = txtEn.Text,
                OutputFormat = OutputFormat.Mp3,
                VoiceId = getVoiceId(),
            };

            var response = client.SynthesizeSpeech(request);

            var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = $"{currentFolder}{Guid.NewGuid()}.mp3";

            using (var fileStream = File.Create(fileName))
            {
                response.AudioStream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }    

            try
            {
                var player = new MediaPlayer();
                player.Open(new Uri(fileName, UriKind.Absolute));
                player.Play();
                player.MediaOpened += Player_MediaOpened;
                player.MediaEnded += Player_MediaEnded;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            this.Text = "Đang phát";
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            this.Text = "Chuyển văn bản thành giọng nói";
        }

        private VoiceId getVoiceId()
        {
            switch (cbVoice.SelectedIndex)
            {
                case 0: return VoiceId.Salli;
                case 1: return VoiceId.Joanna;
                case 2: return VoiceId.Ivy;
                case 3: return VoiceId.Kendra;
                case 4: return VoiceId.Kimberly;
                case 5: return VoiceId.Matthew;
                case 6: return VoiceId.Justin;
                case 7: return VoiceId.Joey;
            }
            return VoiceId.Joanna;
        }
    }
}
