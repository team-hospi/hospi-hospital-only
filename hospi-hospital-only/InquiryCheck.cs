using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    [FirestoreData]
    public partial class InquiryCheck : Form
    {
        DBClass dbc = new DBClass();
        Inquiry inquiry = new Inquiry();
        FirestoreDb fs;
        string patientID;
        string patientTitle;
        List<Inquiry> list = new List<Inquiry>(); // 문의내역 리스트
        string Check; // 답변 여부
        int selectIndex; // 선택된 셀 인덱스
        string hospitalID;


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

            for (int i = 0; i < list.Count; i++)
            {

                ListViewItem item = new ListViewItem();
                item.Text = list[i].id;
                item.SubItems.Add(list[i].timestamp.ToString());
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            selectIndex = listView1.SelectedIndices[0];
            richTextBox1.Text = list[selectIndex].content;
        }
    }
}
