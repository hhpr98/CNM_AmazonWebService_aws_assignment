using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            txtEn.BackColor = Color.LightCyan;
            txtEn.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.Controls.Add(txtEn);

            Label lbl = new Label();
            lbl.Text = "Chọn giọng đọc";
            lbl.Location = new System.Drawing.Point(5, 380);
            lbl.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl.BackColor = Color.Blue;
            lbl.ForeColor = Color.Red;
            this.Controls.Add(lbl);

            Button btnTrans = new Button();
            btnTrans.Location = new System.Drawing.Point(500, 380);
            btnTrans.Text = "Nghe thử";
            btnTrans.Size = new Size(80, 40);
            btnTrans.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btnTrans.BackColor = Color.Blue;
            btnTrans.ForeColor = Color.White;
            btnTrans.Click += BtnTrans_Click;
            this.Controls.Add(btnTrans);

            
        }

        private void BtnTrans_Click(object sender, EventArgs e)
        {
            try
            {
                //System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\nguyenhuuhoa\Desktop\Anh-Oi-O-Lai-Chi-Pu-Dat-G.mp3");
                //player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
