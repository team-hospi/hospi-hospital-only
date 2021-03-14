
namespace hospi_hospital_only
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.button6 = new System.Windows.Forms.Button();
            this.textBoxHospitalID = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.labelPW = new System.Windows.Forms.Label();
            this.textBoxPW = new System.Windows.Forms.TextBox();
            this.labelID = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.Location = new System.Drawing.Point(133, 92);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(110, 32);
            this.button6.TabIndex = 4;
            this.button6.Text = "접속";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // textBoxHospitalID
            // 
            this.textBoxHospitalID.Location = new System.Drawing.Point(93, 19);
            this.textBoxHospitalID.Name = "textBoxHospitalID";
            this.textBoxHospitalID.Size = new System.Drawing.Size(150, 23);
            this.textBoxHospitalID.TabIndex = 1;
            this.textBoxHospitalID.Text = "1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.labelPW);
            this.groupBox1.Controls.Add(this.textBoxPW);
            this.groupBox1.Controls.Add(this.textBoxHospitalID);
            this.groupBox1.Controls.Add(this.labelID);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 137);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button5.Location = new System.Drawing.Point(15, 92);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(110, 32);
            this.button5.TabIndex = 3;
            this.button5.Text = "진료실";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // labelPW
            // 
            this.labelPW.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelPW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPW.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelPW.Location = new System.Drawing.Point(16, 53);
            this.labelPW.Name = "labelPW";
            this.labelPW.Size = new System.Drawing.Size(71, 23);
            this.labelPW.TabIndex = 6;
            this.labelPW.Text = "PW";
            this.labelPW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPW
            // 
            this.textBoxPW.Location = new System.Drawing.Point(93, 53);
            this.textBoxPW.Name = "textBoxPW";
            this.textBoxPW.PasswordChar = '●';
            this.textBoxPW.Size = new System.Drawing.Size(150, 23);
            this.textBoxPW.TabIndex = 2;
            this.textBoxPW.Text = "1234";
            // 
            // labelID
            // 
            this.labelID.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelID.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelID.Location = new System.Drawing.Point(16, 19);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(71, 23);
            this.labelID.TabIndex = 4;
            this.labelID.Text = "ID";
            this.labelID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(321, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 67);
            this.button1.TabIndex = 35;
            this.button1.Text = "병원ID,PW 변경용\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 168);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBoxHospitalID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelPW;
        private System.Windows.Forms.TextBox textBoxPW;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
    }
}

