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
        FirestoreDb fs;
        string hospitalID;

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
            dbc.FireConnect();

            dbc.Delay(200);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            dbc.FireConnect();
        }

    }
}
