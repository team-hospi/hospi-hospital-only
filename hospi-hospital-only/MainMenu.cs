using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace hospi_hospital_only
{
    public partial class MainMenu : Form
    {
        string hospitalID;

        DBClass dbc = new DBClass();
        Main main = new Main();
        CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");
        Inquiry inquiry = new Inquiry();
        string[] user;
        int indexNum;
        string noticeID;

        public MainMenu()
        {
            InitializeComponent();
            dbc.FireConnect();
            inquiry.FireConnect();
        }

        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            //병원 정보 불러오기
            dbc.Hospital_Open(hospitalID);
            dbc.Delay(400);
            // 날짜정보
            string date = DateTime.Now.ToString("yyyy-MM-dd ddd요일 ", cultures);
            string time = DateTime.Now.ToString("tt hh:mm", cultures);
            labelDate.Text = date;
            labelTime.Text = time;
            timer1.Start();
            // 접수처
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
            for(int i=0; i<dbc.ReceptionistTable.Rows.Count; i++)
            {
                string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                int length = name.Length;
                if (name.Substring(length-1) != ")")
                {
                    comboBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                }
            }

            // 진료실
            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];
            for (int i = 0; i < DBClass.hospidepartment.Length; i++)
            {
                    comboBoxOffice.Items.Add(DBClass.hospidepartment[i]);
            }

            label3.Text = dbc.Hospiname;

            // 공지사항 게시자를 위한 접수자, 과목정보 받아오기
            user = new string[comboBoxReceptionist.Items.Count + comboBoxOffice.Items.Count];
            for(int i=0; i< comboBoxOffice.Items.Count; i++)
            {
                user[i] = comboBoxOffice.Items[i].ToString();
            }
            int a = 0;
            for(int j=comboBoxOffice.Items.Count; j<comboBoxReceptionist.Items.Count+comboBoxOffice.Items.Count; j++)
            {
                user[j] = comboBoxReceptionist.Items[a].ToString();
                a++;
            }

            // 공지사항 띄우기
            listView2.Items.Clear();
            dbc.Notice_Open();
            dbc.NoticeTable = dbc.DS.Tables["Notice"];
            for(int i=0; i<dbc.NoticeTable.Rows.Count; i++)
            {
                if(Convert.ToInt32(dbc.NoticeTable.Rows[i]["NoticeEndDate"]) > Convert.ToInt32(DateTime.Now.ToString("yyMMdd")))
                {
                    listView2.Items.Add((listView2.Items.Count + 1).ToString());
                    listView2.Items[i].SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeTitle"].ToString());
                    listView2.Items[i].SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeID"].ToString());
                }
            }
        }

        private void buttonDispose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("tt hh:mm", cultures);
            labelTime.Text = time;

            //inquiry.checkinquiry(hospitalID);
            //inquirycount.Text = Inquiry.count.ToString(); ====> 파이어베이스 용량 문제로 보류
        }

        // 진료실
        private void buttonOffice_Click(object sender, EventArgs e)
        {
            if(comboBoxOffice.Text != "진료과목 선택")
            {
                Office office = new Office();
                office.SubjectID = comboBoxOffice.Text;
                this.Visible = false;
                office.ShowDialog();
                this.Visible = true;
                comboBoxOffice.Text = "진료과목 선택";
            }
            else
            {
                MessageBox.Show("과목을 선택해주세요.", "알림");
            }
        }

        // 접수처
        private void buttonReception_Click(object sender, EventArgs e)
        {
            if(comboBoxReceptionist.Text != "접수자 선택")
            {
                Reception reception = new Reception();
                reception.HospitalID = hospitalID;
                reception.ReceptionistName = comboBoxReceptionist.Text;
                this.Visible = false;
                reception.ShowDialog();
                this.Visible = true;
                comboBoxReceptionist.Text = "접수자 선택";
            }
            else
            {
                MessageBox.Show("접수자를 선택해주세요.", "알림");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 병원설정변경
        private void button3_Click(object sender, EventArgs e)
        {
            Hospital_Setting hospital_Setting = new Hospital_Setting();
            hospital_Setting.HospitalID = hospitalID;
            hospital_Setting.Show();
        }

        // 문의확인
        private void button4_Click(object sender, EventArgs e)
        {
            InquiryCheck inquiry = new InquiryCheck();
            inquiry.HospitalID = hospitalID;
            inquiry.ShowDialog();
        }

        // 공지사항 톱니바퀴 클릭
        private void setting1_Click(object sender, EventArgs e)
        {
            Notice notice = new Notice();
            notice.User = user;
            notice.ShowDialog();

            /*dbc.Notice_Open();
            dbc.NoticeTable = dbc.DS.Tables["Notice"];
            listView2.Items.Clear();
            for (int i = 0; i < dbc.NoticeTable.Rows.Count; i++)
            {
                if (Convert.ToInt32(dbc.NoticeTable.Rows[i]["NoticeEndDate"]) > Convert.ToInt32(DateTime.Now.ToString("yyMMdd")))
                {
                    listView2.Items.Add((listView2.Items.Count + 1).ToString());
                    listView2.Items[i].SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeTitle"].ToString());
                }
            }*/
            MainMenu_Load(sender, e);
        }

        private void listView2_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView2.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MainMenu_Load(sender, e);

            if (listView2.SelectedIndices.Count > 0)
            {
                string noticeID = listView2.Items[listView2.FocusedItem.Index].SubItems[2].Text.ToString();
                NoticeInfo noticeInfo = new NoticeInfo();
                noticeInfo.A = noticeID;
                noticeInfo.ShowDialog();
            }
        }
    }
}
