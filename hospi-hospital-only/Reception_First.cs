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
    public partial class Reception_First : Form
    {
        DBClass dbc = new DBClass();
        Security security = new Security();
        Reserve reserve = new Reserve();
        ReceptionList reception = new ReceptionList();
        Fcm fcm = new Fcm();
        int hospitalID; // 병원코드
        string visitorName; // 수진자명번호 ( Reception 폼으로 넘겨줌 )
        string mobileID;
        bool isreserve;
        string tel;
        string name;
        string time;
        string date;
        string subject;
        string receptionist;
        string comment;
        string doctor;
        int waiting;
        public Reception_First()
        {
            InitializeComponent();
        }

        // 차트번호 Reception으로 넘겨주기 위한 프로퍼티
        public string VisitorName
        {
            get { return visitorName; }
            set { visitorName = value; }
        }

        // 예약에서 넘어올 때 모바일ID를 받음
        public string MobileID
        {
            get { return mobileID; }
            set { mobileID = value; }
        }

        // 어디서 넘어왔는지 저장(초진,예약초진)
        public bool IsReserve
        {
            get { return isreserve; }
            set { isreserve = value; }
        }
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        public string Receptionist
        {
            get { return receptionist; }
            set { receptionist = value; }
        }
        public string Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public int Waiting
        {
            get { return waiting; }
            set { waiting = value; }
        }
        private void Receipt_First_Load(object sender, EventArgs e)
        {
             // 폼 로드시 수신자명 포커스
            this.ActiveControl = textBox1;
            reserve.FireConnect();
            reception.FireConnect();
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"]; // 접수자 테이블
            // 차트번호 DB오픈
            dbc.Visitor_Chart();
            dbc.VisitorTable = dbc.DS.Tables["visitor"];
            DataRow chartRow = dbc.VisitorTable.Rows[0];
            textBox2.Text = (Convert.ToInt32(chartRow["count(*)"])+1).ToString();
            if(isreserve)
            {
                phone1.Enabled = false;
                phone2.Enabled = false;
                phone3.Enabled = false;
                textBox1.Enabled = false;
                textBox1.Text = name;
                phone1.Text = tel.Substring(0, 3);
                phone2.Text = tel.Substring(4, 4);
                phone3.Text = tel.Substring(9, 4);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBoxB1.Text == "" || textBoxB1.Text == "" || 
                phone1.Text == "" ||  phone2.Text == "" ||  phone3.Text == "" ||  textBoxADD.Text == "")
            {
                MessageBox.Show("인적사항에 공백이 있습니다.", "알림");
            }
            else if(textBoxB1.TextLength != 6 || textBoxB2.TextLength != 7 )
            {
                MessageBox.Show("주민등록번호 형식이 잘못되었습니다.", "알림");
                // 문자열 입력방지 추가해야함
            }
            else if(phone1.TextLength != 3 || (phone2.TextLength != 4 && phone2.TextLength != 3) || phone3.TextLength != 4)
            {
                MessageBox.Show("전화번호 형식이 잘못되었습니다.", "알림");
                // 문자열 입력방지 추가해야함
            }
            else
            {
                DialogResult ok = MessageBox.Show("수진자 : "+textBox1.Text+"\r접수를 완료하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc.Visitor_Open();
                        dbc.DBAdapter.Fill(dbc.DS, "visitor");
                        dbc.VisitorTable = dbc.DS.Tables["visitor"];
                        DataRow newRow = dbc.VisitorTable.NewRow();

                        newRow["PatientID"] = textBox2.Text;
                        newRow["PatientName"] = textBox1.Text;
                        newRow["PatientBirthCode"] = textBoxB1.Text + "-" + textBoxB2.Text.Substring(0,1)+security.AESEncrypt128(textBoxB2.Text.Substring(1), DBClass.hospiID);
                        newRow["PatientPhone"] = phone1.Text + phone2.Text + phone3.Text;
                        newRow["PatientAddress"] = textBoxADD.Text;
                        if(isreserve)
                        {
                            newRow["MemberID"] = mobileID;
                        }
                        dbc.VisitorTable.Rows.Add(newRow);
                        dbc.DBAdapter.Update(dbc.DS, "visitor");
                        dbc.DS.AcceptChanges();
                        if(isreserve)
                        {
                            ReceptionAdd();
                        }
                    }
                    catch (DataException DE)
                    {
                        MessageBox.Show(DE.Message);
                    }

                    visitorName = textBox1.Text;
                    Dispose();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBoxB1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자만 입력되도록 필터링
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
            if(textBoxB1.Text.Length>5 && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBoxB2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자만 입력되도록 필터링
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
            if (textBoxB2.Text.Length > 6 && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void phone1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
            if (phone1.Text.Length > 2 && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void phone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
            if (phone2.Text.Length > 3 && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void phone3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
            if (phone3.Text.Length > 3 && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                e.Handled = true;
            }
        }

        //예약 -> 접수 등록
        public void ReceptionAdd()
        {

            dbc.Reception_Open();
            dbc.ReceptionTable = dbc.DS.Tables["Reception"];
            DataRow newRow = dbc.ReceptionTable.NewRow();
            newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
            
            newRow["PATIENTID"] = Convert.ToInt32(textBox2.Text);


            newRow["ReceptionTime"] = time.Substring(0, 2) + time.Substring(3, 2);
            newRow["ReceptionDate"] = date.Substring(2, 8);
            newRow["SubjectName"] = subject;
            for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
            {
                if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == receptionist)
                {
                    newRow["ReceptionistCode"] = i + 1;
                }
            }

            newRow["ReceptionInfo"] = comment;
            newRow["ReceptionType"] = 1;
            reserve.FindDocument(Time, mobileID, date, subject);
            dbc.Delay(200);
            newRow["ReceptionCode"] = Reserve.documentName;
            dbc.ReceptionTable.Rows.Add(newRow);
            dbc.DBAdapter.Update(dbc.DS, "Reception");
            dbc.DS.AcceptChanges();

            reserve.ReserveOpen();
            dbc.Delay(200);

            reserve.ReserveAccept();
            dbc.Delay(200);
            reception.ReceptionAccept(subject, doctor, mobileID, name, date, time, waiting);
            MessageBox.Show("예약이 접수되었습니다.", "알림");


            //알림 메시지 전송
            fcm.PushNotificationToFCM(DBClass.hospiname, Reserve.UserToken, "[" + date + " " + FindDay(date) + " " + time + "] " + " 예약이 확정되었습니다.");
        }

        //요일 검색
        public string FindDay(string Date)
        {
            string day = "";
            DateTime date = new DateTime();
            date = Convert.ToDateTime(Date);

            if (date.DayOfWeek == DayOfWeek.Monday)
                day = "(월)";
            else if (date.DayOfWeek == DayOfWeek.Thursday)
                day = "(화)";
            else if (date.DayOfWeek == DayOfWeek.Wednesday)
                day = "(수)";
            else if (date.DayOfWeek == DayOfWeek.Thursday)
                day = "(목)";
            else if (date.DayOfWeek == DayOfWeek.Friday)
                day = "(금)";
            else if (date.DayOfWeek == DayOfWeek.Saturday)
                day = "(토)";
            else if (date.DayOfWeek == DayOfWeek.Sunday)
                day = "(일)";

            return day;
        }
    }
}
