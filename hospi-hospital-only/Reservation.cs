using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    public partial class Reservation : Form
    {
        DBClass dbc = new DBClass();
        Reserve reserve = new Reserve();


        int SelectRow;
        string hospitalID;
        string comment;
        string status;
        string receptionist;

        static int ReserveCanceled = -1;
        static int ReserveWait = 0;
        static int ReserveAccepted = 1;
        

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
            reserve.FireConnect();
            reserve.ReserveOpen(hospitalID);
            dbc.Delay(400);
            ReservationListUpdate();
            dbc.Visitor_Open();
            dbc.VisitorTable = dbc.DS.Tables["visitor"]; // 환자 테이블

            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"]; // 접수자 테이블
        }

        //리스트뷰 업데이트
        public void ReservationListUpdate()
        {
            listViewReserve.Items.Clear();
            for (int i = 0; i < reserve.list.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = reserve.list[i].id;
                item.SubItems.Add(ConvertDate(reserve.list[i].timestamp).ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(reserve.list[i].reservationDate);
                item.SubItems.Add(reserve.list[i].reservationTime);
                if (reserve.list[i].reservationStatus == ReserveCanceled)
                {
                    item.SubItems.Add("취소됨");
                }
                else if (reserve.list[i].reservationStatus == ReserveWait)
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

            //환자 이름 검색
            reserve.FindPatient(listViewReserve.Items[SelectRow].SubItems[0].Text);
            dbc.Delay(200);
            textBoxName.Text = reserve.patientName;
            //문서찾기(병원id, 시간, 환자id, 날짜)
            reserve.FindDocument(hospitalID, listViewReserve.Items[SelectRow].SubItems[3].Text, listViewReserve.Items[SelectRow].SubItems[0].Text, listViewReserve.Items[SelectRow].SubItems[2].Text);
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

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (listViewReserve.Items[SelectRow].SubItems[4].Text == "대기")
            {
                reserve.ReserveAccept();
                dbc.Delay(200);
                reserve.ReserveOpen(hospitalID);
                dbc.Delay(200);

                dbc.Reception_Open();
                dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                DataRow newRow = dbc.ReceptionTable.NewRow();
                newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
                for (int i=0; i< dbc.VisitorTable.Rows.Count; i++)
                {
                    if(dbc.VisitorTable.Rows[i]["PATIENTNAME"].ToString() == textBoxName.Text)
                    {
                        newRow["PATIENTID"] = i+1;
                    }
                }
                
                newRow["ReceptionTime"] = listViewReserve.Items[SelectRow].SubItems[3].Text.Substring(0,2)+ listViewReserve.Items[SelectRow].SubItems[3].Text.Substring(3, 2);
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

                dbc.ReceptionTable.Rows.Add(newRow);
                dbc.DBAdapter.Update(dbc.DS, "Reception");
                dbc.DS.AcceptChanges();

                MessageBox.Show("예약이 접수되었습니다.", "알림");

                ReservationListUpdate();
            }
            else if(listViewReserve.Items[SelectRow].SubItems[4].Text =="승인됨")
            {
                MessageBox.Show("이미 접수가 완료된 예약입니다", "알림");
            }
        }
    }
}
