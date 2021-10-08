namespace hospi_hospital_only
{
    partial class FindRadiography
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindRadiography));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.labelPatientName = new System.Windows.Forms.Label();
            this.textBoxPatientName = new System.Windows.Forms.TextBox();
            this.labelChart = new System.Windows.Forms.Label();
            this.textBoxChart = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelFileName = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(12, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(672, 499);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "의료 영상";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(18, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(646, 463);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.labelPatientName);
            this.groupBox8.Controls.Add(this.textBoxPatientName);
            this.groupBox8.Controls.Add(this.labelChart);
            this.groupBox8.Controls.Add(this.textBoxChart);
            this.groupBox8.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox8.Location = new System.Drawing.Point(12, 12);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(243, 103);
            this.groupBox8.TabIndex = 53;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "환자 정보";
            // 
            // labelPatientName
            // 
            this.labelPatientName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelPatientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPatientName.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelPatientName.Location = new System.Drawing.Point(18, 59);
            this.labelPatientName.Name = "labelPatientName";
            this.labelPatientName.Size = new System.Drawing.Size(79, 23);
            this.labelPatientName.TabIndex = 50;
            this.labelPatientName.Text = "수진자명";
            this.labelPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPatientName
            // 
            this.textBoxPatientName.Location = new System.Drawing.Point(103, 59);
            this.textBoxPatientName.Name = "textBoxPatientName";
            this.textBoxPatientName.ReadOnly = true;
            this.textBoxPatientName.Size = new System.Drawing.Size(120, 23);
            this.textBoxPatientName.TabIndex = 49;
            this.textBoxPatientName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelChart
            // 
            this.labelChart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelChart.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelChart.Location = new System.Drawing.Point(18, 29);
            this.labelChart.Name = "labelChart";
            this.labelChart.Size = new System.Drawing.Size(79, 23);
            this.labelChart.TabIndex = 48;
            this.labelChart.Text = "차트번호";
            this.labelChart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxChart
            // 
            this.textBoxChart.Location = new System.Drawing.Point(103, 29);
            this.textBoxChart.Name = "textBoxChart";
            this.textBoxChart.ReadOnly = true;
            this.textBoxChart.Size = new System.Drawing.Size(120, 23);
            this.textBoxChart.TabIndex = 47;
            this.textBoxChart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSave);
            this.groupBox2.Controls.Add(this.buttonCancel);
            this.groupBox2.Controls.Add(this.labelFileName);
            this.groupBox2.Controls.Add(this.textBoxFileName);
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(261, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(423, 103);
            this.groupBox2.TabIndex = 54;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "업로드";
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonSave.Location = new System.Drawing.Point(304, 26);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(107, 29);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "저 장";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonCancel.Location = new System.Drawing.Point(304, 61);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(107, 28);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "취 소";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelFileName
            // 
            this.labelFileName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFileName.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelFileName.Location = new System.Drawing.Point(17, 47);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(79, 23);
            this.labelFileName.TabIndex = 44;
            this.labelFileName.Text = "파일명";
            this.labelFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(102, 48);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(183, 23);
            this.textBoxFileName.TabIndex = 0;
            // 
            // FindRadiography
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 630);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FindRadiography";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "의료영상 저장";
            this.Load += new System.EventHandler(this.FindRadiography_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label labelPatientName;
        private System.Windows.Forms.TextBox textBoxPatientName;
        private System.Windows.Forms.Label labelChart;
        private System.Windows.Forms.TextBox textBoxChart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelFileName;
    }
}