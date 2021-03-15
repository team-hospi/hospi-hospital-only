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
        int timeMax = 200;
        int time = 0;

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

            LoginLabel.Visible = true;

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
                Thread rTh = new Thread(Login);
                rTh.Start();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dbc.FireConnect();
            dbc.FireLogin(dbc.SHA256Hash(textBoxPW.Text, textBoxHospitalID.Text));

            dbc.Delay(200);

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
            else if (DBClass.hospiPW == dbc.SHA256Hash(textBoxPW.Text, textBoxHospitalID.Text))
            {
                
                dbc.FindDocument(textBoxHospitalID.Text);
                Main2 main2 = new Main2();
                main2.HospitalID = textBoxHospitalID.Text;
                main2.ShowDialog();

                textBoxPW.Clear();
                
            }
            else
            {
                MessageBox.Show("로그인정보 불일치", "알림");
                TextBoxClear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("미구현");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += 1;
            if (time == timeMax)
            {
                timer1.Stop();
                time = 0;
                LoginLabel.Visible = false;
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
                else if (DBClass.hospiPW != dbc.SHA256Hash(textBoxPW.Text, textBoxHospitalID.Text))
                {
                    MessageBox.Show("로그인정보 불일치.", "알림");
                    TextBoxClear();
                }
                else
                {

                    
                }
            }
        }

        public void Login()
        {
            int cnt = 0;
            while(true)
            {
                ++cnt;
                Thread.Sleep(200);

                if (LoginLabel.Text == "로그인 중...")
                {
                    LoginLabel.Text = "로그인 중";
                }
                LoginLabel.Text += ".";

                if (DBClass.hospiPW == dbc.SHA256Hash(textBoxPW.Text, textBoxHospitalID.Text))
                {
                    LoginLabel.Visible = false;
                    dbc.FindDocument(textBoxHospitalID.Text);
                    Reception reception = new Reception();
                    reception.HospitalID = textBoxHospitalID.Text;
                    reception.ShowDialog();
                    textBoxPW.Clear();
                    break;
                }
                else if(cnt > 150)
                {
                    MessageBox.Show("로그인정보 불일치", "알림");
                    TextBoxClear();
                        break;
                }
            }

            }
        
    }
}
