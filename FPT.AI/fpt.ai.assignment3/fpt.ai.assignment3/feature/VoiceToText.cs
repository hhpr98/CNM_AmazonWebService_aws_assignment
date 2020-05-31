using System;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using fpt.ai.assignment3.Classss;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace fpt.ai.assignment3.feature
{
    public partial class VoiceToText : Form
    {
        public VoiceToText()
        {
            InitializeComponent();
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        private string fileName = "";
        private System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
        private int t = 0;
        private DateTime start;

        private void VoiceToText_Load(object sender, EventArgs e)
        {
            lblRecording.Visible = false;
            txtRes.ScrollBars = ScrollBars.Both;
            txtRes.SelectionStart = txtRes.Text.Length;
            txtRes.ScrollToCaret();
            txtURL.ReadOnly = true;
            txtRes.ReadOnly = true;
            tmr.Interval = 1000; // 1s
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            lblRecording.Visible = true;
            this.Text = "Đang ghi âm";
            fileName = $"C:\\{Guid.NewGuid()}.mp3";
            tmr.Tick += Tmr_Tick;
            t = 0;
            start = DateTime.Now;
            tmr.Enabled = true;
            tmr.Start();
            // start to record
            record("open new Type waveaudio Alias recsound", "", 0, 0);
            record("record recsound", "", 0, 0);
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            if (t++ >= 14 || (DateTime.Now - start).TotalSeconds > 14) // 14 or 15s, FPT.AI free account limit for 15s
            {
                lblRecording.Visible = false;
                this.Text = "Chuyển giọng nói thành văn bản";
                tmr.Stop();
                tmr.Enabled = false;
                record("save recsound " + fileName, "", 0, 0);
                record("close recsound", "", 0, 0);
                //MessageBox.Show("End " + fileName);
                this.txtURL.Text = fileName;
            }
            // do your stuff
            if (t < 10)
            {
                lbl3.Text = "0" + t.ToString();
            }
            else
            {
                lbl3.Text = t.ToString();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\nguyenhuuhoa\Desktop",
                Title = "Tìm âm thanh",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = ".mp3",
                Filter = "MP3 files (*.mp3)|*.mp3",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                this.txtURL.Text = openFile.FileName;
                lbl3.Text = "00";
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (txtURL.Text == "")
            {
                MessageBox.Show("Vui lòng chọn file mp3!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Use this OK, but event playing and media ended not found
            //this.Text = "Đang phát";
            //SoundPlayer simpleSound = new SoundPlayer(txtURL.Text);
            //simpleSound.Play();
            //this.Text = "Chuyển giọng nói thành văn bản";

            try
            {
                var player = new MediaPlayer();
                player.Open(new Uri(txtURL.Text, UriKind.Absolute));
                player.Play();
                player.MediaOpened += Player_MediaOpened;
                player.MediaEnded += Player_MediaEnded;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi khi phát media", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            this.Text = "Đang phát";
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            this.Text = "Chuyển giọng nói thành văn bản";
        }

        private void btnTrans_Click(object sender, EventArgs e)
        {
            if (txtURL.Text == "")
            {
                MessageBox.Show("Vui lòng chọn file mp3!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lbl3.Text = "00";
            txtRes.Text = "Đang chuyển.......";

            //var apikey = "V6g9JmmqFy2KAff2ZxeId29RNYy12KUb";
            var apikey = "ZJZY2Eabdg6rGH1aUJ009i3qCLqUPVYI";

            var file = txtURL.Text;
            var payload = File.ReadAllBytes(file);

            try
            {
                string json = Task.Run(async () =>
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("api-key", apikey);
                    var response = await client.PostAsync("https://api.fpt.ai/hmi/asr/general", new ByteArrayContent(payload));
                    return await response.Content.ReadAsStringAsync();

                }).GetAwaiter().GetResult();

                Thread.Sleep(2000); // sleep about 2s to wait for FPT server response
                var data = JsonConvert.DeserializeObject<SpeechToTextClass>(json);
                //MessageBox.Show(data.hypotheses[0].utterance);
                txtRes.Text = data.hypotheses[0].utterance;
            }
            catch (Exception ex)
            {
                txtRes.Text = "Lỗi";
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
