namespace hospi_hospital_only
{
    partial class Main2
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
            this.comboBoxSub = new System.Windows.Forms.ComboBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.settingLabel = new System.Windows.Forms.Label();
            this.groupBox11.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxSub
            // 
            this.comboBoxSub.FormattingEnabled = true;
            this.comboBoxSub.Location = new System.Drawing.Point(27, 54);
            this.comboBoxSub.Name = "comboBoxSub";
            this.comboBoxSub.Size = new System.Drawing.Size(162, 23);
            this.comboBoxSub.TabIndex = 36;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.settingLabel);
            this.groupBox11.Controls.Add(this.button1);
            this.groupBox11.Controls.Add(this.comboBoxSub);
            this.groupBox11.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox11.Location = new System.Drawing.Point(12, 12);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(296, 110);
            this.groupBox11.TabIndex = 37;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "진료 과목";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "확인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // settingLabel
            // 
            this.settingLabel.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.settingLabel.Location = new System.Drawing.Point(267, 9);
            this.settingLabel.Name = "settingLabel";
            this.settingLabel.Size = new System.Drawing.Size(27, 25);
            this.settingLabel.TabIndex = 37;
            this.settingLabel.Text = "⚙";
            // 
            // Main2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 135);
            this.Controls.Add(this.groupBox11);
            this.Name = "Main2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main2_Load);
            this.groupBox11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxSub;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label settingLabel;
    }
}