namespace hospi_hospital_only
{
    partial class StaffCreatePW
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelPW = new System.Windows.Forms.Label();
            this.textBoxPW2 = new System.Windows.Forms.TextBox();
            this.textBoxPW1 = new System.Windows.Forms.TextBox();
            this.labelID = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.labelPW);
            this.groupBox1.Controls.Add(this.textBoxPW2);
            this.groupBox1.Controls.Add(this.textBoxPW1);
            this.groupBox1.Controls.Add(this.labelID);
            this.groupBox1.Controls.Add(this.buttonCreate);
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 172);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(16, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(340, 32);
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
            this.labelPW.Size = new System.Drawing.Size(118, 23);
            this.labelPW.TabIndex = 6;
            this.labelPW.Text = "비밀번호 확인";
            this.labelPW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPW2
            // 
            this.textBoxPW2.Location = new System.Drawing.Point(140, 53);
            this.textBoxPW2.Name = "textBoxPW2";
            this.textBoxPW2.PasswordChar = '●';
            this.textBoxPW2.Size = new System.Drawing.Size(216, 23);
            this.textBoxPW2.TabIndex = 2;
            // 
            // textBoxPW1
            // 
            this.textBoxPW1.Location = new System.Drawing.Point(140, 19);
            this.textBoxPW1.Name = "textBoxPW1";
            this.textBoxPW1.PasswordChar = '●';
            this.textBoxPW1.Size = new System.Drawing.Size(216, 23);
            this.textBoxPW1.TabIndex = 1;
            // 
            // labelID
            // 
            this.labelID.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelID.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelID.Location = new System.Drawing.Point(16, 19);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(118, 23);
            this.labelID.TabIndex = 4;
            this.labelID.Text = "비밀번호";
            this.labelID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonCreate.Location = new System.Drawing.Point(16, 88);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(340, 32);
            this.buttonCreate.TabIndex = 4;
            this.buttonCreate.Text = "생 성";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // StaffCreatePW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 196);
            this.Controls.Add(this.groupBox1);
            this.Name = "StaffCreatePW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "신규 비밀번호 생성";
            this.Load += new System.EventHandler(this.StaffCreatePW_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelPW;
        private System.Windows.Forms.TextBox textBoxPW2;
        private System.Windows.Forms.TextBox textBoxPW1;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Button buttonCreate;
    }
}