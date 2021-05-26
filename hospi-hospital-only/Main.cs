﻿using Microsoft.Toolkit.Uwp.Notifications;
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
        int login;

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
                
                for(int i =0; i<1; i++)
                {
                    if (login == 1)
                    {
                        rTh.Abort();
                        MessageBox.Show("가나다");
                    }
                    else if (login == 0)
                    {
                        i = 0;
                    }
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Hospital_SignUp hospital_Sign = new Hospital_SignUp();
            hospital_Sign.ShowDialog();
        }


        public void Login()
        {
            int cnt = 0;
            MainMenu mainmenu = new MainMenu();

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
                    mainmenu.HospitalID = textBoxHospitalID.Text;
                    this.Visible = false;
                    mainmenu.ShowDialog();
                    textBoxPW.Clear();
                    login = 1;
                    break;
                }
                else if (cnt > 30)
                {

                    button6.Enabled = true;
                    LoginLabel.Visible = false;
                    MessageBox.Show("로그인정보 불일치", "알림");
                    TextBoxClear();
                    break;
                }
            }
            if(mainmenu.IsDisposed || mainmenu == null)
            {
                this.Visible = true;   
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

        private void button3_Click(object sender, EventArgs e)
        {
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Andrew sent you a picture")
                    .AddText("Check this out, The Enchantments in Washington!")
                    .Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            int mm = Convert.ToInt32(dt.ToString("mm")) - 2;
            int dd = Convert.ToInt32(dt.ToString("ddHH"+mm+"ss"));
            
            
            MessageBox.Show(dd.ToString());
        }

        // 엔터이벤트
        private void textBoxPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            AddImage addImage = new AddImage();
            addImage.ShowDialog();
        }
    }
}
