using fpt.ai.assignment3.Classss;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace fpt.ai.assignment3.feature
{
    public partial class TextToVoice : Form
    {
        public TextToVoice()
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
            txtEn.Text = "Xin chào, đây là chương trình đọc văn bản Tiếng Việt.";
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
                "Lan Nhi (Nữ miền Nam)", "Ban Mai (Nữ miền Bắc)", "Thu Minh (Nữ miền Bắc)",
                "Gia Huy (Nam miền Trung)", "Mỹ An (Nữ miền Trung)", "Lê Minh (Nam miền Bắc)",
                //"Cao Chung (Nam miền Bắc)", "Thu Dung (Nữ miền Bắc)", "Hà Tiểu Mai (Nữ miền Nam)"
            };
            cbVoice.DataSource = voiceArr;
            cbVoice.SelectedItem = voiceArr[0];
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
            //var apikey = "V6g9JmmqFy2KAff2ZxeId29RNYy12KUb";
            var apikey = "ZJZY2Eabdg6rGH1aUJ009i3qCLqUPVYI";

            this.Text = "Đang tải";

            string payload = txtEn.Text;

            var data = new TextToSpeechClass();
            try
            {
                string json = Task.Run(async () =>
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("api-key", apikey);
                    client.DefaultRequestHeaders.Add("voice", getVoiceId());
                    var response = await client.PostAsync("https://api.fpt.ai/hmi/tts/v5", new StringContent(payload));
                    return await response.Content.ReadAsStringAsync();

                }).GetAwaiter().GetResult();

                data = JsonConvert.DeserializeObject<TextToSpeechClass>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = $"{currentFolder}{Guid.NewGuid()}.mp3";

            Thread.Sleep(4000); // ngắt tầm 2s, download liên tục sẽ khiến server trả về 404 Not Found
            using (var client = new WebClient())
            {
                client.DownloadFile(data.async,fileName);
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
                MessageBox.Show(ex.Message, "Lỗi khi phát media", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private string getVoiceId()
        {
            switch (cbVoice.SelectedIndex)
            {
                case 0: return "lannhi";
                case 1: return "banmai";
                case 2: return "thuminh";
                case 3: return "giahuy";
                case 4: return "myan";
                case 5: return "leminh";
            }
            return "lannhi";
        }
    }
}
