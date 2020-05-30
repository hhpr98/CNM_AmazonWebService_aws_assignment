namespace AWS_Assignment02.Features
{
    partial class ocr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ocr));
            this.textURL = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnOCR = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.PictureBox();
            this.txtRes = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // textURL
            // 
            this.textURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textURL.Location = new System.Drawing.Point(12, 25);
            this.textURL.Name = "textURL";
            this.textURL.Size = new System.Drawing.Size(571, 26);
            this.textURL.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnBrowse.Location = new System.Drawing.Point(595, 23);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(36, 30);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOCR
            // 
            this.btnOCR.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnOCR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOCR.Location = new System.Drawing.Point(640, 23);
            this.btnOCR.Name = "btnOCR";
            this.btnOCR.Size = new System.Drawing.Size(81, 30);
            this.btnOCR.TabIndex = 2;
            this.btnOCR.Text = "Phân tích";
            this.btnOCR.UseVisualStyleBackColor = false;
            this.btnOCR.Click += new System.EventHandler(this.btnOCR_Click);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(12, 70);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(571, 368);
            this.pb.TabIndex = 3;
            this.pb.TabStop = false;
            // 
            // txtRes
            // 
            this.txtRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRes.Location = new System.Drawing.Point(595, 70);
            this.txtRes.Multiline = true;
            this.txtRes.Name = "txtRes";
            this.txtRes.ReadOnly = true;
            this.txtRes.Size = new System.Drawing.Size(225, 368);
            this.txtRes.TabIndex = 4;
            // 
            // ocr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(832, 450);
            this.Controls.Add(this.txtRes);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.btnOCR);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textURL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ocr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhận diện chữ trong ảnh";
            this.Load += new System.EventHandler(this.ocr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textURL;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnOCR;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.TextBox txtRes;
    }
}