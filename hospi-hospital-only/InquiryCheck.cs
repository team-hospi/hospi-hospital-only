﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using System.Collections;

namespace hospi_hospital_only
{
    [FirestoreData]
    public partial class InquiryCheck : Form
    {
        

        DBClass dbc = new DBClass();
        Fcm fcm = new Fcm();
        Inquiry inquiry = new Inquiry();

        FirestoreDb fs;
        string patientID;
        string patientTimestamp;
        List<Inquiry> list = new List<Inquiry>(); // 문의내역 리스트
        string Check; // 답변 여부
        int selectIndex; // 선택된 셀 인덱스
        string documentName; //문서 이름저장
        DateTime Date;
        string hospitalID;
        Boolean inquiryCheck;
        int SelectRow;
        string UserToken; // 유저 토큰

        private static string FBdir = "hospi-edcf9-firebase-adminsdk-e07jk-ddc733ff42.json";


        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }
        public InquiryCheck()
        {
            InitializeComponent();
        }

        private void InquiryCheck_Load(object sender, EventArgs e)
        {
            FireConnect();
            dbc.Delay(200);

            InquiryOpen();
            dbc.Delay(200);

            InquiryListUpdate();


        }

        //폼 자체에서 파이어스토어 연결
        public void FireConnect()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @FBdir;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
        }

        async public void InquiryOpen()
        {
            list.Clear();
            Query qref = fs.Collection("inquiryList").WhereEqualTo("hospitalId", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Inquiry fp = docsnap.ConvertTo<Inquiry>();
                if (docsnap.Exists)
                {
                    Inquiry inquiry = fp;

                    list.Add(inquiry);
                }
            }
        }

        //리스트뷰 더블클릭 이벤트 -> 더블클릭 시 문의 내용 불러오기
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectRow = listView1.SelectedItems[0].Index;
            if(listView1.Items[SelectRow].SubItems[3].Text == "O")
            {
                inquiryCheck = true;
            }
            else
            {
                inquiryCheck = false;
            }
            FindToken(listView1.Items[SelectRow].SubItems[0].Text); // 유저 토큰 가져오기
            dbc.Delay(200);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].id == listView1.Items[SelectRow].SubItems[0].Text && list[i].checkedAnswer == inquiryCheck && list[i].title == listView1.Items[SelectRow].SubItems[2].Text)
                {
                    richTextBox1.Text = list[i].content;
                    richTextBox2.Text = list[i].answer;
                    textBox1.Text = list[i].id;
                }
            }

            //ListAnswer();
            FindDocument();
            
            richTextBox2.Focus();
        }

        //답변 완료 버튼클릭 이벤트
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "" || richTextBox2.Text == " ")
            {
                MessageBox.Show("답변 내용을 적어주세요.", "알림");
            }
            else
            {
                InquiryAnswer();
                dbc.Delay(300);
                InquiryOpen();
                dbc.Delay(300);
                InquiryListUpdate();
                fcm.PushNotificationToFCM(listView1.Items[SelectRow].SubItems[2].Text, UserToken);
                
                MessageBox.Show("답변 완료", "알림");
            }
        }

        //답변 완료함수
        async public void InquiryAnswer()
        {
            try
            {
                DocumentReference docref = fs.Collection("inquiryList").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"answer", richTextBox2.Text},
                    {"checkedAnswer", true }

                };
                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        //문서 이름찾기
        async public void FindDocument()
        {
            Query qref = fs.Collection("inquiryList").WhereEqualTo("hospitalId", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Inquiry fp = docsnap.ConvertTo<Inquiry>();
                if (docsnap.Exists)
                {
                    if(listView1.Items[SelectRow].SubItems[0].Text == fp.id && listView1.Items[SelectRow].SubItems[2].Text == fp.title && inquiryCheck == fp.checkedAnswer)
                    {
                        documentName = docsnap.Id;
                    }
                }
            }
        }
        
        //리스트뷰 업데이트
        public void InquiryListUpdate()
        {
            listView1.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = list[i].id;
                item.SubItems.Add(ConvertDate(list[i].timestamp).ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(list[i].title);
                if (list[i].checkedAnswer == true)
                {
                    Check = "O";
                }
                else
                {
                    Check = "X";
                }
                item.SubItems.Add(Check);
                listView1.Items.Add(item);

                this.listView1.ListViewItemSorter = new ListviewItemComparer(1, "asc");
                listView1.Sort();
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

        //리스트뷰 컬럼 선택시 정렬
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                return;
            }

            listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text.Replace("", "");
            listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text.Replace("", "");


            if (this.listView1.Sorting == SortOrder.Ascending || listView1.Sorting == SortOrder.None)
            {
                this.listView1.ListViewItemSorter = new ListviewItemComparer(e.Column, "desc");
                listView1.Sorting = SortOrder.Descending;
                listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + "";
            }
            else
            {
                this.listView1.ListViewItemSorter = new ListviewItemComparer(e.Column, "asc");
                listView1.Sorting = SortOrder.Ascending;
                listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + "";
            }

            listView1.Sort();
        }
        //답변 불러오기
        async void ListAnswer()
        {

            Query qref = fs.Collection("inquiryList").WhereEqualTo("hospitalId", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Inquiry fp = docsnap.ConvertTo<Inquiry>();
                if (docsnap.Exists)
                {
                    if(listView1.Items[SelectRow].SubItems[0].Text == fp.id && listView1.Items[SelectRow].SubItems[2].Text == fp.title && inquiryCheck == fp.checkedAnswer)
                    {
                        richTextBox1.Text = fp.content;
                        richTextBox2.Text = fp.answer;
                        textBox1.Text = fp.id;
                    }
                }
            }
        }
        // 유저 토큰 가져오기
        public async void FindToken(string patientID)
        {
            try
            {
                Query qref = fs.Collection("userList").WhereEqualTo("email", patientID);
                QuerySnapshot snap = await qref.GetSnapshotAsync();

                foreach (DocumentSnapshot docsnap in snap)
                {
                    Fcm fp = docsnap.ConvertTo<Fcm>();

                    if (docsnap.Exists)
                    {
                        UserToken = fp.token;
                    }
                }
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
