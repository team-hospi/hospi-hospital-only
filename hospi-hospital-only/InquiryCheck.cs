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
        string hospitalID;
        string Check;

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
            inquiry.FireConnect();

            dbc.Delay(200);
            inquiry.Inquiry_Open(hospitalID);

            dbc.Delay(400);

            for (int i = 0; i < Inquiry.list.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = Inquiry.list[i].id;
                item.SubItems.Add(Inquiry.list[i].timestamp.ToString());
                item.SubItems.Add(Inquiry.list[i].title);
                if (Inquiry.list[i].checkedAnswer == true)
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

    }
}
