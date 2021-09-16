using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace hospi_hospital_only
{
    public partial class Main : Form
    {
        DBClass dbc = new DBClass();
        private bool loginSuccess;

        public Main()
        {
            InitializeComponent();
            this.ActiveControl = textBoxHospitalID;

            token1.GotFocus += new EventHandler(textBox_GotFocus);
            token2.GotFocus += new EventHandler(textBox_GotFocus);
            token3.GotFocus += new EventHandler(textBox_GotFocus);
            token4.GotFocus += new EventHandler(textBox_GotFocus);
        }

        private void textBox_GotFocus(object sender, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate { (sender as TextBox).SelectAll(); })
            );
        }

        public void TextBoxClear() // 로그인 정보 불일치시 ID,PW 텍스트박스 비워주고 포커스
        {
            textBoxHospitalID.Clear();
            textBoxPW.Clear();
            textBoxHospitalID.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            loginSuccess = false;

            dbc.FireConnect();

            
            dbc.FireLogin(dbc.SHA256Hash(textBoxPW.Text, textBoxHospitalID.Text));


            if (textBoxHospitalID.Text == "")
            {
                MessageBox.Show("아이디를 입력하세요.", "알림");
                textBoxHospitalID.Focus();
                button6.Enabled = true;
            }
            else if (textBoxPW.Text == "")
            {
                MessageBox.Show("비밀번호를 입력하세요.", "알림");
                textBoxPW.Focus();
                button6.Enabled = true;
            }
            else
            {
                DBClass.DBname = textBoxHospitalID.Text;
                button6.Enabled = false;
                LoginLabel.Visible = true;
                Thread rTh = new Thread(Login);
                rTh.Start();
                dbc.Delay(3000);

                this.Visible = false;

                StaffLogin staffLogin = new StaffLogin();

                if (loginSuccess == true)
                {
                    loginSuccess = true;
                    button6.Enabled = true;
                    LoginLabel.Visible = false;
                    dbc.FindDocument(textBoxHospitalID.Text);
                    staffLogin.HospitalID = textBoxHospitalID.Text;
                    try
                    {
                        staffLogin.ShowDialog();
                    }
                    catch
                    {

                    }
                    textBoxPW.Clear();

                }
                else if (loginSuccess == false)
                {
                    button6.Enabled = true;
                    LoginLabel.Visible = false;
                    MessageBox.Show("로그인정보 불일치", "알림");
                    TextBoxClear();
                }

                rTh.Abort();
            }

            this.Visible = true;
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
                    loginSuccess = true;
                    break;
                }
                else if (cnt > 30)
                {
                    loginSuccess = false;

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

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        #region 토큰 텍스트박스 이벤트
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(token1.Text.Length == 5)
            {
                token2.Focus();
            }
        }

        private void token2_TextChanged(object sender, EventArgs e)
        {
            if (token2.Text.Length == 5)
            {
                token3.Focus();
            }
        }

        private void token3_TextChanged(object sender, EventArgs e)
        {
            if (token3.Text.Length == 5)
            {
                token4.Focus();
            }
        }

        private void token4_TextChanged(object sender, EventArgs e)
        {
            if (token4.Text.Length == 5)
            {
                button2.Focus();
            }
        }
        #endregion

        private void button2_Click_2(object sender, EventArgs e)
        {
            string productKey = token1.Text + "-" + token2.Text + "-" + token3.Text + "-" + token4.Text;

            dbc.SelectProductKey(productKey);
            dbc.ProductKeyTable = dbc.DS.Tables["payment"];

            if (productKey.Length != 23)
            {
                MessageBox.Show("인증키의 형식이 잘못되었습니다.", "알림");
            }
            else
            {
                if (dbc.ProductKeyTable.Rows.Count == 1)
                {
                    // 인증키 정보 보내고 병원가입폼 띄움
                    string productKeyForSchema = token1.Text + token2.Text + token3.Text + token4.Text;

                    Hospital_SignUp hospital_Sign = new Hospital_SignUp();
                    hospital_Sign.ProductKeyForSchema = productKeyForSchema;    //스키마 저장용
                    hospital_Sign.ProductKey = productKey;    // '-' 포함된 변수용
                    hospital_Sign.ShowDialog();
                }
                else
                {
                    MessageBox.Show("MySQL에 토근 없음");
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 테스트용 토큰정보 넣기
            token1.Text = "57C8J";
            token2.Text = "NS1TJ";
            token3.Text = "GC7PR";
            token4.Text = "GWWZ0";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // 토큰 인증상태 확인
            if(Properties.Settings.Default.ProductKey == string.Empty)
            {
                MessageBox.Show("인증키 저장되지 않음", "알림");
            }
            else
            {
                MessageBox.Show("인증키 저장됨", "알림");
            }
           
        }
    }
}

