using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    public partial class Main : Form
    {
        DBClass dbc = new DBClass();
        int timeMax = 20;
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
            timer1.Start();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dbc.Hospital_Open(textBoxHospitalID.Text);
            dbc.HospitalTable = dbc.DS.Tables["hospital"];
            if (textBoxHospitalID.Text.ToString() == "")
            {
                MessageBox.Show("아이디를 입력하세요.", "알림");
                textBoxHospitalID.Focus();
            }
            else if (textBoxPW.Text == "")
            {
                MessageBox.Show("비밀번호를 입력하세요.", "알림");
                textBoxPW.Focus();
            }
            else if (textBoxHospitalID.Text != dbc.HospitalTable.Rows[0]["hospitalID"].ToString())
            {
                MessageBox.Show("로그인정보 불일치.", "알림");
                TextBoxClear();
            }
            else
            {
                dbc.Hospital_Open(textBoxHospitalID.Text);
                DataTable hosTable = dbc.DS.Tables["hospital"];
                if (hosTable.Rows[0]["hospitalPW"].ToString() == textBoxPW.Text)
                {
                    Main2 main2 = new Main2();
                    main2.ShowDialog();
                    textBoxPW.Clear();
                }
                else
                {
                    MessageBox.Show("로그인정보 불일치", "알림");
                    TextBoxClear();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("미구현");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += 1;
            if (LoginLabel.Text == "로그인 중...")
            {
                LoginLabel.Text = "로그인 중";
            }
            LoginLabel.Text += ".";
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
                    
                    if (DBClass.hospiPW == dbc.SHA256Hash(textBoxPW.Text, textBoxHospitalID.Text))
                    {
                        dbc.FindDocument(textBoxHospitalID.Text);
                        Reception reception = new Reception();
                        reception.HospitalID = textBoxHospitalID.Text;
                        reception.ShowDialog();
                        textBoxPW.Clear();
                    }
                    else
                    {
                        MessageBox.Show("로그인정보 불일치", "알림");
                        TextBoxClear();
                    }
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
