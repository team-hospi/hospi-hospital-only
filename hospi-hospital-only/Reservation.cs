﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    public partial class Reservation : Form
    {
        DBClass dbc = new DBClass();
        Reserve reserve = new Reserve();
        ReceptionList reception = new ReceptionList();

        int SelectRow;
        string hospitalID;
        string comment;
        string status;
        string receptionist;
        int receptionIndex;
        public string reserveStatus;

        string selectid;
        string selectdepartment;
        string selectDate;
        string selectTime;
        string selectHour;
        string selectminuit;


        string today = DateTime.Now.ToString("yyyy-MM-dd (ddd) HH:mm");
        int state = 0;

        List<string> Time = new List<string>();

        static int ReserveCanceled = -1;
        static int ReserveWait = 0;
        static int ReserveAccepted = 1;

        Fcm fcm = new Fcm();

        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        public string Receptionist
        {
            get { return receptionist; }
            set { receptionist = value; }
        }

        public Reservation()
        {
            InitializeComponent();
        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            reception.FireConnect();
            reserve.FireConnect();
            reserve.ReserveOpen(hospitalID);
            dbc.Delay(400);
            ReservationListUpdate(state);
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"]; // 접수자 테이블
            dbc.Visitor_Open();
            dbc.VisitorTable = dbc.DS.Tables["visitor"]; // 환자 테이블
            
        }
        //리스트뷰 업데이트
        public void ReservationListUpdate(int status)
        {
            listViewReserve.Items.Clear();
            
            for (int i = 0; i < reserve.list.Count; i++)
            {
                if (reserve.list[i].reservationStatus != ReserveCanceled && status == 0)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = reserve.list[i].id;
                    item.SubItems.Add(ConvertDate(reserve.list[i].timestamp).ToString("yyyy-MM-dd HH:mm"));
                    item.SubItems.Add(reserve.list[i].reservationDate);
                    item.SubItems.Add(reserve.list[i].reservationTime);
                    if (reserve.list[i].reservationStatus == ReserveWait)
                    {
                        item.SubItems.Add("대기");
                    }
                    else if (reserve.list[i].reservationStatus == ReserveAccepted)
                    {
                        item.SubItems.Add("승인됨");
                    }
                    item.SubItems.Add(reserve.list[i].id);
                    item.SubItems.Add(reserve.list[i].department);
                    listViewReserve.Items.Add(item);

                    this.listViewReserve.ListViewItemSorter = new ListviewItemComparer(1, "asc");
                    listViewReserve.Sort();
                }
                else if (reserve.list[i].reservationStatus == ReserveWait && status == 1)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = reserve.list[i].id;
                    item.SubItems.Add(ConvertDate(reserve.list[i].timestamp).ToString("yyyy-MM-dd HH:mm"));
                    item.SubItems.Add(reserve.list[i].reservationDate);
                    item.SubItems.Add(reserve.list[i].reservationTime);
                    item.SubItems.Add("대기");
                    item.SubItems.Add(reserve.list[i].id);
                    item.SubItems.Add(reserve.list[i].department);
                    listViewReserve.Items.Add(item);

                    this.listViewReserve.ListViewItemSorter = new ListviewItemComparer(1, "asc");
                    listViewReserve.Sort();
                }
                else if(reserve.list[i].reservationStatus == ReserveAccepted && status ==2)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = reserve.list[i].id;
                    item.SubItems.Add(ConvertDate(reserve.list[i].timestamp).ToString("yyyy-MM-dd HH:mm"));
                    item.SubItems.Add(reserve.list[i].reservationDate);
                    item.SubItems.Add(reserve.list[i].reservationTime);
                    item.SubItems.Add("승인됨");
                    item.SubItems.Add(reserve.list[i].id);
                    item.SubItems.Add(reserve.list[i].department);
                    listViewReserve.Items.Add(item);

                    this.listViewReserve.ListViewItemSorter = new ListviewItemComparer(1, "asc");
                    listViewReserve.Sort();
                }
                else if (reserve.list[i].reservationDate == DateTime.Now.ToString("yyyy-MM-dd") && status == 3)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = reserve.list[i].id;
                    item.SubItems.Add(ConvertDate(reserve.list[i].timestamp).ToString("yyyy-MM-dd HH:mm"));
                    item.SubItems.Add(reserve.list[i].reservationDate);
                    item.SubItems.Add(reserve.list[i].reservationTime);
                    if (reserve.list[i].reservationStatus == ReserveWait)
                    {
                        item.SubItems.Add("대기");
                    }
                    else if (reserve.list[i].reservationStatus == ReserveAccepted)
                    {
                        item.SubItems.Add("승인됨");
                    }
                    item.SubItems.Add(reserve.list[i].id);
                    item.SubItems.Add(reserve.list[i].department);
                    listViewReserve.Items.Add(item);

                    this.listViewReserve.ListViewItemSorter = new ListviewItemComparer(1, "asc");
                    listViewReserve.Sort();
                }
            }
        }

        //timestamp -> DateTime변형 함수
        public DateTime ConvertDate(long timestamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(timestamp).ToLocalTime();
            return dtDateTime;

        }

        //리스트뷰 정렬
        class ListviewItemComparer : IComparer
        {
            private int col;
            public string sort = "asc";
            public ListviewItemComparer()
            {
                col = 0;
            }

            public ListviewItemComparer(int column, string sort)
            {
                col = column;
                this.sort = sort;
            }

            public int Compare(object x, object y)
            {
                if (sort == "asc")
                {
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                }
            }
        }





        //더블클릭 이벤트
        private void listViewReserve_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectRow = listViewReserve.SelectedItems[0].Index;

            reserveStatus = listViewReserve.Items[SelectRow].SubItems[4].Text;

            selectDate = listViewReserve.Items[SelectRow].SubItems[2].Text;
            selectTime = listViewReserve.Items[SelectRow].SubItems[3].Text;
            selectdepartment = listViewReserve.Items[SelectRow].SubItems[6].Text;
            selectHour = listViewReserve.Items[SelectRow].SubItems[3].Text.Substring(0, 2);
            selectminuit = listViewReserve.Items[SelectRow].SubItems[3].Text.Substring(3, 2);
            selectid = listViewReserve.Items[SelectRow].SubItems[0].Text;
            //환자 정보 검색
            reserve.FindPatient(listViewReserve.Items[SelectRow].SubItems[0].Text);

            dbc.Delay(200);
            textBoxName.Text = reserve.patientName;
            //문서찾기(병원id, 시간, 환자id, 날짜)
            reserve.FindDocument(hospitalID, listViewReserve.Items[SelectRow].SubItems[3].Text, listViewReserve.Items[SelectRow].SubItems[0].Text, listViewReserve.Items[SelectRow].SubItems[2].Text);
            reserve.FindReserveDocument(hospitalID, listViewReserve.Items[SelectRow].SubItems[6].Text);
            TextBoxComment.Text = ViewComment();
        }

        //추가 내용 확인
        public string ViewComment()
        {

            for (int i = 0; i < listViewReserve.Items.Count; i++)
            {
                if (reserve.list[i].id == listViewReserve.Items[SelectRow].SubItems[0].Text && reserve.list[i].reservationDate == listViewReserve.Items[SelectRow].SubItems[2].Text && reserve.list[i].reservationTime == listViewReserve.Items[SelectRow].SubItems[3].Text)
                {
                    comment = reserve.list[i].additionalContent;
                }
            }
            return comment;
        }

        //예약 승인 버튼 클릭이벤트
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewReserve.Items[SelectRow].SubItems[4].Text == "대기")
                {


                    reserve.ReserveAccept();
                    dbc.Delay(200);


                    if (listViewReserve.Items[SelectRow].SubItems[2].Text == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        try
                        {

                            dbc.countWaiting(selectdepartment, selectHour + selectminuit, selectDate.Substring(2, 8));
                            dbc.ReceptionTable = dbc.DS.Tables["Reception"];

                            reception.ReceptionAccept(selectdepartment, selectid, textBoxName.Text, selectDate, selectTime, Convert.ToInt32(dbc.ReceptionTable.Rows[0][0]));

                            ReceptionAdd();

                            reception.TodayReceptionOpen(hospitalID, listViewReserve.Items[SelectRow].SubItems[6].Text);
                            
                            dbc.Delay(400);
                            for (int i = 0; i < reception.list.Count; i++)
                            {
                                dbc.countWaiting(reception.list[i].department, reception.list[i].receptionTime, DateTime.Now.ToString("yy-MM-dd"));
                                dbc.WaitingTable = dbc.DS.Tables["Reception"];

                                reception.FindDocument(hospitalID, reception.today, reception.list[i].receptionTime, reception.list[i].id, reception.list[i].department);
                                dbc.Delay(200);
                                reception.watingNumberUpdate(Convert.ToInt32(dbc.WaitingTable.Rows[0][0]));
                                dbc.Delay(200);
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            newPatientAdd();

                            ReceptionAdd();

                            reception.TodayReceptionOpen(hospitalID, listViewReserve.Items[SelectRow].SubItems[6].Text);
                            dbc.Delay(200);
                            for (int i = 0; i < reception.list.Count; i++)
                            {

                                dbc.countWaiting(reception.list[i].department, reception.list[i].receptionTime, DateTime.Now.ToString("yy-MM-dd"));
                                dbc.WaitingTable = dbc.DS.Tables["Reception"];

                                reception.FindDocument(hospitalID, reception.today, reception.list[i].receptionTime, reception.list[i].id, reception.list[i].department);
                                dbc.Delay(200);
                                reception.watingNumberUpdate(Convert.ToInt32(dbc.WaitingTable.Rows[0][0]));
                                dbc.Delay(200);
                            }
                        }
                    }

                    reserve.ReserveOpen(hospitalID);
                    dbc.Delay(200);

                    MessageBox.Show("예약이 접수되었습니다.", "알림");

                    ReservationListUpdate(state);
                    
                    //알림 메시지 전송
                    fcm.PushNotificationToFCM(DBClass.hospiname, Reserve.UserToken, "[" + selectDate + " " + FindDay(selectDate) + " "+ selectTime + "] " + " 예약이 확정되었습니다.");
                }
                else if (listViewReserve.Items[SelectRow].SubItems[4].Text == "승인됨")
                {
                    MessageBox.Show("이미 접수가 완료된 예약입니다", "알림");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //예약 취소 버튼 클릭 이벤트
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("예약을 취소하시겠습니까?", "예약 취소", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                ReserveCancel reservecancel = new ReserveCancel();
                    reservecancel.HospitalID = hospitalID;
                    reservecancel.Date = selectDate;
                    reservecancel.Time = selectTime;
                    reservecancel.Status = this.reserveStatus;

                if (reservecancel.ShowDialog() == DialogResult.OK)
                {
                    reserve.FireConnect();
                    reserve.ReserveOpen(hospitalID);
                    dbc.Delay(400);
                    ReservationListUpdate(state);
                    dbc.Visitor_Open();
                    dbc.VisitorTable = dbc.DS.Tables["visitor"]; // 환자 테이블
                    
                }
            }
        }

        //초진환자 등록
        public void newPatientAdd()
        {
            dbc.Visitor_Open();
            dbc.VisitorTable = dbc.DS.Tables["Visitor"];
            DataRow newRow = dbc.VisitorTable.NewRow();
            newRow["PatientID"] = dbc.VisitorTable.Rows.Count + 1;
            newRow["PatientName"] = textBoxName.Text;
            if (Convert.ToInt32(reserve.patientBirth.Substring(0, 4)) < 2000)
            {
                newRow["PatientBirthcode"] = reserve.patientBirth.Substring(2, 2) + reserve.patientBirth.Substring(5, 2) + reserve.patientBirth.Substring(8, 2) + "-0";
            }
            else if (Convert.ToInt32(reserve.patientBirth.Substring(0, 4)) > 2000)
            {
                newRow["PatientBirthcode"] = reserve.patientBirth.Substring(2, 2) + reserve.patientBirth.Substring(5, 2) + reserve.patientBirth.Substring(8, 2) + "-5";
            }
            newRow["PatientPhone"] = reserve.patientPhone.Substring(0, 3) + reserve.patientPhone.Substring(4, 4) + reserve.patientPhone.Substring(9, 4);
            newRow["PatientAddress"] = reserve.patientAddress;
            newRow["MemberID"] = reserve.patientId;
            newRow["PatientMemo"] = "";

            dbc.VisitorTable.Rows.Add(newRow);
            dbc.DBAdapter.Update(dbc.DS, "Visitor");
            dbc.DS.AcceptChanges();

        }


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

        private void button1_Click(object sender, EventArgs e)
        {
            state = 2;
            ReservationListUpdate(state);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            state = 1;
            ReservationListUpdate(state);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            state = 0;
            ReservationListUpdate(state);
        }

        //예약 검색
        private void button4_Click(object sender, EventArgs e)
        {
            reserve.ConvertToName(textBoxSearch.Text);
            dbc.Delay(200);
            listViewReserve.Items.Clear();
            if(reserve.UserEmail.Count ==0)
            {
                MessageBox.Show("검색어에 일치하는 예약이 없습니다.", "알림");
            }
            for (int i = 0; i < reserve.list.Count; i++)
            {
                for (int j = 0; j < reserve.UserEmail.Count; j++)
                {
                    if (reserve.list[i].id == reserve.UserEmail[j] && reserve.list[i].reservationStatus != ReserveCanceled)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = reserve.list[i].id;
                        item.SubItems.Add(ConvertDate(reserve.list[i].timestamp).ToString("yyyy-MM-dd HH:mm"));
                        item.SubItems.Add(reserve.list[i].reservationDate);
                        item.SubItems.Add(reserve.list[i].reservationTime);
                        if (reserve.list[i].reservationStatus == ReserveWait)
                        {
                            item.SubItems.Add("대기");
                        }
                        else if (reserve.list[i].reservationStatus == ReserveAccepted)
                        {
                            item.SubItems.Add("승인됨");
                        }
                        item.SubItems.Add(reserve.list[i].id);
                        item.SubItems.Add(reserve.list[i].department);
                        listViewReserve.Items.Add(item);

                        this.listViewReserve.ListViewItemSorter = new ListviewItemComparer(1, "asc");
                        listViewReserve.Sort();
                    }
                }
            }
        }

        //접수 등록
        public void ReceptionAdd()
        {
            try
            {

                dbc.Reception_Open();
                dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                DataRow newRow = dbc.ReceptionTable.NewRow();
                newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
                for (int i = 0; i < dbc.VisitorTable.Rows.Count; i++)
                {
                    if (dbc.VisitorTable.Rows[i]["PATIENTNAME"].ToString() == textBoxName.Text)
                    {
                        newRow["PATIENTID"] = i + 1;
                    }
                }

                newRow["ReceptionTime"] = listViewReserve.Items[SelectRow].SubItems[3].Text.Substring(0, 2) + listViewReserve.Items[SelectRow].SubItems[3].Text.Substring(3, 2);
                newRow["ReceptionDate"] = listViewReserve.Items[SelectRow].SubItems[2].Text.Substring(2, 8);
                newRow["SubjectName"] = listViewReserve.Items[SelectRow].SubItems[6].Text;
                for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                {
                    if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == receptionist)
                    {
                        newRow["ReceptionistCode"] = i + 1;
                    }
                }

                newRow["ReceptionInfo"] = ViewComment();
                newRow["ReceptionType"] = 1;
                reserve.FindDocument(hospitalID, selectTime, selectid, selectDate);
                dbc.Delay(200);
                newRow["ReceptionCode"] = Reserve.documentName;
                dbc.ReceptionTable.Rows.Add(newRow);
                dbc.DBAdapter.Update(dbc.DS, "Reception");
                dbc.DS.AcceptChanges();
            }
            catch
            {
                dbc.Delay(200);
                reserve.FindPatient(listViewReserve.Items[SelectRow].SubItems[0].Text);
                dbc.Delay(200);
                dbc.Visitor_Open();
                dbc.VisitorTable = dbc.DS.Tables["Visitor"];
                DataRow newRow = dbc.VisitorTable.NewRow();
                newRow["PatientID"] = dbc.VisitorTable.Rows.Count + 1;
                newRow["PatientName"] = reserve.patientName;
                if (Convert.ToInt32(reserve.patientBirth.Substring(0, 4)) < 2000)
                {
                    newRow["PatientBirthcode"] = reserve.patientBirth.Substring(2, 2) + reserve.patientBirth.Substring(5, 2) + reserve.patientBirth.Substring(8, 2) + "-0";
                }
                else if (Convert.ToInt32(reserve.patientBirth.Substring(0, 4)) > 2000)
                {
                    newRow["PatientBirthcode"] = reserve.patientBirth.Substring(2, 2) + reserve.patientBirth.Substring(5, 2) + reserve.patientBirth.Substring(8, 2) + "-5";
                }
                newRow["PatientPhone"] = reserve.patientPhone.Substring(0, 3) + reserve.patientPhone.Substring(4, 4) + reserve.patientPhone.Substring(9, 4);
                newRow["PatientAddress"] = reserve.patientAddress;
                newRow["MemberID"] = reserve.patientId;
                newRow["PatientMemo"] = "";

                dbc.VisitorTable.Rows.Add(newRow);
                dbc.DBAdapter.Update(dbc.DS, "Visitor");
                dbc.DS.AcceptChanges();
                dbc.Reception_Open();
                dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                newRow = dbc.ReceptionTable.NewRow();
                newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
                for (int i = 0; i < dbc.VisitorTable.Rows.Count; i++)
                {
                    if (dbc.VisitorTable.Rows[i]["PATIENTNAME"].ToString() == textBoxName.Text)
                    {
                        newRow["PATIENTID"] = i + 1;
                    }
                }

                newRow["ReceptionTime"] = listViewReserve.Items[SelectRow].SubItems[3].Text.Substring(0, 2) + listViewReserve.Items[SelectRow].SubItems[3].Text.Substring(3, 2);
                newRow["ReceptionDate"] = listViewReserve.Items[SelectRow].SubItems[2].Text.Substring(2, 8);
                newRow["SubjectName"] = listViewReserve.Items[SelectRow].SubItems[6].Text;
                for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                {
                    if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == receptionist)
                    {
                        newRow["ReceptionistCode"] = i + 1;
                    }
                }

                newRow["ReceptionInfo"] = ViewComment();
                newRow["ReceptionType"] = 1;
                reserve.FindDocument(hospitalID, selectTime, selectid, selectDate);
                dbc.Delay(200);
                newRow["ReceptionCode"] = Reserve.documentName;
                dbc.ReceptionTable.Rows.Add(newRow);
                dbc.DBAdapter.Update(dbc.DS, "Reception");
                dbc.DS.AcceptChanges();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            state = 3;
            ReservationListUpdate(3);
        }
    }
}
