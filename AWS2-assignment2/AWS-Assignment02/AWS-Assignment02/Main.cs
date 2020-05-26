using AWS_Assignment02.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AWS_Assignment02
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();

            btn1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\ic_banned.png");
            btn1.BackgroundImageLayout = ImageLayout.Stretch;
            tt.SetToolTip(btn1, "Phân tích ảnh để xác định ảnh có các yếu tố nhạy cảm với trẻ em hay không");

            btn2.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\ic_ocr.png");
            btn2.BackgroundImageLayout = ImageLayout.Stretch;
            tt.SetToolTip(btn2, "Xác định các text có trong ảnh");

            btn3.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\ic_translate.png");
            btn3.BackgroundImageLayout = ImageLayout.Stretch;
            tt.SetToolTip(btn3, "Dịch thuật từ tiếng Anh sang tiếng Việt");

            btn4.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\ic_voice.png");
            btn4.BackgroundImageLayout = ImageLayout.Stretch;
            tt.SetToolTip(btn4, "Chuyển văn bản thành giọng nói");
        }

        private void btn1_Click(object sender, EventArgs e)
        {

        }

        private void btn2_Click(object sender, EventArgs e)
        {

        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Translate frm = new Translate();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            VoiceTrans frm = new VoiceTrans();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
