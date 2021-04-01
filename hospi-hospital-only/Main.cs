using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    public partial class Main : Form
    {
        DBClass dbc = new DBClass();
        public Main()
        {
            InitializeComponent();
            this.ActiveControl = textBoxHospitalID;
        }

        public void TextBoxClear() // 로그인 정보 불일치시 ID,PW 텍스트박스 비워주고 포커스
        {
            textBoxHospitalID.Clear();
            textBoxPW.Clear();
            textBoxHospitalID.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dbc.FireConnect();
            dbc.FireLogin(dbc.SHA256Hash(textBoxPW.Text, textBoxHospitalID.Text));


            if (textBoxHospitalID.Text == "")
            {
                MessageBox.Show("아이디를 입력하세요.", "알림");
                textBoxHospitalID.Focus();
            }
            else if (textBoxPW.Text == "")
            {
                MessageBox.Show("비밀번호를 입력하세요.", "알림");
                textBoxPW.Focus();
            }
            else
            {

                button6.Enabled = false;
                LoginLabel.Visible = true;
                Thread rTh = new Thread(Login);
                rTh.Start();
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            
        }


        public void Login()
        {
            int cnt = 0;

            while (true)
            {
                ++cnt;
                Thread.Sleep(200);
                CheckForIllegalCrossThreadCalls = false;
                if (LoginLabel.Text == "로그인 중...")
                {
                    LoginLabel.Text = "로그인 중";
                }
                LoginLabel.Text += ".";

                if (DBClass.hospiPW == dbc.SHA256Hash(textBoxPW.Text, textBoxHospitalID.Text))
                {
                    button6.Enabled = true;
                    LoginLabel.Visible = false;
                    dbc.FindDocument(textBoxHospitalID.Text);
                    MainMenu mainmenu = new MainMenu();
                    mainmenu.HospitalID = textBoxHospitalID.Text;
                    mainmenu.ShowDialog();
                    textBoxPW.Clear();
                    break;
                }
                else if (cnt > 150)
                {

                    button6.Enabled = true;
                    LoginLabel.Visible = false;
                    MessageBox.Show("로그인정보 불일치", "알림");
                    TextBoxClear();
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            this.Visible = false;
            mm.HospitalID = textBoxHospitalID.Text;
            mm.ShowDialog();
            this.Visible = true;
        }
    }
}
