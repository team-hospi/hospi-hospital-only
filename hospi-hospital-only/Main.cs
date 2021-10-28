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
        string str;
        int textlength;
        TextBox[] txtList;
        const string TokenPlaceholder = "XXXXX-XXXXX-XXXXX-XXXXX";

        public Main()
        {
            InitializeComponent();
            dbc.FireConnect();
            //this.ActiveControl = token1;

            //token1.GotFocus += new EventHandler(textBox_GotFocus);

            //ID, Password TextBox Placeholder 설정
            txtList = new TextBox[] { token1 };
            foreach (var txt in txtList)
            {
                //처음 공백 Placeholder 지정
                txt.ForeColor = Color.DarkGray;
                if (txt == token1) txt.Text = TokenPlaceholder;
                //텍스트박스 커서 Focus 여부에 따라 이벤트 지정
                txt.GotFocus += RemovePlaceholder;
                txt.LostFocus += SetPlaceholder;
            }
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
                LoginLabel.Text += ".";
                if (LoginLabel.Text == "인증 진행중...")
                {
                    LoginLabel.Text = "인증 진행중";
                }
                

                
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
            
        }

        #endregion

        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {
                string token = token1.Text;
                productKey = token.Substring(0, 5) + token.Substring(6, 5) + token.Substring(12, 5) + token.Substring(18, 5);

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
                                DBClass.hospiID = productKey;
                                Hospital_SignUp hospital_Sign = new Hospital_SignUp();
                                hospital_Sign.ProductKeyForSchema = productKey;    //스키마 저장용
                                this.Visible = false;
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
            catch
            {
                MessageBox.Show("인증코드가 올바르지 않습니다.");
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
            token1.Text = "57C8J-NS1TJ-GC7PR-GWWZ0";

           /*token1.Text = "08475";
            token2.Text = "F4L1Q";
            token3.Text = "JP7G8";
            token4.Text = "SZS3I";*/
        }

        private void token1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    MessageBox.Show("공백은 입력할 수 없습니다.");
                    token1.Text = token1.Text.Substring(0, token1.Text.LastIndexOf(" "));
                }
                else if (e.KeyCode != Keys.Back && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Delete)
                {
                    str = "";
                    textlength = token1.TextLength;
                    for (int i = 0; i < textlength; i++)
                    {
                        if (token1.Text.Substring(i, 1) != "-")
                            str += token1.Text.Substring(i, 1);
                        if (str.Length == 5 || str.Length == 11 || str.Length == 17)
                            str += "-";
                    }
                    if (str.Length > 23)
                        str = str.Substring(0,23);
                    token1.Clear();
                    token1.AppendText(str);
                }
            }
            catch
            {

            }
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == TokenPlaceholder)
            { //텍스트박스 내용이 사용자가 입력한 값이 아닌 Placeholder일 경우에만, 커서 포커스일때 빈칸으로 만들기
                txt.ForeColor = Color.Black; //사용자 입력 진한 글씨
                txt.Text = string.Empty;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                //사용자 입력값이 하나도 없는 경우에 포커스 잃으면 Placeholder 적용해주기
                txt.ForeColor = Color.DarkGray; //Placeholder 흐린 글씨
                if (txt == token1) txt.Text = TokenPlaceholder;
            }
        }
    }
}

