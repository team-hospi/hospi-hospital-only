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
    public partial class CheckMasterPW : Form
    {
        DBClass dbc = new DBClass();
        string hospitalID;
        int passwordOK = 0; // 공지사항 수정버튼 // 비밀번호 참:1 오류:0
        string noticeWriter;    // 공지사항 작성자 문자열
        int masterID;             // master 아이디 저장

        int formNum;
        /* 
         1 = Hospital_Setting
         2 = Notice
         3 = NoticeInfo (수정버튼)
         4 = MasterMenu
             */

        public int FormNum
        {
            get { return formNum; }
            set { formNum = value; }
        }
        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }
        public int PasswordOK
        {
            get { return passwordOK; }
            set { passwordOK = value; }
        }
        public string NoticeWriter
        {
            get { return noticeWriter; }
            set { noticeWriter = value; }
        }

        public CheckMasterPW()
        {
            InitializeComponent();
        }

        public void Open_Form(int formNum)
        {
            if (formNum == 1)
            {
                Hospital_Setting hospital_Setting = new Hospital_Setting();
                hospital_Setting.HospitalID = hospitalID;
                hospital_Setting.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBoxPW.Text == "")
            {
                MessageBox.Show("비밀번호를 입력해주세요.", "알림");
                textBoxPW.Focus();
            }
            else
            {
                dbc.Master_Open();
                dbc.MasterTable = dbc.DS.Tables["master"];

                if (dbc.MasterTable.Rows[masterID]["masterPassword"].ToString() == textBoxPW.Text)
                {
                    if (formNum == 1)    // 병원정보 설정
                    {
                        Hospital_Setting hospital_Setting = new Hospital_Setting();
                        hospital_Setting.HospitalID = hospitalID;
                        hospital_Setting.ShowDialog();
                        if (hospital_Setting.IsDisposed || hospital_Setting == null)
                        {
                            Dispose();
                        }
                    }
                    else if (formNum == 2)    // 공지사항 등록
                    {
                        Notice notice = new Notice();
                        notice.Writer = dbc.MasterTable.Rows[masterID]["MasterName"].ToString();
                        notice.ShowDialog();
                        if (notice.IsDisposed || notice == null)
                        {
                            Dispose();
                        }
                    }
                    else if (formNum == 3)   // 공지사항 수정
                    {
                        passwordOK = 1;
                        Dispose();
                    }
                    else if (formNum == 4)   // 관리자 메뉴
                    {
                        MasterMenu masterMenu = new MasterMenu();
                        masterMenu.ShowDialog();
                    }
                }
                else if(dbc.MasterTable.Rows[masterID]["masterPassword"].ToString() != textBoxPW.Text)
                {
                    MessageBox.Show("비밀번호를 확인해주세요.", "알림");
                    textBoxPW.Text = "";
                    textBoxPW.Focus();
                }
            }
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 엔터이벤트
        private void textBoxPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        private void CheckMasterPW_Load(object sender, EventArgs e)
        {
            dbc.Master_Open();
            dbc.MasterTable = dbc.DS.Tables["master"];
            
            for(int i=0; i<dbc.MasterTable.Rows.Count; i++)
            {
                comboBoxMaster.Items.Add(dbc.MasterTable.Rows[i]["masterName"].ToString());
            }
            comboBoxMaster.Text = comboBoxMaster.Items[0].ToString();

            // 공지사항 수정일 경우 콤보박스 Enable = false, 관리자명고정 
            if(formNum == 3)
            {
                comboBoxMaster.Enabled = false;
                comboBoxMaster.Text = noticeWriter;
            }

        }

        private void comboBoxMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            masterID = comboBoxMaster.SelectedIndex;
            textBoxPW.Focus();
        }

        private void comboBoxMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 'º')
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        // 관리자명 문자입력 방지 (콤보박스로만 변경할 수 있도록)

    }
}
