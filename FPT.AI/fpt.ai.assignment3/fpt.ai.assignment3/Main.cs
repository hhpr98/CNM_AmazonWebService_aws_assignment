using fpt.ai.assignment3.feature;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace fpt.ai.assignment3
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            pcbAI.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\avt.png");
            pcbAI.BackgroundImageLayout = ImageLayout.Stretch;

            ToolTip tt = new ToolTip();

            btn1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\ic_speechtotext.png");
            btn1.BackgroundImageLayout = ImageLayout.Stretch;
            tt.SetToolTip(btn1, "Viết chương trình đọc văn bản thành tiếng. Cho phép lựa chọn các giọng đọc khác nhau mà FPT.AI cung cấp");

            btn2.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\ic_speech.png");
            btn2.BackgroundImageLayout = ImageLayout.Stretch;
            tt.SetToolTip(btn2, "Viết chương trình chuyển âm thanh thu được từ micro thành giọng nói, ngữ cảnh áp dụng là đọc chính tả.");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            TextToVoice frm = new TextToVoice();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            VoiceToText frm = new VoiceToText();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

    }
}
