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
        string productKey;

        public Main()
        {
            InitializeComponent();
            this.ActiveControl = token1;

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

                
                if (DBClass.hospiID == productKey)
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
            productKey = token1.Text + token2.Text + token3.Text + token4.Text;

            dbc.SelectProductKey(productKey);
            dbc.ProductKeyTable = dbc.DS.Tables["payment"];

            if (productKey.Length != 20)
            {
                MessageBox.Show("인증키의 형식이 잘못되었습니다.", "알림");
            }
            else
            {
                if (dbc.ProductKeyTable.Rows.Count == 1)
                {
                    try
                    {
                        dbc.FireConnect();
                        dbc.Delay(200);
                        dbc.Hospital_Open(productKey);
                        dbc.Delay(200);
                        LoginLabel.Visible = true;
                        Thread rTh = new Thread(Login);
                        rTh.Start();
                        dbc.Delay(3000);

                        StaffLogin staffLogin = new StaffLogin();

                        if (loginSuccess == true)
                        {

                            LoginLabel.Visible = false;
                            dbc.FindDocument(DBClass.hospiID);
                            staffLogin.HospitalID = DBClass.hospiID;
                            try
                            {
                                SaveProductKey(productKey);
                                
                                staffLogin.ShowDialog();
                                Dispose();
                            }
                            catch
                            {

                            }
                        }
                        else if (loginSuccess == false)
                        {

                            MessageBox.Show("등록을 위해 병원 정보를 입력해주세요.", "알림");
                            // 인증키 정보 보내고 병원가입폼 띄움

                            Hospital_SignUp hospital_Sign = new Hospital_SignUp();
                            hospital_Sign.ProductKeyForSchema = productKey;    //스키마 저장용
                            hospital_Sign.ShowDialog();
                            Dispose();
                        }

                        rTh.Abort();
                    }
                    catch
                    {
                        
                    }
                }
                else
                {
                    MessageBox.Show("MySQL에 토근 없음");
                }
            }

        }

        private void SaveProductKey(string ProductKeyValue)
        {
            Properties.Settings.Default.ProductKey = ProductKeyValue;
            Properties.Settings.Default.Save();
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

