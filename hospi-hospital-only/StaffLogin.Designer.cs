namespace hospi_hospital_only
{
    partial class StaffLogin
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelPW = new System.Windows.Forms.Label();
            this.textBoxPW = new System.Windows.Forms.TextBox();
            this.textBoxStaffId = new System.Windows.Forms.TextBox();
            this.labelID = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(123, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 69);
            this.label1.TabIndex = 42;
            this.label1.Text = "ID : master\r\nPW : master\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.labelPW);
            this.groupBox1.Controls.Add(this.textBoxPW);
            this.groupBox1.Controls.Add(this.textBoxStaffId);
            this.groupBox1.Controls.Add(this.labelID);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(128, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 232);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(16, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(227, 32);
            this.button2.TabIndex = 38;
            this.button2.Text = "회원 가입";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(16, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 32);
            this.button1.TabIndex = 37;
            this.button1.Text = "종 료";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.textBoxPW.Text = "master";
            this.textBoxPW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPW_KeyDown);
            // 
            // textBoxStaffId
            // 
            this.textBoxStaffId.Location = new System.Drawing.Point(93, 19);
            this.textBoxStaffId.Name = "textBoxStaffId";
            this.textBoxStaffId.Size = new System.Drawing.Size(150, 23);
            this.textBoxStaffId.TabIndex = 1;
            this.textBoxStaffId.Text = "master";
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
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.Location = new System.Drawing.Point(16, 88);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(227, 32);
            this.button6.TabIndex = 4;
            this.button6.Text = "접 속";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // StaffLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 382);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "StaffLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "직원 접속";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelPW;
        private System.Windows.Forms.TextBox textBoxPW;
        private System.Windows.Forms.TextBox textBoxStaffId;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button2;
    }
}